这个文件夹下的资源也会全都打包在.apk或者.ipa
它和Resources的区别是，Resources会压缩文件，但是它不会压缩原封不动的打包进去
它是一个只读的文件夹，就是程序运行时只能读 不能写
在各个平台下的路径是不同的，不过你可以用Application.streamingAssetsPath 它会根据当前的平台选择对应的路径

有些游戏为了让所有的资源全部使用assetbundle，
会把一些初始的assetbundle放在StreamingAssets目录下，
运行程序的时候在把这些assetbundle拷贝在Application.persistentDataPath目录下，
如果这些assetbundle有更新的话，
那么下载到新的assetbundle在把Application.persistentDataPath目录下原有的覆盖掉。

因为Application.persistentDataPath目录是应用程序的沙盒目录，所以打包之前是没有这个目录的，直到应用程序在手机上安装完毕才有这个目录。

StreamingAssets目录下的资源都是不压缩的，所以它比较大会占空间，
比如你的应用装在手机上会占用100M的容量，那么你又在StreamingAssets放了一个100M的assetbundle，
那么此时在装在手机上就会在200M的容量。


Streaming Assets主要存放不想在Build过程中改变格式的文件，
因为其他目录下的文件在Build之后，都会被打包成为一种Unity播放器专用的资源文件，
利用这个目录，再结合Assets Bundle的资源加载机制，可以用来实现，
让Unity客户端程序从标准开发环境，快速切换到目标平台的真机调试环境中去

- iOS : Application.dataPath + /Raw
- Android : jar:file:// + Application.dataPath + !/assets/