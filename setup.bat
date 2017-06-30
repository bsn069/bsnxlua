@echo off

if not exist nogit (
	mkdir nogit
	if not exist nogit (
		echo not found dir nogit in path %CD%
		exit 1
	) 
) 

if not exist nogit/xLua (
	git clone https://github.com/Tencent/xLua.git
	if not exist nogit/xlua (
		echo not found dir nogit/xlua in path %CD%
		exit 1
	) 

	if exist nogit/xLuaTmp (
		rm -rf nogit/xLuaTmp 
	)
	cp -r nogit/xLua/Assets/XLua nogit/XLuaTmp
	cp -r nogit/xLua/Assets/Plugins nogit/XLuaTmp/Plugins
	rm -rf nogit/xLuaTmp/Doc	
	rm -rf nogit/xLuaTmp/Examples	
	rm -rf nogit/xLuaTmp/Tutorial	
	del /s /q nogit\xLuaTmp\*.meta

	if not exist u3d/Assets/XLua (
		mkdir u3d\Assets\XLua
		if not exist u3d/Assets/XLua (
			echo not found dir u3d/Assets/XLua in path %CD%
			exit 1
		) 
	) 
	cp -r nogit/xLuaTmp/* u3d/Assets/XLua
	rm -rf nogit/xLuaTmp
) 


pause