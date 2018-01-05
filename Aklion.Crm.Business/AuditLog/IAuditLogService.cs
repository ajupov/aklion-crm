namespace Aklion.Crm.Business.AuditLog
{
    public interface IAuditLogService
    {
        void LogInserting(int userId, int storeId, object newModel);

        void LogUpdating(int userId, int storeId, object oldModel, object newModel);

        void LogDeleting(int userId, int storeId, object oldModel);
    }
}