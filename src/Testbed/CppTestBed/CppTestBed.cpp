#include <cassert>
#include <cstdio>

#include <Windows.h>

class SteamClientTest {
public:
	virtual int Test(const char *a, int b) = 0;
	virtual const char *Test2(const char *a, const char *b) = 0;
};

using CreateInterfaceFn = void *(__stdcall *)(const char *);

int main(int argc, const char **argv) {
	if (argc == 1) return 0;

	auto path = argv[1];

	auto handle = LoadLibrary(path);

	auto create_interface_proc = (CreateInterfaceFn) GetProcAddress(handle, "CreateInterface");
	if (create_interface_proc != nullptr) {
		auto iface = (SteamClientTest *)create_interface_proc("STEAMCLIENT_INTERFACE_VERSION001");
		if (iface != nullptr) {
			
			auto out = iface->Test("good job nicely done", 10);
			printf("iface->Test('good job nicely done', 10) = %d\n", out);

			auto out2 = iface->Test2("good job ", "nicely done");
			printf("iface->Test2('good job ', 'nicely done') = %s", out2);

		} else {
			printf("CreateInterface returned null!\n");
		}
	} else {
		printf("Unable to find CreateInterface in %s\n", path);
	}

	getc(stdin);

    return 0;
}

