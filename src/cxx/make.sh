# set cwd to script directory
cd "${0%/*}"

./create_projects.sh

echo building...
cd premake/
make config=debug_x64 -j4

echo done.
exit
