@echo off

if not exist nogit (
	mkdir nogit
) 

pushd nogit
if not exist xlua (
	git clone https://github.com/Tencent/xLua.git
) 
popd