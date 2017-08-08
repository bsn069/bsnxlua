using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NBsn 
{

public class MMain : MonoBehaviour 
{
	void Awake() 
	{
		DontDestroyOnLoad(gameObject);  //防止销毁自己
		new NBsn.CGlobal();
		NBsn.CGlobal.Instance.Init(gameObject, this);
	}

	void Update()
	{
		NBsn.CGlobal.Instance.Update();
	}

	void LateUpdate()
	{
		NBsn.CGlobal.Instance.LateUpdate();
	}

	void OnDestroy() 
	{
		NBsn.CGlobal.Instance.UnInit();		
		NBsn.CGlobal.Instance.Dispose();		
	}
}

}