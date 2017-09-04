using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace NBsn {

public class Singleton<TYPE> where TYPE : Singleton<TYPE>, new ()
{
    public static TYPE Instance()
    {
        if (ms_instance == null)
        {
            ms_instance = new TYPE();
            ms_instance.OnNewCreated();
        }

        //Log.Assert(ms_instance != null);
        return ms_instance;
    }

    protected Singleton()
    {
    }

    private Singleton(ref Singleton<TYPE> instance)
    {
    }

    private Singleton(Singleton<TYPE> instance)
    {
    }

    protected virtual void OnNewCreated()
    {
    }

    private static TYPE ms_instance = null;
}

}