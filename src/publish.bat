@echo off

pushd "%~dp0"
	dotnet publish Testbed/PublishTest/PublishHelper.csproj --output "..\..\bin\argon" --self-contained --framework netcoreapp2.0 --runtime win-x86

	copy "cxx\bin\debug\testbed.exe" "bin\cpptestbed.exe"
	copy "cxx\bin\debug\steamclient.dll" "bin\steamclient.dll"
popd
