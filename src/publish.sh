#!/usr/bin/env bash

BASEDIR=$(dirname "$0")

pushd $BASEDIR
    rm -rf "bin/"
    TERM=xterm dotnet publish Testbed/PublishTest/PublishHelper.csproj --output "../../bin/argon" --self-contained --framework netcoreapp2.0 --runtime linux-x64

    #mv "Testbed/PublishTest/bin/argon" "bin/argon"

    cp "cxx/bin/debug/testbed" "bin/testbed"
    cp "cxx/bin/debug/libsteamclient.so" "bin/libsteamclient.so"
popd
