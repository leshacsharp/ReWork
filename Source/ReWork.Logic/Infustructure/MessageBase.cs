using System;

namespace ReWork.Logic.Infustructure
{
    public abstract class MessageBase
    {
        public MessageBase()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public string UserId { get; set; }
    }
}
