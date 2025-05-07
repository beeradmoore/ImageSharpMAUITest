#!/bin/sh

set -e

rm -rf ImageSharpMAUITest/bin
rm -rf ImageSharpMAUITest/obj
rm -rf ~/.nuget/packages/sixlabors.imagesharp/0.0.1

CONFIGURATION=Debug

# Config for device
#RUNTIME_IDENTIFIER=ios-arm64
#DEVICE_UDID=YOUR-DEVICE-UDID-GOES-HERE

# Config for simulator

# For simulator udid you can get it with the following command
# xcrun simctl list devices "iPhone 15"

RUNTIME_IDENTIFIER=iossimulator-arm64
DEVICE_UDID=:v2:udid=SIMULATOR-UDID-GOES-HERE

dotnet build \
    /t:Run \
    --framework net9.0-ios \
    --configuration $CONFIGURATION \
    -p:RuntimeIdentifier=$RUNTIME_IDENTIFIER \
    -p:_DeviceName=$DEVICE_UDID \
    ImageSharpMAUITest/ImageSharpMAUITest.csproj
