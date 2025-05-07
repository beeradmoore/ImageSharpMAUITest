
REM rm -rf ImageSharpMAUITest/bin
REM rm -rf ImageSharpMAUITest/obj
REM rm -rf ~/.nuget/packages/sixlabors.imagesharp/0.0.1

dotnet publish ^
    -framework net9.0-windows10.0.19041.0 -c Release -p:RuntimeIdentifierOverride=win10-x64 -p:WindowsPackageType=None
