using System;

namespace ReWork.Logic.Infustructure
{
    public class MessageWrapper<T> where T : new()
    {
        private DateTime _dateNextSending;
        private int _attemptsCount;

        public MessageWrapper(T item)
        {
            Data = item;
            _dateNextSending = DateTime.Now;
        }

        public T Data { get; set; }

        public MessageStatus Status { get; set; }

        public int AttemptsCount {
            get
            {
                return _attemptsCount;
            }

            set
            {
                _attemptsCount = value;

                switch (_attemptsCount)
                {
                    case 1: _dateNextSending = _dateNextSending.AddMinutes(1); break;
                    case 2: _dateNextSending = _dateNextSending.AddMinutes(1); break;
                    case 3: _dateNextSending = _dateNextSending.AddMinutes(5); break;
                    case 4: _dateNextSending = _dateNextSending.AddHours(1); break;
                    case 5: _dateNextSending = _dateNextSending.AddDays(1); break;
                }
            }
        }

        public DateTime DateNextSending
        {
            get
            {
                return _dateNextSending;
            }
        }
    }
}
