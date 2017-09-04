using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;

namespace NBsn {

public class C_EventMgr 
{
    public static C_EventMgr Instance 
	{
		get { return m_instance; }
	}

    #region p0
    public bool Add(int nEventId, Action pAction)
    {
        if (pAction == null) 
        {
#if UNITY_EDITOR
            Debug.LogErrorFormat(
                "Add(nEventId={0}, pAction=null)"
                , nEventId
            );
#endif
            return false;
        }

        Delegate pDelegate;
        if (!m_id2Delegate.TryGetValue(nEventId, out pDelegate)) 
        {
            m_id2Delegate.Add(nEventId, pDelegate);
        }
#if UNITY_EDITOR
        else 
        {
            if (pDelegate.GetType() != pAction.GetType())
            {
                Debug.LogErrorFormat(
                    "Add(nEventId={0}, pAction) pDelegate={1} not match"
                    , nEventId
                    , pDelegate.GetType()
                );
                return false;
            }
        }
#endif

        m_id2Delegate[nEventId] = (Action)pDelegate + pAction;
        return true;
    }

    public bool Del(int nEventId, Action pAction)
    {
        if (pAction == null) 
        {
#if UNITY_EDITOR
            Debug.LogErrorFormat(
                "Add(nEventId={0}, pAction=null)"
                , nEventId
            );
#endif
            return false;
        }

        Delegate pDelegate;
        if (!m_id2Delegate.TryGetValue(nEventId, out pDelegate)) 
        {
#if UNITY_EDITOR
            Debug.LogErrorFormat(
                "nEventId={0} pAction={1} not found"
                , nEventId
                , pAction.GetType().Name
            );
#endif
            return false;
        }
#if UNITY_EDITOR
        else 
        {
            if (pDelegate.GetType() != pAction.GetType())
            {
                Debug.LogErrorFormat(
                    "nEventId={0} pAction={1} pDelegate={2} type error"
                    , nEventId
                    , pAction.GetType().Name
                    , pDelegate.GetType().Name
                );
                return false;
            }
        }
#endif

        var pDelegateAction = (Action)pDelegate;
        pDelegateAction -= pAction;
        if (pDelegateAction == null) {
            m_id2Delegate.Remove(nEventId);
        }
        else 
        {
            m_id2Delegate[nEventId] = pDelegateAction;
        }
        return true;
    }

    public void Exec(int nEventId)
    {
        Delegate pDelegate;
        if (!m_id2Delegate.TryGetValue(nEventId, out pDelegate)) 
        {
            return;
        }
#if UNITY_EDITOR
        else 
        {
            if (pDelegate == null)
            {
                Debug.LogErrorFormat(
                    "nEventId={0} pDelegate is null"
                    , nEventId
                );
                return;
            }
        }
#endif

        var pDelegateAction = (Action)pDelegate;
        pDelegateAction();
    }
    #endregion

    #region p1
    public bool Add<P1>(int nEventId, Action<P1> pAction)
    {
        if (pAction == null) 
        {
#if UNITY_EDITOR
            Debug.LogErrorFormat(
                "Add<P1={1}>(nEventId={0}, pAction=null)"
                , nEventId
                , typeof(P1)
            );
#endif
            return false;
        }

        Delegate pDelegate;
        if (!m_id2Delegate.TryGetValue(nEventId, out pDelegate)) 
        {
            m_id2Delegate.Add(nEventId, pDelegate);
        }
#if UNITY_EDITOR
        else 
        {
            if (pDelegate.GetType() != pAction.GetType())
            {
                Debug.LogErrorFormat(
                    "Add<P1={2}>(nEventId={0}, pAction) pDelegate={1} not match"
                    , nEventId
                    , pDelegate.GetType()
                    , typeof(P1)
                );
                return false;
            }
        }
#endif

        m_id2Delegate[nEventId] = (Action<P1>)pDelegate + pAction;
        return true;
    }

