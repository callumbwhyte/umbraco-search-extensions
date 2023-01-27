# Umbraco Search Extensions

<img src="docs/img/logo.png?raw=true" alt="Umbraco Search Extensions" width="250" align="right" />

[![NuGet release](https://img.shields.io/nuget/v/Our.Umbraco.Extensions.Search.svg)](https://www.nuget.org/packages/Our.Umbraco.Extensions.Search/)

## Getting started

This package is supported on Umbraco v9, v10, and v11

### Installation

Search Extensions is available via [NuGet](https://www.nuget.org/packages/Our.Umbraco.Extensions.Search/).

To install with the .NET CLI, run the following command:

    $ dotnet add package Our.Umbraco.Extensions.Search

To install from within Visual Studio, use the NuGet Package Manager UI or run the following command:

    PM> Install-Package Our.Umbraco.Extensions.Search

## Usage

### Querying

There are several short-hand extension methods for querying Umbraco content in an index – checking if an item is published, is visible, or has a template.

Querying only published content items can be done like this:

```
query.And().IsPublished()
```

Similarly, querying all content where the `umbracoNaviHide` property is **not** set can be done like this:

```
query.And().IsVisible()
```

It is possible to query content with a specific template ID set. If `0` or no value is passed to the method, the query will match content with **any** templatee ID set.

```
query.And().HasTemplate(int templateId)
```

Finally, it is possible to query for content that has **any** one of the specified content type aliases. Out of the box Umbraco supports querying for a single content alias.

```
query.And().NodeTypeAlias(string[] aliases)
```

### Cultures

Umbraco properties that have been set to "vary by culture" are indexed with a specific alias: `{fieldName}_{culture}`. For example, if the "pageTitle" field varies by culture and has 2 languages, English and Spanish, the index would contain 2 fields: `pageTitle_en` and `pageTitle_es`.

A culture can be passed to `Field` and `NodeName` queries like this:

```
query.And().Field(string field, string culture)

query.And().NodeName(string nodeName, string culture)
```

It even works with grouped queries such as `GroupedAnd`, `GroupedOr`, and `GroupedNot`, where multiple fields can be specified:

```
query.And().GroupedOr(string[] fields, string culture)
```

### Searching

The `SearchHelper` class contains logic for commonly performed actions when searching, particularly helpful for creating paged search functionality.

The `Get<T>` method gets all results for a query cast to a given type, including `IPublishedContent`.

```
var query = searcher.CreatePublishedQuery();

var results = searchHelper.Get<T>(query, out int totalResults);
```

The `Page<T>` method efficiently gets a given number of items *(`perPage`)* at a specific position *(`page`)* in the results for a query. An optional type constraint can be added to also return paged results cast to `IPublishedContent`.

```
var query = searcher.CreatePublishedQuery();

var results = searchHelper.Page<T>(query, int page, int perPage, out int totalResults);
```

All helper methods provide the total number of results found as an `out` parameter.

### Results

For more specific cases where the `SearchHelper` is not appropriate, the same features for accessing strongly typed results are available as extension methods.

An entire results collection can be cast to a type like this:

```
var results = query.Execute().GetResults<T>();
```

Specific fields from an individual search result can be accessed via the `.Value<T>()` extension method like this:

```
foreach (var result in query.Execute())
{
    var value = result.Value<T>(string field);
}
```

### Advanced fields

Search Extensions introduces several new field types into Examine – `json`, `list`, `UDI` and `picker` – to ensure Umbraco data is correctly indexed and queryable.

Examine allows controlling an index's fields, field types, and [more](https://shazwazza.github.io/Examine/configuration#iconfigurenamedoptions), via [.NET's Named Options pattern](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options):

```
public class ConfigureIndexOptions : IConfigureNamedOptions<LuceneDirectoryIndexOptions>
{
    public void Configure(string name, LuceneDirectoryIndexOptions options)
    {
        if (name == "ExternalIndex")
        {
            options.FieldDefinitions.AddOrUpdate(new FieldDefinition("fieldName", "fieldType"));
        }
    }
}
```

The options class must be registered in the [Dependency Injection](https://our.umbraco.com/documentation/reference/using-ioc/) container to apply:

```
builder.Services.ConfigureOptions<ConfigureIndexOptions>();
```

#### Core fields

Umbraco's "path" field is automatically indexed as a list and so a content item with the path `-1,1050,1100` can be queried like this:

```
query.Field("path", "1100");
```

Umbraco's "createDate" and "updateDate" fields are automatically indexed as `date` values, whereas they would be regularly indexed as string values.

#### Pickers

The `picker` field type adds search-friendly aliases for the picked items into the index.

A picker with a selected a content item called "Example Page" can be queried like this:

```
query.Field("somePicker", "example-page");
```

#### JSON

The `json` field type splits the properties of a JSON object into individual fields within the index.

Imagine a field called "locations" has the following JSON value:

```
[
    {
        "city": "London",
        "position": {
            "latitude": 51.5074,
            "longitude": 0.1278
        }
    },
    {
        "city": "New York",
        "position": {
            "latitude": 40.7128,
            "longitude": 74.0060
        }
    }
]
```

Each property will be created as a field in the index, including any nested properties. In this example these would be called "locations_city", "locations_position_latitude" and "locations_position_longitude".

It is possible to index a subset of a JSON object's properties by supplying a path in [JSON Path format](https://www.newtonsoft.com/json/help/html/QueryJsonSelectTokenJsonPath.htm).

Register a new `ValueTypeFactory` in the index implementing the `json` type, and define the path as a parameter, before assigning it to a field:

```
public class ConfigureIndexOptions : IConfigureNamedOptions<LuceneDirectoryIndexOptions>
{
    public void Configure(string name, LuceneDirectoryIndexOptions options)
    {
        if (name == "ExternalIndex")
        {
            options.IndexValueTypesFactory = new Dictionary<string, IFieldValueTypeFactory>(options.IndexValueTypesFactory)
            {
                ["position"] = new DelegateFieldValueTypeFactory(fieldName =>
                {
                    return new JsonValueType(fieldName, "$[*].position");
                };
            };

            options.FieldDefinitions.AddOrUpdate(new FieldDefinition("locations", "position"));
        }
    }
}
```

#### Multiple field types

There are advanced cases where indexing a value as multiple field types might be necessary, such as indexing different parts of the same JSON object into separately named fields or indexing specific properties within a JSON object as a defined type.

The `MultipleValueTypeFactory` assigns a chain of field types to a field and applies them in sequence:

```
public class ConfigureIndexOptions : IConfigureNamedOptions<LuceneDirectoryIndexOptions>
{
    public void Configure(string name, LuceneDirectoryIndexOptions options)
    {
        if (name == "ExternalIndex")
        {
            options.IndexValueTypesFactory = new Dictionary<string, IFieldValueTypeFactory>(options.IndexValueTypesFactory)
            {
                ["locationData"] = new DelegateFieldValueTypeFactory(fieldName =>
                {
                    return new MultipleValueTypeFactory(
                        fieldName,
                        new IIndexFieldValueType[]
                        {
                            new JsonValueType(x, "$[*].city"),
                            new JsonValueType("position", "$[*].position")
                        }
                    );
                };
            };

            options.FieldDefinitions.AddOrUpdate(new FieldDefinition("locations", "locationData"));
        }
    }
}
```

In this example, the same "locations" JSON object will include all cities while an entirely new "position" field will be created including all latitudes and longitudes.

## Contribution guidelines

To raise a new bug, create an issue on the GitHub repository. To fix a bug or add new features, fork the repository and send a pull request with your changes. Feel free to add ideas to the repository's issues list if you would to discuss anything related to the library.

### Who do I talk to?

This project is maintained by [Callum Whyte](https://callumwhyte.com/) and contributors. If you have any questions about the project please get in touch on [Twitter](https://twitter.com/callumbwhyte), or by raising an issue on GitHub.

## Credits

The package logo uses the [Magnifying Glass](https://thenounproject.com/term/search/74453/) icon from the [Noun Project](https://thenounproject.com/) by [Rohith M S](https://thenounproject.com/rohithdezinr/), licensed under [CC BY 3.0 US](https://creativecommons.org/licenses/by/3.0/us/).

### A special #h5yr to our contributors

* [Busra Sengul](https://github.com/busrasengul)
* [Chriztian Steinmeier](https://github.com/greystate)
* [Dave Woestenborghs](https://github.com/dawoe)

## License

Copyright &copy; 2023 [Callum Whyte](https://callumwhyte.com/), and other contributors

Licensed under the [MIT License](LICENSE.md).