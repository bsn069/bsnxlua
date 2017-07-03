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
		NBsn.CGlobal.Instance.Awake();
	}

	void Start() 
	{
		NBsn.CGlobal.Instance.Start(gameObject, this);
	}

	void Update()
	{
		NBsn.CGlobal.Instance.Update();
	}

	void OnDestroy() 
	{
		NBsn.CGlobal.Instance.OnDestroy();		
	}
}

}