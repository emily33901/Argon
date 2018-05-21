# Argon

Argon `/ˈɑːɡɒn/` is an attempt to create a open source replacement for `steamclient.dll` that can be used as a drop-in replacement for the original version (which is definitely very far-fetched). Argon is __not__ `open-steamworks`. it is not trying to expose interfaces that already exist to users, it is trying to replace the interface in its entirety.

There is still lots of work to be done, Argon is only in its infancy right now. 
More reversing and implementing still needs to be done in order to make this project viable, although the overall structure has been established (see the longer overview for more information about this).

## Quick overview:
`Argon` aims to mimic the frontend behaviour of `steamclient` (that is not the UI but rather the backend for steam) so that it can be dropped inplace and functionality should remain intact. 
It primarily uses C# and alot of interoperability helpers to expose code. 
The `steamclient` project bootstraps the `ArgonCore` project and then through the `steamclient` C api users will interact with `Argon` in the same way they did with the default `steamclient`.

## Longer overview:
`Argon` is programmed primarily in C# as it not only provides easy interoperability with SteamKit2 (Which deals with alot of the `steamclient` backend) but also easy serialization and IPC functionality which would be difficult to implement with other languages.

The downside of this is that `steamclient` is a native dll, and is loaded into processes as such. In order to deal with this there is a `.net core` bootstrapper that loads `ArgonCore` into the process and provides the native C dll api to the whoever loaded the dll. `ArgonCore` then also needs to export native vtables to the process that called it. This is done using methods in `Interface.Loader` and `Interface.Context` along with the `AutoGenerator` project that deals with the creation of delegate types for these interfaces.

`steamclient` uses a single host instance of itself acting as a central server for all of steam's jobs. This allows steam to coordinate between games that use the `steam_api`, the steam UI and overlay. The native `steamclient` does this using IPC and serialized functions. This has been mimicked in `Argon` through the use of `NamedPipes` and function maps (which area also generated with the autogenerator).