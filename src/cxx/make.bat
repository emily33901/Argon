pushd %~dp0

call create_projects

echo building...

"C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Enterprise\\MSBuild\\15.0\\Bin\\msbuild" /property:GenerateFullPaths=true /t:build premake\workspace.sln /p:Configuration=Debug /verbosity:minimal

echo done.

popd