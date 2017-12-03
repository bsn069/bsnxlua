namespace NBsn 
{


public enum E_ResLoadType 
{
	EditorRes = 0, // 编辑器原资源
	EditorAB = 1, // 编辑器使用ab
	AppAB = 2, // 使用ab
}


// ui显示状态
public enum E_UIShowState 
{
	Showing = 0, // 显示过程中
	Show 	= 1, // 显示
	Hiding 	= 2, // 隐藏过程中
	Hide 	= 3, // 隐藏
	NoInit 	= 4, // 尚未初始化
}


}