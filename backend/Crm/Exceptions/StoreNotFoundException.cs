using System;

namespace Crm.Exceptions
{
    public class StoreNotFoundException : Exception
    {
        private const string ErrorMessage = "Текущий магазин не найден";

        public StoreNotFoundException() : base(ErrorMessage)
        {
        }
    }
}