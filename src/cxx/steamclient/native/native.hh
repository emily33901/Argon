#pragma once

namespace native_loader {
// Returns true on success
bool load_runtime(const char *relative_location, const char *relative_dotnet_root);

// Returns null on failure
void *create_delegate(const char *assembly_name, const char *type_name, const char *entry_point);
} // namespace native_loader