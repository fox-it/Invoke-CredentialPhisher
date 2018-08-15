using System;
using Windows.Foundation;

namespace Phishwin
{
    public class EventWrapper<TSender, TResult>
    {
        public event EventHandler<EventEventArg> FireEvent;

        public class EventEventArg : EventArgs
        {
            public EventEventArg(TSender sender, TResult result)
            {
                this.sender = sender;
                this.result = result;
            }
            private TSender sender;
            private TResult result;
            public TSender Sender { get { return this.sender; } }
            public TResult Result { get { return this.result; } }
        }

        public TypedEventHandler<TSender, TResult> TypedEventHandler
        {
            get
            {
                return new TypedEventHandler<TSender, TResult>((sender, result) => {
                    this.FireEvent?.Invoke(sender, new EventEventArg(sender, result));
                });
            }
        }

        public EventWrapper<TSender, TResult> Register(object target, string eventName)
        {
            var targetType = target.GetType();
            var methodType = targetType.GetMethod("add_" + eventName);
            methodType.Invoke(target, new[] { this.TypedEventHandler });
            return this;
        }
    }

    class Helper
    {
        public static EventWrapper<TSender, TResult> Register<TSender, TResult>(object target, string eventName)
        {
            var wrapper = new EventWrapper<TSender, TResult>();
            Type.GetTypeHandle(target).GetType().GetMethod("add_" + eventName).Invoke(target, new[] { wrapper });
            return wrapper;
        }
    }
}
