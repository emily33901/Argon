pushd %~dp0

call create_projects

echo Building RELEASE...

"C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\MSBuild\\15.0\\Bin\\msbuild" /property:GenerateFullPaths=true /t:build premake\doghook.sln /p:Configuration=Release /verbosity:minimal

echo done.

popd
exit
