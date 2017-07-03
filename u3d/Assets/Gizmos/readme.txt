如下代码所示它可以在Scene视图里给某个坐标绘制一个icon。它的好处是可以传一个Vecotor3 作为图片显示的位置。 参数2就是图片的名子，当然这个图片必须放在Gizmos文件夹下面。

void
OnDrawGizmos()
{

        Gizmos.DrawIcon(transform.position,
"0.png",
true);

    }

如果只想挂在某个游戏对象身上，那么在Inspecotr里面就可以直接设置。
这里还是要说说OnDrawGizmos()方法，只要脚本继承了MonoBehaviour后，并且在编辑模式下就会每一帧都执行它。发布的游戏肯定就不会执行了，它只能用于在scene视图中绘制一些小物件。比如要做摄像机轨迹，那么肯定是要在Scene视图中做一个预览的线，那么用Gizmos.DrawLine 和Gizmos.DrawFrustum就再好不过了。