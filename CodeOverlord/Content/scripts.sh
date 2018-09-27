#! /bin/sh
#
# scripts.sh
#
# Corrects the 'build' action to 'copy' on script files

sed -i 's=/build:Scripts=/copy:Scripts=g' Content.mgcb

