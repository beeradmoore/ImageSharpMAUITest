# ImageSharp MAUI Test

SixLabors.ImageSharp.0.0.1.nupkg was built by:
- pulling the ImageSharp repo
- changing to the mono PR
- init the submodule
- executing `dotnet pack -c Release`
- copying the `.nupkg` into `local-packages` folder.

## Swapping between latest release and local nuget builds

I keep my IDEs closed while doing this to ensure it does not interfere with things.

I edit `ImageSharpMAUITest.csproj` to change the line

```xml
<PackageReference Include="SixLabors.ImageSharp" Version="x.y.z" />
```

to be either version 3.1.4 (remote) or 0.0.1 (local).

I then delete `bin/`, `obj/`, and `~/.nuget/packages/sixlabors.*`. When I build the nuget packages are restored which will mean also loaded directly from local directory or nuget.org.

## Building and deploying for debug

```
dotnet build -t:Run -c Debug -f net8.0-ios -p:RuntimeIdentifier=ios-arm64 -p:_DeviceName=MYDEVICEID
```

## Building and deploying for release

```
dotnet build -t:Run -c Release -f net8.0-ios -p:RuntimeIdentifier=ios-arm64 -p:_DeviceName=MYDEVICEID
```