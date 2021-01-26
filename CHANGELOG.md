# Change Log

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/) and this project adheres to [Semantic Versioning](https://semver.org/).

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

[Unreleased]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-1.2.0...HEAD
[1.2.0]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-1.1.0...release-1.2.0
[1.1.0]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-1.0.2...release-1.1.0
[1.0.2]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-1.0.1...release-1.0.2
[1.0.1]: https://github.com/callumbwhyte/umbraco-search-extensions/compare/release-1.0.0...release-1.0.1
[1.0.0]: https://github.com/callumbwhyte/umbraco-search-extensions/tree/release-1.0.0