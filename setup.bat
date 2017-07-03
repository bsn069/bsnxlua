@echo off

if not exist nogit (
	echo create nogit dir
	mkdir nogit
	if not exist nogit (
		echo not found dir nogit in path %CD%
		goto Exit1
	) 
) 

pushd nogit
	if not exist xLua (
		echo clone xLua
		git clone https://github.com/Tencent/xLua.git
		if not exist xlua (
			echo not found dir xlua in path %CD%
			goto Exit1
		) 
	)

	pushd xLua
		echo update xLua
		git pull
	popd

	if exist xLuaTmp (
		echo remove xLuaTmp dir
		rm -rf xLuaTmp
	)

	echo init XLuaTmp dir
	cp -r xLua/Assets/XLua XLuaTmp
	cp -r xLua/Assets/Plugins XLuaTmp/Plugins
	pushd XLuaTmp
		rm -rf Doc	
		rm -rf Examples	
		rm -rf Tutorial	
		rm -rf Plugins/WSA
		find . -name "*.meta" -exec rm -f {} ;
	popd

popd

echo copy new XLua
if not exist u3d\Assets\XLua (
	mkdir u3d\Assets\XLua
	if not exist u3d\Assets\XLua (
		echo not found dir u3d\Assets\XLua 
		goto Exit1
	)
)
cp -r nogit/XLuaTmp/* u3d/Assets/XLua 
 

:Exit1
REM pause
