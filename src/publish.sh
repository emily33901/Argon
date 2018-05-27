#!/usr/bin/env bash

BASEDIR=$(dirname "$0")

pushd $BASEDIR
    # There should be no reason to remove the directory itself
    #rm -rf "bin/"
    # Trigger a cxx build
    ./cxx/make.sh
    
    TERM=xterm dotnet publish Testbed/PublishTest/PublishHelper.csproj --output "../../bin/argon" --self-contained --framework netcoreapp2.0 --runtime linux-x64

    #mv "Testbed/PublishTest/bin/argon" "bin/argon"

    cp -f "cxx/bin/debug/testbed" "bin/testbed"
    cp -f "cxx/bin/debug/libsteamclient.so" "bin/libsteamclient.so"
popd
