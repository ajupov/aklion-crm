using System;

namespace Crm.Exceptions
{
    public class ObjectIsDeletedException : Exception
    {
        private const string ErrorMessage = "Текущий объект удалён";

        public ObjectIsDeletedException() : base(ErrorMessage)
        {
        }
    }
}