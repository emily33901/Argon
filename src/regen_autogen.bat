@echo off

echo Regenerating...

pushd "%~dp0"Interfaces\AutoGenerator
    dotnet run
popd