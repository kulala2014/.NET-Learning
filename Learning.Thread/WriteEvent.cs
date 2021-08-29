using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Thread
{
    public static class WriteEvent
    {

        //显示实现event
        public static void Show()
        {
            TryEventFoo fooEvent = new TryEventFoo();
            fooEvent.FooEvent += (object sender, EventArgs e) => Console.WriteLine("handing Foo event here....");
            fooEvent.SimulateFoo();
        }        
    }


    public sealed class EventKey{}

    public sealed class EventSet
    {
        private Dictionary<EventKey, Delegate> events = new Dictionary<EventKey, Delegate>();

        public void Add(EventKey eventKey, Delegate @event)
        {
            Monitor.Enter(events);
            events.TryGetValue(eventKey, out Delegate d);
            events[eventKey] = Delegate.Combine(d, @event);
            Monitor.Exit(events);
        }

        public void Remove(EventKey eventKey, Delegate @event)
        {
            Monitor.Enter(events);
            events.TryGetValue(eventKey, out Delegate d);
            if (d != null)
            {
                d =  Delegate.Remove(d, @event);
            }
            if (d is null)
                events.Remove(eventKey);
            else
                events[eventKey] = d;
            Monitor.Exit(events);
        }

        public void InvokeEvents(EventKey eventKey, object sender, EventArgs e)
        {
            Monitor.Enter(events);
            events.TryGetValue(eventKey, out Delegate d);
            Monitor.Exit(events);
            if (d != null)
            {
                d.DynamicInvoke(new object[]{ sender, e });
            }
        }
    }

    public class TryEventFoo
    {
        private EventSet eventSet = new EventSet();
        private EventKey foo_evetKey = new EventKey();

        public event EventHandler FooEvent
        {
            add { eventSet.Add(foo_evetKey, value); }
            remove { eventSet.Remove(foo_evetKey, value); }
        }

        protected virtual void onFoo(EventArgs e)
        {
            eventSet.InvokeEvents(foo_evetKey, this, e);
        }

        public void SimulateFoo() { onFoo(new EventArgs()); }
    }

}
