using System;
using UnityEngine;
using System.Collections;
using System;

namespace NBsn.NContainer {

public interface I_Key<TKey>
{
	TKey GetKey();
}


// 线性存储的表
public class Table<TKey, TValue>
    where TKey : IComparable<TKey>
    where TValue : IComparable<TValue>, I_Key<TKey>
{
    public Table(){
        m_list = new Vector<TValue>();
    }

    public Table(int capacity) {
        m_list = new Vector<TValue>(capacity);
    }

    public int Size() {
        return m_list.Size();
    }

	public int Capacity() {
        return m_list.Capacity();
    }

    public bool Add(TKey key, TValue value) {
#if UNITY_EDITOR
        if (value.GetKey().CompareTo(key) != 0) {
            Debug.LogError("Add Value With different key.");
            return false;
        }
#endif
        if (m_bSorted) {
            if (m_list.Size() > 0 && m_list[m_list.Size() - 1].CompareTo(value) > 0) {
                m_bSorted = false;
            }
        }

        m_list.PushBack(value);

        return true;
    }

    public bool TryGetValue(TKey key, out TValue ret) {
        if (!m_bSorted) {
            Sort();
        }

        int pos = LowerBounder(key);
        if (pos < m_list.Size() && m_list[pos].GetKey().CompareTo(key) == 0) {
            ret = m_list[pos];
            return true;
        }

        ret = default(TValue);
        return false;
    }

    public void Clear() {
        m_list.Clear();
        m_bSorted = true;
    }

    public bool SetBuffer(byte[] buffer) {
        m_list.SetData(buffer);
        return true;
    }

    public byte[] GetBuffer() {
        return m_list.GetData();
    }

    public bool Contains(TKey key) {
        if (!m_bSorted) {
            Sort();
        }

        int pos = LowerBounder(key);
        if (pos < m_list.Size() && m_list[pos].GetKey().CompareTo(key) == 0) {
            return true;
        }

        return false;
    }

    public TValue Get(int index) {
#if UNITY_EDITOR
        if (index < 0 || index >= m_list.Size()) {
            Debug.LogError("[Table] Argument out of Index.");
            return default(TValue);
        }
#endif
        return m_list[index];
    }
	
    public void Sort() {
        TValue[] data = m_list.ToArray();
        if (data != null && !m_bSorted) {
            Array.Sort<TValue>(data);
        }
        m_bSorted = true;
    }


    public bool CheckData() {
#if UNITY_EDITOR
        for (int i = 1; i < m_list.Size(); ++i) {
            if (m_list[i - 1].CompareTo(m_list[i]) == 0) {
                Debug.LogErrorFormat("[Table] Key={0},Value={1} Has SameKey.", typeof(TKey), typeof(TValue));
                return false;
            }
        }
#endif
        return true;
    }

    public int LowerBounder(TKey key) {
        int low = 0, high = m_list.Size();

        while (low < high) {
            int mid = (low + high) >> 1;
            if (m_list[mid].GetKey().CompareTo(key) < 0) {
                low = mid + 1;
            } else {
                high = mid;
            }
        }

        return low;
    }

    private bool m_bSorted = true;
    private Vector<TValue> m_list = null;
}


}