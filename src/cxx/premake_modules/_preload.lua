local p = premake

newaction {
	trigger         = "cmake",
	shortname       = "CMake",
	description     = "Generate classical CMakeLists.txt",

	valid_kinds     = { "ConsoleApp", "WindowedApp", "StaticLib", "SharedLib" },

	valid_languages = { "C", "C++" },

	valid_tools     = {
		cc = { "clang", "gcc" },
	},
	onWorkspace = function(wks)
		p.modules.cmake.generateWorkspace(wks)
	end,
	onProject = function(prj)
		p.modules.cmake.generateProject(prj)
	end,
	onCleanWorkspace = function(wks)
		p.modules.cmake.cleanWorkspace(wks)
	end,
	onCleanProject = function(prj)
		p.modules.cmake.cleanProject(prj)
	end,
	onCleanTarget = function(prj)
		p.modules.cmake.cleanTarget(prj)
	end,
}

return function(cfg)
  return _ACTION == 'export-compile-commands' or _ACTION == 'cmake'
end
