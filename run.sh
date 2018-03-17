#!/bin/sh

if [[ "$1" = "release" ]]; then
	msbuild /t:Build /p:Configuration=Release
	cd ./CodeOverlord/bin/DesktopGL/AnyCPU/Release/
else
	msbuild
	cd ./CodeOverlord/bin/DesktopGL/AnyCPU/Debug/
fi

mono CodeOverlord.exe
cd ../../../../../
