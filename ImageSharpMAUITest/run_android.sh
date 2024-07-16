#!/bin/sh

set -e


#emulator -list-avds
#emulator -avd Pixel_3a_API_34

CONFIGURATION=Release
RUNTIME_IDENTIFIER=android-arm64


dotnet build \
    /t:Run \
    --framework net8.0-android \
    --configuration $CONFIGURATION \
    -p:RuntimeIdentifier=$RUNTIME_IDENTIFIER \
    ImageSharpMAUITest/ImageSharpMAUITest.csproj
