# Cyanide

Cyanide is a Blockly-based code editor that can create a C++ operating system, from blockcode.

# Windows
## Dependencies
.NET 6.0 
## Installation
Download `CyanideInstaller.exe` from Releases, and run it. (not out yet lol)
# Linux
## Installation
For compatibility purposes, you will have to build the Linux version of Cyanide from source.
See Build.
## Dependencies
g++
linux-headers
git
make
## Build
Building cyanide on linux is easy, as there is a Makefile provided.
Just run: `make cyanide` and you should be good.
Then, run: `sudo cp /CyanideSourceCodePath/cyanide /usr/bin` to install cyanide.
Finally, run `cyanide`. You should see `cyanide: no parameters specified
for a list of parameters, use cyanide -lp`.

(make sure to run `cyanide -r` before you launch the editor!)
