using System;

namespace Evolution.Model
{
    public class Date : IDate
    {
        private DateTime _InstanceDate;

        public Date()
        {
            _InstanceDate = DateTime.Now;
        }

        public override string ToString()
        {
            return _InstanceDate.ToString("yyyyMMddHHmmss");
        }
    }
}
