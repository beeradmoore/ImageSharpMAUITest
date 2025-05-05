#!/bin/sh

set -e

CONFIGURATION=Release
RUNTIME_IDENTIFIER=maccatalyst-arm64


dotnet build \
    /t:Run \
    --framework net9.0-maccatalyst \
    --configuration $CONFIGURATION \
    -p:RuntimeIdentifier=$RUNTIME_IDENTIFIER \
    ImageSharpMAUITest/ImageSharpMAUITest.csproj
