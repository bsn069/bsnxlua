using UnityEngine;
using System.Collections;
// using LuaInterface;
// using System.Runtime.InteropServices;
using System;

namespace NBsn {
    // [Reg2LuaAttribute]
    public class CLog {
        public void InfoFormat(string format, params object[] args) {
            Debug.LogFormat(format, args);
        }

        public void ErrorFormat(string format, params object[] args) {
            Debug.LogErrorFormat(format, args);
        }

        public void Info(object message) {
            Debug.Log(message);
        }

        public void Error(object message) {
            Debug.LogError(message);
        }
    }
}