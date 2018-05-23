@echo off

pushd "%~dp0\Testbed\PublishTest"
	dotnet publish --output "..\..\bin\Argon" --self-contained --framework netcoreapp2.0 --runtime win10-x86
popd

pushd "%~dp0"
	copy "cxx\bin\debug\testbed.exe" "bin\cpptestbed.exe"
	copy "cxx\bin\debug\steamclient.dll" "bin\steamclient.dll"
popd
