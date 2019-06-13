using System;

namespace ReWork.Logic.Infustructure
{
    public class MessageWrapper<T> where T : new()
    {
        public MessageWrapper(T item)
        {
            Data = item;
        }

        public T Data { get; set; }

        public MessageStatus Status { get; set; }

        public int AttemptsCount { get; set; }

        public DateTime DateNextSending {
            get
            {
                DateTime date = DateTime.Now;
                switch (AttemptsCount)
                {
                    case 1: date = date.AddMinutes(1);break;
                    case 2: date = date.AddMinutes(1); break;
                    case 3: date = date.AddMinutes(5); break;
                    case 4: date = date.AddHours(1); break;
                    case 5: date = date.AddDays(1); break;
                }

                return date;
            }
        }
    }
}
