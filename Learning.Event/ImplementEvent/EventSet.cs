using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Learning.Event.ImplementEvent
{

    public sealed class EventKey { }
    public sealed class EventSet
    {
        //该私有字典用于维护EventKey -> Delegate映射
        private readonly Dictionary<EventKey, Delegate> m_events = new Dictionary<EventKey, Delegate>();

        //添加EventKey -> Delegate映射
        //将委托和现有的EventKey合并
        public void Add(EventKey eventKey, Delegate handler)
        {
            //获取指定对象的排他锁。
            Monitor.Enter(m_events);
            Delegate d;
            m_events.TryGetValue(eventKey, out d);

            d = Delegate.Combine(d, handler);
            m_events[eventKey] = d;

            Monitor.Exit(m_events);
        }

        public void Remove(EventKey eventKey, Delegate handler)
        {
            //获取指定对象的排他锁。
            Monitor.Enter(m_events);
            Delegate d;
            m_events.TryGetValue(eventKey, out d);

            if (d != null)
            {
                d = Delegate.Remove(d, handler);
                m_events[eventKey] = d;
            }
            else
            {
                m_events.Remove(eventKey);
            }

            Monitor.Exit(m_events);
        }

        //引发指定eventKey的事件
        public void Raise(EventKey eventKey, object sender, EventArgs e)
        {
            Delegate d;
            Monitor.Enter(m_events);
            m_events.TryGetValue(eventKey, out d);
            Monitor.Exit(m_events);
            if (d != null)
            {
                //由于字典可能包含几个不同的委托类型，所以无法在编译时构造一个类型安全的委托调用
                //因此调用System.Delegate 类型的 DynamicInvoke方法，以一个数组的形式向他传递回调方法的参数
                //在内部，DynamicInvoke 方法会调用的回调方法查证参数的类型安全性，并调用方法
                //如果存在类型不匹配的情况，会抛出异常
                d.DynamicInvoke(new object[] { sender, e});
            }
        }
    }
}
