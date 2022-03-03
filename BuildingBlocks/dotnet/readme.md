# Semantic versioning
The BuildingBlocks roughly adheres to Semantic Versioning (SemVer), adopting the use of MAJOR.MINOR.PATCH versioning (with some adaptions), using the various parts of the version number to describe the degree and type of change.

```
DOTNET_VERSION.MAJOR.MINOR.PATCH[-PRERELEASE-BUILDNUMBER]
```
The optional PRERELEASE and BUILDNUMBER parts are never part of supported releases and only exist on nightly builds, local builds from source targets, and unsupported preview releases including developers' releases.

## Understand runtime version number changes
- DOTNET_VERSION coresponds to .NET version released by Microsoft

- MAJOR is incremented once a year and may contain:
  - Significant changes in the product, or a new product direction.
  - API introduced breaking changes. There's a high bar to accepting breaking changes.
  - A newer MAJOR version of an existing dependency is adopted.

- MINOR is incremented when:
  - Public API surface area is added.
  - A new behavior is added.
  - A newer MINOR version of an existing dependency is adopted.
  - A new dependency is introduced.

- PATCH is incremented when:
  - Bug fixes are made.
  - Support for a newer platform is added.
  - A newer PATCH version of an existing dependency is adopted.
  - Any other change doesn't fit one of the previous cases.
