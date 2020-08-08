![logo](https://github.com/nightwolf93/brisk/blob/master/logo.png?raw=true)

# BriskDotNet

[![CircleCI](https://circleci.com/gh/nightwolf93/BriskDotNet.svg?style=svg)](https://github.com/nightwolf93/brisk)

BriskDotNet is a .net library for interact with the Brisk API

## Documentation

First create a instance of BriskClient

```csharp
var client = new BriskClient("http://localhost:3000", "master", "changeme");
```

Create a link

```csharp
var link = await client.CreateLink(new CreateLinkRequest("https://github.com/nightwolf93/brisk", 30000, 5));
```

You can check the official brisk api too : https://nico-style931.gitbook.io/brisk/

## Nuget

```
dotnet add package BriskDotNet
```

https://www.nuget.org/packages/BriskDotNet/

## Build

```
make build
```

## Test

```
make test
```

## Authors

Nightwolf93
