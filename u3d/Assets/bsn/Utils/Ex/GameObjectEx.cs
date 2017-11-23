using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace NBsn 
{

public static class GameObjectEx 
{    
    // Instantiate go then set name
	public static T Clone<T>(this GameObject go) where T : UnityEngine.Object
	{
		var goClone = (GameObject)UnityEngine.Object.Instantiate(go);
		goClone.name = goClone.name.Replace("(Clone)", "");
		return goClone as T;
    }
}  

}