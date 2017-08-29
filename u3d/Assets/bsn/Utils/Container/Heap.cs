using System;
using System.Collections;
using System.Collections.Generic;

namespace NBsn.NContainer {

public class Heap<T> where T : IComparable<T> {
    private Vector<T> m_heap = null;

    public Heap() : this(4) {
    }

    public Heap(int caption) {
        m_heap = new Vector<T>(caption);
    }

    public int Size() {
        return m_heap.Size();
    }

	public int Capacity() {
		return m_heap.Capacity();
	}

    public void Push(T item) {
        m_heap.PushBack(item);
        BubbleUp(Size() - 1);
    }

    public T Top() {
        if (Size() == 0) {
            throw new InvalidOperationException("[Heap] Heap is empty");
        }
        return m_heap[0];
    }

    public T Pop() {
        if (Size() == 0) {
            throw new InvalidOperationException("[Heap] Heap is empty");
        }
        T ret = m_heap[0];
        Swap(Size() - 1, 0);
        m_heap.PopBack();

        BubbleDown(0);
        return ret;
    }

    private void BubbleUp(int i) {
        if (i == 0 || Dominates(m_heap[Parent(i)], m_heap[i]))
            return; //correct domination (or root)

        Swap(i, Parent(i));
        BubbleUp(Parent(i));
    }

    private void BubbleDown(int i) {
        int dominatingNode = Dominating(i);
        if (dominatingNode == i) return;
        Swap(i, dominatingNode);
        BubbleDown(dominatingNode);
    }

    private int Dominating(int i) {
        int dominatingNode = i;
        dominatingNode = GetDominating(Left(i), dominatingNode);
        dominatingNode = GetDominating(Right(i), dominatingNode);

        return dominatingNode;
    }

    protected bool Dominates(T x, T y) {
        return x.CompareTo(y) <= 0;
    }

    private int GetDominating(int newNode, int dominatingNode) {
        if (newNode < m_heap.Count && !Dominates(m_heap[dominatingNode], m_heap[newNode]))
            return newNode;
        else
            return dominatingNode;
    }

    private void Swap(int i, int j) {
        T tmp = m_heap[i];
        m_heap[i] = m_heap[j];
        m_heap[j] = tmp;
    }

    private static int Parent(int i) {
        return ((i + 1) >> 1) - 1;
    }

    private static int Left(int i) {
        return (i << 1) + 1;
    }

    private static int Right(int i) {
        return (i << 1) + 2;
    }
}


}