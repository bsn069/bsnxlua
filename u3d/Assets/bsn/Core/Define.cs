namespace NBsn 
{


public enum E_ResLoadType 
{
	EditorABRes = 0, // ABRes目录 原资源
	EditorABOut = 1, // ABOut目录 ab
	AppAB = 2,
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