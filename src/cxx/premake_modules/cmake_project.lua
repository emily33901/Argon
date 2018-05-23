local p = premake
local tree = p.tree
local project = p.project
local config = p.config
local cmake = p.modules.cmake

cmake.project = {}
local m = cmake.project


function cmake.getLinks(cfg)
	-- System libraries are undecorated, add the required extension
	return config.getlinks(cfg, "system", "fullpath")
end

function cmake.getSiblingLinks(cfg)
	-- If we need sibling projects to be listed explicitly, add them on
	return config.getlinks(cfg, "siblings", "fullpath")
end


m.elements = {}

m.ctools = {
	gcc = "gnu gcc",
	clang = "clang",
	msc = "Visual C++",
}
m.cxxtools = {
	gcc = "gnu g++",
	clang = "clang++",
	msc = "Visual C++",
}



m.elements.project = function(prj, cfg)
	return {
		m.files,
	}
end

-- Project: Generate the cmake project file.
function m.generate(prj)
	p.utf8()
	for cfg in project.eachconfig(prj) do
		local target = prj.name..'_'..cmake.cfgname(cfg)
		local funcn  = 'project_'..target
		_p('function(%s)', funcn)
		-- _p(1, cmake.cfgname(cfg)..'()')
		-- p.callArray(m.elements.project, prj, cfg)
		cmake.config.generate(prj,cfg)
		_p('endfunction(%s)', funcn)
		_p(funcn..'()')
		_p('')
	end
end
