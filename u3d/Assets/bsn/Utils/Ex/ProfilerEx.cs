using UnityEngine;
using UnityEngine.Profiling;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace NBsn
{
    public static class ProfilerEx
    {
        private static bool          m_enabled   = true;
        private static Stack<string> m_nameStack = new Stack<string>();

        private static long m_timeStampBegin      = 0;
        private static long m_timeStampEnd        = 0;
        private static Stack<long> m_timeStampStack = new Stack<long>();

        public static bool enabled
        {
            get { return m_enabled; }
            set {
                if (m_enabled == value)
                    return;

                m_enabled = value;
                _OnEnableChanged();
            }
        }

        [Conditional("ENABLE_PROFILER")]
        public static void BeginSample(string name)
        {
            if (!enabled)
                return;

            Profiler.BeginSample(name);
            m_nameStack.Push(name);

            long tsBegin = Stopwatch.GetTimestamp();
            m_timeStampStack.Push(tsBegin);
        }

        // Ignore checking matching for BeginSample and EndSample if name is null
        [Conditional("ENABLE_PROFILER")]
        public static void EndSample(string name = null)
        {
            if (!enabled)
                return;

            if (name != null)
            {
                string top = m_nameStack.Count > 0 ? m_nameStack.Peek() : null;
                if (top == null || !top.Equals(name))
                {                    
                    UnityEngine.Debug.LogErrorFormat("BeginSample({0}) and EndSample({1}) don't match", top, name);
                    return;
                }
            }

            m_nameStack.Pop();
            Profiler.EndSample();

            m_timeStampBegin = m_timeStampStack.Pop();
            m_timeStampEnd = Stopwatch.GetTimestamp();
        }

        // 返回当前sample耗时，单位ms
        // 此函数在EndSampe后调用才有效
        public static float CurrentSampleCostTime() {
            if (m_timeStampEnd <= m_timeStampBegin) {
                return 0;
            }
            long timeStampCost = m_timeStampEnd - m_timeStampBegin;
            float ms = (float)(timeStampCost / (float)TimeSpan.TicksPerMillisecond);
            return ms;
        }

        private static void _OnEnableChanged()
        {
            m_nameStack.Clear();
        }
    }
}
