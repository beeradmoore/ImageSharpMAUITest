#!/bin/sh

set -e

rm -rf ImageSharpMAUITest/bin
rm -rf ImageSharpMAUITest/obj


#emulator -list-avds
#emulator -avd Pixel_3a_API_34

CONFIGURATION=Release
RUNTIME_IDENTIFIER=android-arm64


dotnet build \
    /t:Run \
    --framework net9.0-android \
    --configuration $CONFIGURATION \
    -p:RuntimeIdentifier=$RUNTIME_IDENTIFIER \
    ImageSharpMAUITest/ImageSharpMAUITest.csproj
