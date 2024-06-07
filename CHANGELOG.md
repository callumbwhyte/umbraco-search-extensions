# Change Log

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/) and this project adheres to [Semantic Versioning](https://semver.org/).

## [3.2.1] - 2024-06-07
### Fixed
* `Page` extensions return correct total results count

## [3.2.0] - 2024-06-01
### Added
* Support for Search Extensions on Umbraco 13 (LTS), and v14

## [3.1.0] - 2023-06-22
### Added
* Easier paging of `ISearchResults` with new `Page` extensions

### Changed
* `SearchHelper` is now obsolete and will be removed in a future version, use extensions instead

## [3.0.2] - 2023-02-08
### Added
* Overload for the `Page` method within `SearchHelper` that returns a page count

## [3.0.1] - 2023-01-27
### Fixed
* `WhitespaceSeparatorTokenizer` correctly separates whitespace so paths are indexed correctly

## [3.0.0] - 2022-11-28
### Added
* Initial release of Search Extensions for Umbraco v9, v10 (LTS), and v11

## [2.0.0] - 2022-05-30
### Added
* Initial release of Search Extensions for Umbraco 9

## [1.5.1] - 2022-02-29
### Fixed
* `CreatePublishedQuery` returns content without templates and with `umbracoNaviHide` set

## [1.5.0] - 2022-02-28
### Changed
* Indexing UDI values as GUIDs instead of strings for easier searching

### Fixed
* Corrected `IsVisble` query method name to `IsVisible`

## [1.4.1] - 2021-03-14
### Fixed
* `GetResults` method is now available on `IEnumerable<ISearchResult>` to allow for more efficient paging
* `SearchHelper` now uses `GetResults` method for consistency when getting typed results

## [1.4.0] - 2021-02-22
### Added
* `JSON` value type for indexing nested properties as unique fields
* Support for indexing values through multiple field types with `MultipleValueTypeFactory`

### Changed
* Field types no longer inherit `FullText` to control when values are committed to the index

### Fixed
* UDI values are no longer being analyzed and stored in the index

## [1.3.0] - 2021-02-10
### Added
* Support for sanitizing, boosting, fuzzy and wildcard matches on arrays
* Separator for `picker` and `UDI` value types is configurable

### Changed
* Renamed `SantizeSplit` method for getting an array of terms excluding stop words to `ToSafeArray`

### Fixed
* `picker` and `UDI` values are no longer stored in index field values, only in field tokens

## [1.2.0] - 2021-01-26
### Added
* Indexing `createDate` and `updateDate` fields as dates

## [1.1.0] - 2021-01-23
### Added
* Support for querying by culture in `Field`, `Group`, and `NodeName` queries
* Support for querying by multiple node type aliases
* Separator for list field types is now configurable

### Fixed
* Correctly indexing UDIs when multiple values are present

### Removed
* Search-friendly aliases for picker values are no longer indexed as a separate `__Search_` prefixed field

## [1.0.2] - 2020-11-10
### Fixed
* Field types are registered only after Examine has loaded
* Separator characters are correctly identified by the list tokenizer

## [1.0.1] - 2020-11-09
### Added
* Supporting query extensions for nested queries
* Extension for sanitizing and removing stop words from query values

### Fixed
* `CreatePublishedQuery` correctly checks for `template` and `umbracoNaviHide` values

## [1.0.0] - 2020-08-25
### Added
* Initial release of Search Extensions for Umbraco 8.1

[Unreleased]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-3.2.1...HEAD
[3.2.1]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-3.2.0...release-3.2.1
[3.2.0]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-3.1.0...release-3.2.0
[3.1.0]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-3.0.2...release-3.1.0
[3.0.2]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-3.0.1...release-3.0.2
[3.0.1]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-3.0.0...release-3.0.1
[3.0.0]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-2.0.0...release-3.0.0
[2.0.0]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-1.5.1...release-2.0.0
[1.5.1]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-1.5.0...release-1.5.1
[1.5.0]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-1.4.1...release-1.5.0
[1.4.1]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-1.4.0...release-1.4.1
[1.4.0]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-1.3.0...release-1.4.0
[1.3.0]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-1.2.0...release-1.3.0
[1.2.0]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-1.1.0...release-1.2.0
[1.1.0]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-1.0.2...release-1.1.0
[1.0.2]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-1.0.1...release-1.0.2
[1.0.1]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-1.0.0...release-1.0.1
[1.0.0]: https://github.com/callumbwhyte/umbraco-search-extensions/tree/release-1.0.0