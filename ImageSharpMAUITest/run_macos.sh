#!/bin/sh

set -e

rm -rf ImageSharpMAUITest/bin
rm -rf ImageSharpMAUITest/obj
rm -rf ~/.nuget/packages/sixlabors.imagesharp/0.0.1

CONFIGURATION=Release
RUNTIME_IDENTIFIER=maccatalyst-arm64


dotnet build \
    /t:Run \
    --framework net9.0-maccatalyst \
    --configuration $CONFIGURATION \
    -p:RuntimeIdentifier=$RUNTIME_IDENTIFIER \
    ImageSharpMAUITest/ImageSharpMAUITest.csproj
