可以在根目录下，也可以在子目录里，只要名子叫Resources就可以 包括在Assets目录下所有名叫Resources的目录
主要用来存放需要在运行时加载的资源，也没有限制不能存放非运行时加载的资源，但是对它其中的资源，不管有没有被设置过的场景说引用，都会被添加到Build中

Unity不会自动清理该目录下未使用的资源，需要我们自行维护，尤其是在最终的Build过程中，Unity对该目录下的资源的处置过程可以说操作绝不简单，具体参考官方文档Loading Resources at Runtime这一章节，操作越复杂自然意味着要花更高的成本，所以在想把资源放到该目录的时候，一定要慎重考虑

比如目录：/xxx/xxx/Resources  和 /Resources 是一样的，无论多少个叫Resources的文件夹都可以
Resources文件夹下的资源不管你用还是不用都会被打包进.apk或者.ipa

Resource.Load ：编辑时和运行时都可以通过Resource.Load来直接读取。

Resources.LoadAssetAtPath() ：它可以读取Assets目录下的任意文件夹下的资源，它可以在编辑时或者编辑器运行时用，它但是它不能在真机上用，它的路径是”Assets/xx/xx.xxx” 必须是这种路径，并且要带文件的后缀名

我觉得在电脑上开发的时候尽量来用Resource.Load() 或者 Resources.LoadAssetAtPath() ，假如手机上选择一部分资源要打assetbundle，一部分资源Resource.Load().那么在做.apk或者.ipa的时候 现在都是用脚本来自动化打包，在打包之前 可以用AssetDatabase.MoveAsset()把已经打包成assetbundle的原始文件从Resources文件夹下移动出去在打包，这样打出来的运行包就不会包行多余的文件了。打完包以后再把移动出去的文件夹移动回来。