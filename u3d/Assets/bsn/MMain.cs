using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NBsn {
    // [Reg2LuaAttribute]
    public class MMain : MonoBehaviour {
        void Awake() {
            DontDestroyOnLoad(gameObject);  //防止销毁自己
            new NBsn.CGlobal();
            NBsn.CGlobal.Instance.Log.InfoFormat("NBsn.MMain.Awake()"); 
            NBsn.CGlobal.Instance.ConfigInit();
        }

        void Start() {
            NBsn.CGlobal.Instance.Log.InfoFormat("NBsn.MMain.Start()"); 
            NBsn.CGlobal.Instance.AppInit(gameObject, this);
            NBsn.CGlobal.Instance.StartApp();
        }

        void OnDestroy() {
            NBsn.CGlobal.Instance.Log.InfoFormat("NBsn.MMain.OnDestroy()"); 
            NBsn.CGlobal.Instance.AppUnInit();
            NBsn.CGlobal.Instance.Dispose();
        }
    }
}