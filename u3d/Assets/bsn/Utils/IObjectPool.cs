using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn 
{

public interface I_ObjectPool
{
	bool Put(GameObject go);
	GameObject Get(); 
}

}

