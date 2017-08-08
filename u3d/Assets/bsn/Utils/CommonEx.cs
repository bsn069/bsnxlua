using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace NBsn {

public static class CommonEx  
{
	public static string UTF8(this byte[] me)
	{
        string val = System.Text.Encoding.UTF8.GetString(me);
        return val;
	}

	
}  

}