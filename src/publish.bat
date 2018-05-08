@echo off

pushd "%~dp0\Testbed\PublishTest"
	dotnet publish --output "..\..\bin\Argon" --self-contained --framework netcoreapp2.0 --runtime win10-x86
popd

pushd "%~dp0"
	copy "TestBed\CppTestBed\bin\Debug\CppTestBed.exe" "bin\CppTestBed.exe"
	copy "steamclient\bin\Debug\steamclient.dll" "bin\steamclient.dll"
popd
