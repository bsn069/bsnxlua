using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn 
{

public interface IABLoad
{
	GameObject Load(string strPath, string strSuffix); 
	bool Init();
	void UnInit(); 
}

}

