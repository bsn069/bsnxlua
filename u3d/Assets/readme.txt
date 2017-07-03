Assets为资源总目录
对于非Resources和Streaming Assets以及Plungins下面的部分目录下的资源文件在Build的时候，只有被设置过的场景有引用到，参考Build Settings，才会添加到Build中，对于没有引用到的资源是不会添加到Build中去的
利用这个功能，最大的好处莫过于能自动清理未使用的资源，减少人工资源维护成本，从而提升工作效率
对于不需要在运行时加载的资源，我们都应直接放到该目录下，而不是任何一个Resources目录下


5. Unity常用目录对应的Android && iOS平台地址
IOS:
Application.dataPath : Application/xxxxx/xxx.app/Data
Application.streamingAssetsPath : Application/xxxxx/xxx.app/Data/Raw
Application.persistentDataPath : Application/xxxxx/Documents
Application.temporaryCachePath : Application/xxxxx/Library/Caches


Android:
Application.dataPath : /data/app/xxx.xxx.xxx.apk
Application.streamingAssetsPath : jar:file:///data/app/xxx.xxx.xxx.apk/!/assets
Application.persistentDataPath : /data/data/xxx.xxx.xxx/files
Application.temporaryCachePath : /data/data/xxx.xxx.xxx/cache

Windows:
Application.dataPath :                         /Assets
Application.streamingAssetsPath :      /Assets/StreamingAssets
Application.persistentDataPath :         C:/Users/xxxx/AppData/LocalLow/CompanyName/ProductName
Application.temporaryCachePath :      C:/Users/xxxx/AppData/Local/Temp/CompanyName/ProductName

Mac:
Application.dataPath :                         /Assets
Application.streamingAssetsPath :      /Assets/StreamingAssets
Application.persistentDataPath :         /Users/xxxx/Library/Caches/CompanyName/Product Name
Application.temporaryCachePath :     /var/folders/57/6b4_9w8113x2fsmzx_yhrhvh0000gn/T/CompanyName/Product Name

Application.persistentDataPath
http://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html
Contains the path to a persistent data directory (Read Only).
设备中的公开目录，根据平台的不同而不同。这里面的文件不会因为App升级而删除

Application.streamingAssetsPath
http://docs.unity3d.com/ScriptReference/Application-streamingAssetsPath.html
工程目录下面的Assets/StreamingAssets。

Application.temporaryCachePath
http://docs.unity3d.com/ScriptReference/Application-temporaryCachePath.html
Contains the path to a temporary data / cache directory (Read Only).
设备的临时存储路径。








