local p = premake
local tree = p.tree
local project = p.project
local config = p.config
local cmake = p.modules.cmake
cmake.config = {}
local m = cmake.config
m.elements = {}

-- Flags
function m.flags(cfg)
  local cmakeflags = '-Wall'
  local buildType = 'RelWithDebInfo'
  if cfg.flags and #cfg.flags > 0 then
    for _, flag in ipairs(cfg.flags) do
      if flag == 'C++11' then
        _p(1, 'set(CMAKE_CXX_STANDARD 11)')
      elseif flag == 'C++14' then
        _p(1, 'set(CMAKE_CXX_STANDARD 14)')
      elseif flag == 'Symbols' then
        buildType = 'DebugFull'
      elseif flag == 'FatalWarnings' or flag == 'FatalCompileWarnings' then
        cmakeflags = cmakeflags..' -Werror'
      elseif flag == 'Unicode' then
        _p(1,'add_definitions(-DUNICODE -D_UNICODE)')
      end
    end
  end
  _p(1, 'set(CMAKE_C_FLAGS "${CMAKE_C_FLAGS} %s")', cmakeflags)
  _p(1, 'set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} %s")', cmakeflags)
  _p(1, 'set(CMAKE_BUILD_TYPE %s)', buildType)
end

-- Add files
function m.files(cfg)
  if cfg.files then
    _p('')
    _p(1, "set(SRC ")
    for i,v in ipairs(cfg.files) do
      _p(2, project.getrelative(cfg.project, v))
    end
    _p(1, ")")
  end
end

-- Generate Defines
function m.defines(cfg)
  if cfg.defines and #cfg.defines then
    _p('')
    _p(1,'add_definitions(')
    for _, define in ipairs(cfg.defines) do
      _p(2, '-D%s', define)
    end
    _p(1,')')
  end
end

-- Generate include directories
function m.includedirs(cfg)
  if cfg.includedirs and #cfg.includedirs > 0 then
    _p('')
    _p(1,'set(INCLUD_DIRS ')
    for _, includedir in ipairs(cfg.includedirs) do
      local dirpath = project.getrelative(cfg.project, includedir)
      _p(2, dirpath)
    end
    _p(1,')')
    _p(1,'include_directories(${INCLUD_DIRS})')
  end
end

-- Add executable / libs
function m.target(cfg)
  local kind = cfg.project.kind
  local targetname = cmake.targetname(cfg)
  if kind == 'StaticLib' then
    _p(1,'add_library( %s STATIC ${SRC})', targetname)
  elseif kind == 'SharedLib' then
    _p(1,'add_library( %s SHARED ${SRC})', targetname)
  elseif kind == 'ConsoleApp' or kind == 'WindowedApp' then
    _p(1,'add_executable( %s ${SRC})', targetname)
  else

  end
end

-- Set targets output properties
function m.targetprops(cfg)
  local targetname = cmake.targetname(cfg)
  local filename = cfg.filename
  if cfg.targetname then filename = cfg.targetname end
  if cfg.targetdir and targetname and filename then
    _p(1,'set_target_properties( %s ', targetname)
    _p(2,'PROPERTIES')
    _p(2,'ARCHIVE_OUTPUT_DIRECTORY "%s"', cfg.targetdir)
    _p(2,'LIBRARY_OUTPUT_DIRECTORY "%s"', cfg.targetdir)
    _p(2,'RUNTIME_OUTPUT_DIRECTORY "%s"', cfg.targetdir)
    _p(2,'OUTPUT_NAME  "%s"', filename)
    _p(1,')')
  end
end

-- Set lib directories
function m.libdirs(cfg)
  if #cfg.libdirs > 0 then
    _p('')
    _p(1,'set(LIB_DIRS')
    local libdirs = project.getrelative(cfg.project, cfg.libdirs)
    for _, libpath in ipairs(libdirs) do
      _p(2, libpath)
    end
    _p(1,')')
    _p(1,'link_directories(${LIB_DIRS})')
  end
end

-- Set Link libs
function m.links(cfg)
  local links = config.getlinks(cfg, "system", "fullpath")
  if links and #links>0 then
    _p('')
    _p(1, 'set(LIBS ')
    for _, libname in ipairs(links) do
      _p(2, libname)
    end
    _p(1, ')')
    local targetname = cmake.targetname(cfg)
    _p(1, 'target_link_libraries(%s ${LIBS})', targetname)
  end
end

-- Generate Call array
function m.elements.generate(cfg)
  return {
    m.flags,
    m.defines,
    m.includedirs,
    m.libdirs,
    m.files,
    m.target,
    m.targetprops,
    m.links,
  }
end

function m.generate(prj, cfg)
	p.callArray(m.elements.generate, cfg)
end
