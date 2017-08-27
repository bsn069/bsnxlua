
namespace NBsn.NContainer {


public class List<T> {
    private const int INVALID_HEAD = -1;

    public List(int capacity) {
        m_prev = new Vector<int>(capacity);
        m_next = new Vector<int>(capacity);
        m_value = new Vector<T>(capacity);
        m_freeList = new Vector<int>(4);

        _RequestFirstNew(default(T));
        m_count = 0;
    }

    public int Size() {
        return m_count;
    }

	public int Capacity() {
		return m_value.Capacity();
	}

    public int PushFront(T value) {
        int nid = _RequestFirstNew(value);
        _InsertLast(m_prev[0], nid);
        return nid;
    }

    public int PushBack(T value) {
        int nid = _RequestFirstNew(value);
        _InsertLast(0, nid);
        return nid;
    }

    public bool Contains(T value) {
        for (int h = m_next[0]; h != 0; h = m_next[h]) {
            if (value.Equals(m_value[h])) return true;
        }
        return false;
    }

    public void Remove(int id) {
        if (id == INVALID_HEAD || id >= m_value.Size()) {
            return;
        }
        m_next[m_prev[id]] = m_next[id];
        m_prev[m_next[id]] = m_prev[id];
        m_next[id] = m_prev[id] = id;
        m_value[id] = default(T);
        m_freeList.PushBack(id);
        --m_count;
    }

    public T GetValue(int id) {
        if (id == INVALID_HEAD || id >= m_value.Size()) {
            return default(T);
        }
        return m_value[id];
    }

    public int First {
        get { return m_next[0] == 0 ? INVALID_HEAD : m_next[0]; }
    }

    public int Last {
        get { return m_prev[0] == 0 ? INVALID_HEAD : m_prev[0]; }
    }

    public T FirstValue {
        get { return m_next[0] == 0 ? default(T) : m_value[m_next[0]]; }
    }

    public T LastValue {
        get { return m_prev[0] == 0 ? default(T) : m_value[m_prev[0]]; }
    }



    private void _InsertLast(int oid, int nid) {
        m_prev[nid] = oid;
        m_next[nid] = m_next[oid];
        m_next[oid] = nid;
        m_prev[m_next[nid]] = nid;
        ++m_count;
    }

    private int _RequestFirstNew(T value) {
        int id = INVALID_HEAD;
        if (m_freeList.Size() > 0) {
            id = m_freeList.PopBack();
            m_next[id] = id;
            m_prev[id] = id;
            m_value[id] = value;
        } else {
            id = m_value.Size();
            m_next.PushBack(id);
            m_prev.PushBack(id);
            m_value.PushBack(value);
        }

        return id;
    }

    private int m_count;
    private Vector<int> m_freeList  = null;
    private Vector<int> m_prev      = null;
    private Vector<int> m_next      = null;
    private Vector<T>   m_value     = null;
}

}