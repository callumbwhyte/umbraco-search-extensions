# Umbraco Search Extensions

<img src="docs/img/logo.png?raw=true" alt="Umbraco Search Extensions" width="250" align="right" />

[![NuGet release](https://img.shields.io/nuget/v/Our.Umbraco.Extensions.Search.svg)](https://www.nuget.org/packages/Our.Umbraco.Extensions.Search/)

## Getting started

This package is supported on Umbraco 8.1+.

### Installation

Search Extensions is available from NuGet, or as a manual download directly from GitHub.

#### NuGet package repository

To [install from NuGet](https://www.nuget.org/packages/Our.Umbraco.Extensions.Search/), run the following command in your instance of Visual Studio.

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

#### Cultures

Umbraco properties that have been set to "vary by culture" are indexed with a specific alias: `{culture}_{fieldName}`. For example, if the "pageTitle" field varies by culture and has 2 languages, English and Spanish, the index would contain 2 fields: `en_pageTitle` and `es_pageTitle`.

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
var query = _searcher.CreatePublishedQuery();

var results = _searchHelper.Get<T>(query, out int totalResults);
```

The `Page<T>` method efficiently gets a given number of items *(`perPage`)* at a specific position *(`page`)* in the results for a query An optional type constraint can be added to also return paged results cast to `IPublishedContent`.

```
var query = _searcher.CreatePublishedQuery();

var results = _searchHelper.Page<T>(query, int page, int perPage, out int totalResults);
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

Search Extensions introduces several new field types into Examine – `list`, `UDI` and `picker` – to ensure Umbraco data is correctly indexed and queryable. Defining which fields in the index use which types is done through the `IExamineManager`:

```
if (_examineManager.TryGetIndex("ExternalIndex", out IIndex index))
{
    index.FieldDefinitionCollection.AddOrUpdate(new FieldDefinition("somePicker", "picker"));
}
```

The `picker` field type adds search-friendly aliases for the picked items into the index.

A picker with a selected a content item called "Example Page" can be queried like this:

```
query.Field("somePicker", "example-page");
```

Umbraco's "path" field is automatically indexed as a list and so a content item with the path `-1,1050,1100` can be queried like this:

```
query.Field("path", "1100");
```

## Contribution guidelines

To raise a new bug, create an issue on the GitHub repository. To fix a bug or add new features, fork the repository and send a pull request with your changes. Feel free to add ideas to the repository's issues list if you would to discuss anything related to the library.

### Who do I talk to?

This project is maintained by [Callum Whyte](https://callumwhyte.com/) and contributors. If you have any questions about the project please get in touch on [Twitter](https://twitter.com/callumbwhyte), or by raising an issue on GitHub.

## Credits

The package logo uses the [Magnifying Glass](https://thenounproject.com/term/search/74453/) icon from the [Noun Project](https://thenounproject.com/) by [Rohith M S](https://thenounproject.com/rohithdezinr/), licensed under [CC BY 3.0 US](https://creativecommons.org/licenses/by/3.0/us/).

## License

Copyright &copy; 2020 [Callum Whyte](https://callumwhyte.com/), and other contributors

Licensed under the [MIT License](LICENSE.md).