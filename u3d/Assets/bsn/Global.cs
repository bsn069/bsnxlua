using UnityEngine;
using System.Collections;
// using LuaInterface;
using System.Runtime.InteropServices;
using System;

namespace NBsn {
    // [Reg2LuaAttribute]
    public class CGlobal : IDisposable {
        public static NBsn.CGlobal Instance {
            get { 
                #if UNITY_EDITOR
                if (m_instance == null) {
                    new CGlobal();
                }
                #endif
                return m_instance; 
            }
        }

        public NBsn.CLog Log {
            get { return m_Log; }
        }

		public NBsn.MMain Main {
            get { return m_Main; }
        }

        public UnityEngine.GameObject GoMain {
            get { return m_goMain; }
        }

        public UnityEngine.Transform TfMain {
            get { return m_tfMain; }
        }

        public void ConfigInit( ) {
            Log.Info("NBsn.CGlobal.ConfigInit()"); 
        }

        #region game init
        // 游戏逻辑初始化
        public void AppInit(GameObject goMain, NBsn.MMain Main) {
            Log.Info("NBsn.CGlobal.AppInit()"); 

            m_goMain    = goMain;
            m_Main      = Main;
            m_tfMain    = m_goMain.transform;
        }

        public void AppUnInit() {
            Log.Info("NBsn.CGlobal.AppUnInit()"); 

            m_tfMain    = null;
            m_Main      = null;
            m_goMain    = null;
        }
        #endregion

        public void StartApp() {
            Log.Info("NBsn.CGlobal.StartApp()"); 
        }

        #region
        public CGlobal() {
            m_instance = this;
        }
        public void Dispose() {
            Log.Info("NBsn.CGlobal.Dispose()"); 
            m_instance = null;
        }

        protected static CGlobal m_instance = null;

        protected NBsn.CLog m_Log = new NBsn.CLog();

        protected NBsn.MMain m_Main = null;
        protected GameObject m_goMain = null;
        protected Transform m_tfMain = null;
        #endregion
    }
}