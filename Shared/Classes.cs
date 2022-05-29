using System;

namespace Shared
{
    public delegate void RemoteEventHandler(RemoteEventArg arg);

    public class SomeServer : MarshalByRefObject
    {
        public string Status;
        public SomeServer() => Status = "Active";

        public event RemoteEventHandler RemoteEvent;

        public void RaiseSomeClassEvent()
        {
            RemoteEventArg arg = new RemoteEventArg();
            RemoteEvent(arg);
        }

        public void MultiplyBy42(RemoteEventArg arg) => arg.num *= 42;
        public void MultiplyBy1337(RemoteEventArg arg) => arg.num *= 1337;
        public void Add42(RemoteEventArg arg) => arg.num += 42;
    }

    public class RemoteEventArg : MarshalByRefObject
    {
        public int num = 1;
    }

    public class AnotherServer : MarshalByRefObject
    {
        public string Status;

        public AnotherServer() => Status= "Active";
    }
}
