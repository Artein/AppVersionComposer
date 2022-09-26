# AppVersionComposer
This package handles app versioning.
App version composes from git tag, number of commits since the tag, commit hash (optionally) and branch name (optionally).
Composed version is written to 2 places: `PlayerSettings.bundleVersion` field and to <a href="#app_version_holder_asset">AppVersionHolder asset</a>

### Example: 

| Name                            | Value           |
|---------------------------------|-----------------|
| Git tag                         | v1.3            |
| Number of commits since the tag | 5               |
| Commit hash                     | f540d3          |
| Branch name                     | MyFeatureBranch |

**Results matrix**

|         | Development build            | Production build |
|---------|------------------------------|------------------|
| iOS     | 1.3.5[^1]                    | 1.3.5[^1]        |
| Android | 1.3.5-f540d3-MyFeatureBranch | 1.3.5-f540d3     |
| MacOS   | 1.3.5-f540d3-MyFeatureBranch | 1.3.5-f540d3     |
| Windows | 1.3.5-f540d3-MyFeatureBranch | 1.3.5-f540d3     |

[^1]: iOS (ipadOS etc) supports only semantic versioning ([semver](https://semver.org/)). To get full app version check usage of <a href="#app_version_holder_asset">AppVersionHolder asset</a>

### Requirements:
* Repository has at least one tag
* Git tag must be 'vX.X', where X - is a positive number (eg. v1.2)

### AppVersionHolder asset {#app_version_holder_asset}
Some platforms might not support any kind of version string (for example iOS). 
Often, they use semantic versioning with strict restriction to have `{major.minor.patch}` view. 
In this case in your implementation you can use `AppVersionHolder` scriptable object located in `Assets/Resources`.
It contains several properties:
* AppVersion — data structure that contains all values required to build app version string
* [editor only] BuildPreprocessorOder — specifies order in which app version will be composed during build preprocessing stage

So, in order to get full app version at Runtime in semver-only platforms, reference `AppVersionHolder` asset and use `AppVersion` property