using System;

namespace Tanks3D.Messaging
{
    public interface ISubscription<TMessage> : IDisposable
        where TMessage : IMessage
    {
        Action<TMessage> Action { get; }

        IMessageBus MessageBus { get; }
    }
}