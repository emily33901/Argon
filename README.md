# Argon

Argon `/ˈɑːɡɒn/` is an attempt to create a open source replacement for `steamclient.dll` that can be used as a drop-in replacement for the original version (which is definitely very far-fetched). Argon is __not__ `open-steamworks`. it is not trying to expose interfaces that already exist to users, it is trying to replace the interface in its entirety.

There is still lots of work to be done, Argon is only in its infancy right now. 
More reversing and implementing still needs to be done in order to make this project viable, although the overall structure has been established (see the longer overview for more information about this).

## Quick overview:
`Argon` aims to mimic the frontend behaviour of `steamclient` (that is not the UI but rather the backend for steam) so that it can be dropped inplace and functionality should remain intact. 
It primarily uses C# and alot of interoperability helpers to expose code. 
The `steamclient` project bootstraps the `ArgonCore` project and then through the `steamclient` C api users will interact with `Argon` in the same way they did with the default `steamclient`.