    public bool Del<P1>(int nEventId, Action<P1> pAction)
    {
        if (pAction == null) 
        {
#if UNITY_EDITOR
            Debug.LogErrorFormat(
                "Add<P1={1}>(nEventId={0}, pAction=null)"
                , nEventId
                , typeof(P1)
            );
#endif
            return false;
        }

        Delegate pDelegate;
        if (!m_id2Delegate.TryGetValue(nEventId, out pDelegate)) 
        {
#if UNITY_EDITOR
            Debug.LogErrorFormat(
                "nEventId={0} pAction={1} not found"
                , nEventId
                , pAction.GetType().Name
            );
#endif
            return false;
        }
#if UNITY_EDITOR
        else 
        {
            if (pDelegate.GetType() != pAction.GetType())
            {
                Debug.LogErrorFormat(
                    "nEventId={0} pAction={1} pDelegate={2} type error"
                    , nEventId
                    , pAction.GetType().Name
                    , pDelegate.GetType().Name
                );
                return false;
            }
        }
#endif

        var pDelegateAction = (Action<P1>)pDelegate;
        pDelegateAction -= pAction;
        if (pDelegateAction == null)
        {
            m_id2Delegate.Remove(nEventId);
        }
        else 
        {
            m_id2Delegate[nEventId] = pDelegateAction;
        }
        return true;
    }

    public void Exec<P1>(int nEventId, P1 p1)
    {
        Delegate pDelegate;
        if (!m_id2Delegate.TryGetValue(nEventId, out pDelegate)) 
        {
            return;
        }
#if UNITY_EDITOR
        else 
        {
            if (pDelegate == null)
            {
                Debug.LogErrorFormat(
                    "nEventId={0} pDelegate={1} is null p1={2}"
                    , nEventId
                    , pDelegate.GetType().Name
                    , p1
                );
                return;
            }
        }
#endif

        var pDelegateAction = (Action<P1>)pDelegate;
        pDelegateAction(p1);
    }
    #endregion

    #region p2
    public bool Add<P1, P2>(int nEventId, Action<P1, P2> pAction)
    {
        if (pAction == null) 
        {
#if UNITY_EDITOR
            Debug.LogErrorFormat(
                "Add<P1={1}, P2={2}>(nEventId={0}, pAction=null)"
                , nEventId
                , typeof(P1)
                , typeof(P2)
            );
#endif
            return false;
        }

        Delegate pDelegate;
        if (!m_id2Delegate.TryGetValue(nEventId, out pDelegate)) 
        {
            m_id2Delegate.Add(nEventId, pDelegate);
        }
#if UNITY_EDITOR
        else 
        {
            if (pDelegate.GetType() != pAction.GetType())
            {
                Debug.LogErrorFormat(
                    "Add<P1={2}, P2={3}>(nEventId={0}, pAction) pDelegate={1} not match"
                    , nEventId
                    , pDelegate.GetType()
                    , typeof(P1)
                    , typeof(P2)
                );
                return false;
            }
        }
#endif

        m_id2Delegate[nEventId] = (Action<P1, P2>)pDelegate + pAction;
        return true;
    }

    public bool Del<P1, P2>(int nEventId, Action<P1, P2> pAction)
    {
        if (pAction == null) 
        {
#if UNITY_EDITOR
            Debug.LogErrorFormat(
                "Add<P1={1}, P2={2}>(nEventId={0}, pAction=null)"
                , nEventId
                , typeof(P1)
                , typeof(P2)
            );
#endif
            return false;
        }

        Delegate pDelegate;
        if (!m_id2Delegate.TryGetValue(nEventId, out pDelegate)) 
        {
#if UNITY_EDITOR
            Debug.LogErrorFormat(
                "nEventId={0} pAction={1} not found"
                , nEventId
                , pAction.GetType().Name
            );
#endif
            return false;
        }
#if UNITY_EDITOR
        else 
        {
            if (pDelegate.GetType() != pAction.GetType())
            {
                Debug.LogErrorFormat(
                    "nEventId={0} pAction={1} pDelegate={2} type error"
                    , nEventId
                    , pAction.GetType().Name
                    , pDelegate.GetType().Name
                );
                return false;
            }
        }
#endif

        var pDelegateAction = (Action<P1, P2>)pDelegate;
        pDelegateAction -= pAction;
        if (pDelegateAction == null)
        {
            m_id2Delegate.Remove(nEventId);
        }
        else 
        {
            m_id2Delegate[nEventId] = pDelegateAction;
        }
        return true;
    }

