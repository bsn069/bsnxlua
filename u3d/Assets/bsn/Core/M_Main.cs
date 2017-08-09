using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NBsn 
{

public class M_Main : MonoBehaviour 
{
	void Awake() 
	{
		DontDestroyOnLoad(gameObject);  //防止销毁自己
		new NBsn.C_Global();
		NBsn.C_Global.Instance.Init(gameObject, this);
	}

	void Update()
	{
		NBsn.C_Global.Instance.Update();
	}

	void LateUpdate()
	{
		NBsn.C_Global.Instance.LateUpdate();
	}

	void OnDestroy() 
	{
		NBsn.C_Global.Instance.UnInit();		
		NBsn.C_Global.Instance.Dispose();		
	}
}

}