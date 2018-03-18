using System;

namespace Crm.Exceptions
{
    public class StoreIsDeletedException : Exception
    {
        private const string ErrorMessage = "Текущий магазин удалён";

        public StoreIsDeletedException() : base(ErrorMessage)
        {
        }
    }
}