using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;

public class C_EventMgr 
{
    public bool Add(int nEventId, Action pAction)
    {
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
                    "nEventId={0} pAction={1} pDelegate={2} type error"
                    , nEventId
                    , pAction.GetType().Name
                    , pDelegate.GetType().Name
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

    public bool Add<P1>(int nEventId, Action<P1> pAction)
    {
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
                    "nEventId={0} pAction={1} pDelegate={2} type error"
                    , nEventId
                    , pAction.GetType().Name
                    , pDelegate.GetType().Name
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

    public bool Add<P1, P2>(int nEventId, Action<P1, P2> pAction)
    {
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
                    "nEventId={0} pAction={1} pDelegate={2} type error"
                    , nEventId
                    , pAction.GetType().Name
                    , pDelegate.GetType().Name
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

    public bool Add<P1, P2, P3>(int nEventId, Action<P1, P2, P3> pAction)
    {
        if (pAction == null) 
        {
#if UNITY_EDITOR
            Debug.LogErrorFormat(
                "nEventId={0} P1={1}, P2={2}, P3={3} pAction=null"
                , nEventId
                , typeof(P1).GetType().Name
                , typeof(P2).GetType().Name
                , typeof(P3).GetType().Name
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
                    "nEventId={0} pAction={1} pDelegate={2} type error"
                    , nEventId
                    , pAction.GetType().Name
                    , pDelegate.GetType().Name
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
                    "nEventId={0} pDelegate={1} is null p1={2} p2={3} p3={4}"
                    , nEventId
                    , pDelegate.GetType().Name
                    , p1
                    , p2
                    , p3
                );
                return;
            }
        }
#endif

        var pDelegateAction = (Action<P1, P2, P3>)pDelegate;
        pDelegateAction(p1, p2, p3);
    }


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

        Add<int, string, byte>(32, null);
        Del<int, string, byte>(32, null);
    }

    private void P3_1(int i, string j, uint k)
    {
        Debug.LogFormat("exec P3_1 {0} {1} {2}", i, j, k);
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

    Dictionary<int, Delegate> m_id2Delegate = new Dictionary<int, Delegate>();
}