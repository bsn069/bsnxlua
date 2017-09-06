using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

namespace NBsn.NContainer {

public class Vector<T> {
    public Vector() {
    }

    public Vector(int capacity) {
        m_data = new T[capacity];
    }

    public T this[int index] {
        get { return m_data[index]; }
        set { m_data[index] = value; }
    }

    public int Size() {
        return m_size; 
    }

	public int Capacity() {
		return m_data.Length;
	}

    public void PushBack(T item) {
        if (m_data == null || m_size == m_data.Length) {
            _Reserve();
        }
        m_data[m_size++] = item;
    }

    public T PopBack() {
        if (m_size == 0) {
            return default(T);
        }
        m_size = m_size - 1;
        T ret = m_data[m_size];
        m_data[m_size] = default(T);
        return ret;
    }

    public void Clear() {
        for (int i = 0; i < m_size; ++i) {
            m_data[i] = default(T);
        }
        m_size = 0;
    }

    public T[] ToArray() {
        Trim();
        return m_data;
    }
	
    public void Trim() {
        if (m_size > 0) {
            if (m_size < m_data.Length) {
                T[] newList = new T[m_size];
                for (int i = 0; i < m_size; ++i) {
                    newList[i] = m_data[i];
                }
                m_data = newList;
            }
        } else {
            m_data = null;
        }
    }

    public void Sort() {
        Array.Sort(m_data, 0, m_size);
    }

    private void _Reserve() {
        int newSize = 32;
        if (m_data != null) {
            newSize = m_data.Length + m_data.Length / 2 + 1;
        }

        T[] newList = new T[newSize];
        if (m_data != null && m_size > 0) {
            m_data.CopyTo(newList, 0);
        }
        m_data = newList;
    }

    public void SetData(byte[] buffer) {
        int structSize = Marshal.SizeOf(typeof(T));
        int count = buffer.Length / structSize;
        if(m_data == null || m_data.Length < count)
            m_data = new T[count];
        GCHandle handle = new GCHandle();
        try {
            handle = GCHandle.Alloc(m_data, GCHandleType.Pinned);
            IntPtr pointer = handle.AddrOfPinnedObject();
            Marshal.Copy(buffer, 0, pointer, buffer.Length);
            m_size = count;
        } catch (Exception e) {
            Debug.Log(e.Message);
        } finally {
            if (handle.IsAllocated) handle.Free();
        }
    }

    public byte[] GetData() {
        GCHandle handle = new GCHandle();
        try {
            handle = GCHandle.Alloc(m_data, GCHandleType.Pinned);
            IntPtr pointer = handle.AddrOfPinnedObject();
            byte[] destination = new byte[m_data.Length * Marshal.SizeOf(typeof(T))];
            Marshal.Copy(pointer, destination, 0, destination.Length);
            return destination;
        } catch (Exception e) {
            Debug.Log(e.Message);
        } finally {
            if (handle.IsAllocated) handle.Free();
        }
        return null;

    }

    private T[] m_data = null;
    private int m_size = 0;
}


}