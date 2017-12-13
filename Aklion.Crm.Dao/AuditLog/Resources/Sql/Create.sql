insert dbo.AuditLog
(
    UserId,
    StoreId,
    ActionType,
    OldValue,
    NewValue,
    TimeStamp
)
values
(
    @UserId,
    @StoreId,
    @ActionType,
    @OldValue,
    @NewValue,
	getdate()
);