    public void Exec<P1, P2>(int nEventId, P1 p1, P2 p2)
    {
        Delegate pDelegate;
        if (!m_id2Delegate.TryGetValue(nEventId, out pDelegate)) 
        {
            return;
        }
#if UNITY_EDITOR
        else 
        {
            if (pDelegate == null)
            {
                Debug.LogErrorFormat(
                    "nEventId={0} pDelegate={1} is null p1={2} p2={3}"
                    , nEventId
                    , pDelegate.GetType().Name
                    , p1
                    , p2
                );
                return;
            }
        }
#endif

        var pDelegateAction = (Action<P1, P2>)pDelegate;
        pDelegateAction(p1, p2);
    }
    #endregion

    #region p3
    public bool Add<P1, P2, P3>(int nEventId, Action<P1, P2, P3> pAction)
    {
        if (pAction == null) 
        {
#if UNITY_EDITOR
            Debug.LogErrorFormat(
                "Add<P1={1}, P2={2}, P3={3}>(nEventId={0}, pAction=null)"
                , nEventId
                , typeof(P1)
                , typeof(P2)
                , typeof(P3)
            );
#endif
            return false;
        }

        Delegate pDelegate;
        if (!m_id2Delegate.TryGetValue(nEventId, out pDelegate)) 
        {
            m_id2Delegate.Add(nEventId, pDelegate);
        }
#if UNITY_EDITOR
        else 
        {
            if (pDelegate.GetType() != pAction.GetType())
            {
                Debug.LogErrorFormat(
                    "Add<P1={2}, P2={3}, P3={4}>(nEventId={0}, pAction) pDelegate={1} not match"
                    , nEventId
                    , pDelegate.GetType()
                    , typeof(P1)
                    , typeof(P2)
                    , typeof(P3)
                );
                return false;
            }
        }
#endif

        m_id2Delegate[nEventId] = (Action<P1, P2, P3>)pDelegate + pAction;
        return true;
    }

    public bool Del<P1, P2, P3>(int nEventId, Action<P1, P2, P3> pAction)
    {
        if (pAction == null) 
        {
#if UNITY_EDITOR
            Debug.LogErrorFormat(
                "Del<P1={1}, P2={2}, P3={3}>(nEventId={0}, pAction=null)"
                , nEventId
                , typeof(P1)
                , typeof(P2)
                , typeof(P3)
            );
#endif
            return false;
        }

        Delegate pDelegate;
        if (!m_id2Delegate.TryGetValue(nEventId, out pDelegate)) 
        {
#if UNITY_EDITOR
            Debug.LogErrorFormat(
                "nEventId={0} pAction={1} not found"
                , nEventId
                , pAction.GetType().Name
            );
#endif
            return false;
        }
#if UNITY_EDITOR
        else 
        {
            if (pDelegate.GetType() != pAction.GetType())
            {
                Debug.LogErrorFormat(
                    "nEventId={0} pAction={1} pDelegate={2} type error"
                    , nEventId
                    , pAction.GetType().Name
                    , pDelegate.GetType().Name
                );
                return false;
            }
        }
#endif

        var pDelegateAction = (Action<P1, P2, P3>)pDelegate;
        pDelegateAction -= pAction;
        if (pDelegateAction == null)
        {
            m_id2Delegate.Remove(nEventId);
        }
        else 
        {
            m_id2Delegate[nEventId] = pDelegateAction;
        }
        return true;
    }

