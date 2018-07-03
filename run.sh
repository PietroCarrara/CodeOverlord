#!/bin/sh

cd goserver
go build
GOOS=windows GOARCH=amd64 go build
cd ..

if [[ "$1" = "release" ]]; then
	msbuild /t:Build /p:Configuration=Release
	cd ./CodeOverlord/bin/DesktopGL/AnyCPU/Release/
else
	msbuild
	cd ./CodeOverlord/bin/DesktopGL/AnyCPU/Debug/
fi

# Junk dll's
rm atk-sharp.dll
rm gdk-sharp.dll
rm gio-sharp.dll
rm gtk-sharp.dll
rm glib-sharp.dll
rm cairo-sharp.dll
rm pango-sharp.dll

root="/home/pietro/Projects/CodeOverlord"

rm -rf "./ace"
cp "$root/EditorHtml/ace/" "./ace/" -r

rm -rf "./bootstrap"
cp "$root/EditorHtml/bootstrap/" "./bootstrap/" -r

rm -rf "./css"
mkdir -p "./css"
cp "$root/EditorHtml/font-awesome.min.css" "./css/"

cp "$root/goserver/goserver" goserver
cp "$root/goserver/goserver.exe" goserver.exe

cp "$root/CodeOverlord/Index.html" ./Index.html

mono CodeOverlord.exe

cd "$root"
