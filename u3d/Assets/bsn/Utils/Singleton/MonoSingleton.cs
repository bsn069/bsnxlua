using System;
using UnityEngine;

namespace NBsn {


public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T ms_instance = null;
    public  static T Instance {
        get {
            return ms_instance;
        }
    }

    protected virtual void Awake()
    {
        if (ms_instance != null)
        {
            Log.Error("[MonoSingleton]Duplicate {0}", ms_instance.GetType().ToString());
            return;
        }
        ms_instance = this as T;
    }

    protected virtual void OnDestroy()
    {
        if (ms_instance == this as T)
            ms_instance = null;
    }
}

}