    public void Exec<P1, P2, P3>(int nEventId, P1 p1, P2 p2, P3 p3)
    {
        Delegate pDelegate;
        if (!m_id2Delegate.TryGetValue(nEventId, out pDelegate)) 
        {
            return;
        }
#if UNITY_EDITOR
        else 
        {
            if (pDelegate == null)
            {
                Debug.LogErrorFormat(
                    "Exec<P1={1}, P2={2}, P3={3}>(nEventId={0}, p1={4}, p2={5}, p3={6}) pDelegate == null"
                    , nEventId
                    , typeof(P1)
                    , typeof(P2)
                    , typeof(P3)
                    , p1
                    , p2
                    , p3
                );
                return;
            }
        }
#endif

        var pDelegateAction = (Action<P1, P2, P3>)pDelegate;
        if (pDelegateAction == null) {
#if UNITY_EDITOR
            Debug.LogErrorFormat(
                "Exec<P1={2}, P2={4}, P3={6}>(nEventId={0}, p1={3}, p2={5}, p3={7}) pDelegate == {1}"
                , nEventId
                , pDelegate.GetType()
                , typeof(P1)
                , p1
                , typeof(P2)
                , p2
                , typeof(P3)
                , p3
            );
#endif
            return;
        }

        pDelegateAction(p1, p2, p3);
    }
    #endregion

    #region test
#if UNITY_EDITOR
    [UnityEditor.MenuItem("Bsn/Test/EventMgr")]
    public static void Test()
    {
        C_EventMgr p = new C_EventMgr();
        p.TestP0();
        p.TestP1();
        p.TestP2();
        p.TestP3();
    }

    private void TestP3()
    {
        Add<int, string, uint>(31, P3_1);
        Del<int, string, uint>(31, P3_1);

        Add<int, string, uint>(31, P3_1);
        Add<int, string, uint>(31, P3_1);
        Del<int, string, uint>(31, P3_1);
        Del<int, string, uint>(31, P3_1);

        Add<int, string, byte>(32, null);
        Del<int, string, byte>(32, null);

        Add<int, string, uint>(33, P3_1);
        Add<string, int, uint>(33, P3_2);
    }

    private void P3_1(int i, string j, uint k)
    {
        Debug.LogFormat("exec P3_1 {0} {1} {2}", i, j, k);
    }

    private void P3_2(string i, int j, uint k)
    {
        Debug.LogFormat("exec P3_2 {0} {1} {2}", i, j, k);
    }

    private void TestP2()
    {
        Exec<int, string>(4, 1, "1");
        Add<int, string>(4, P2_1);
        Exec<int, string>(4, 2, "2");
        Del<int, string>(4, P2_1);
        Exec<int, string>(4, 3, "3");

        Exec<string, int>(5, "1", 1);
        Add<string, int>(5, P2_2);
        Exec<string, int>(5, "2", 2);
        Del<string, int>(5, P2_2);
        Exec<string, int>(5, "3", 3);
    }

    private void P2_1(int i, string j)
    {
        Debug.LogFormat("exec P2_1{0} {1}", i, j);
    }

    private void P2_2(string i, int j)
    {
        Debug.LogFormat("exec P2_2{0} {1}", i, j);
    }

    private void TestP1()
    {
        Exec<int>(2, 1);
        Add<int>(2, P1_1);
        Exec<int>(2, 2);
        Del<int>(2, P1_1);
        Exec<int>(2, 3);

        Exec<string>(3, "1");
        Add<string>(3, P1_2);
        Exec<string>(3, "2");
        Del<string>(3, P1_2);
        Exec<string>(3, "3");
    }

    private void P1_1(int i)
    {
        Debug.LogFormat("exec P1_1{0}", i);
    }

    private void P1_2(string i)
    {
        Debug.LogFormat("exec P1_2{0}", i);
    }

    private void TestP0()
    {
        Exec(1);
        Add(1, P0);
        Exec(1);
        Del(1, P0);
        Exec(1);
    }

    private void P0()
    {
        Debug.LogFormat("exec {0}", "P0");
    }
#endif
    #endregion

    protected static C_EventMgr m_instance = new C_EventMgr();
    Dictionary<int, Delegate> m_id2Delegate = new Dictionary<int, Delegate>();
}

}