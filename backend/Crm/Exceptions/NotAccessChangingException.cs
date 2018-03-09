using System;

namespace Crm.Exceptions
{
    public class NotAccessChangingException : Exception
    {
        private const string ErrorMessage = "Этот объект не принадлежит текущему магазину";

        public NotAccessChangingException() : base(ErrorMessage)
        {
        }
    }
}