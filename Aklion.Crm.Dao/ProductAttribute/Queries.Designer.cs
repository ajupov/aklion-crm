﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aklion.Crm.Dao.ProductAttribute {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Queries {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Queries() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Aklion.Crm.Dao.ProductAttribute.Queries", typeof(Queries).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на insert dbo.ProductAttribute
        ///(
        ///	StoreId,
        ///	ProductId,
        ///	AttributeId,
        ///	[Value],
        ///	IsDeleted,
        ///	CreateDate,
        ///	ModifyDate
        ///)
        ///values
        ///(
        ///	@StoreId,
        ///	@ProductId,
        ///	@AttributeId,
        ///	@Value,
        ///	@IsDeleted,
        ///    getdate(),
        ///    null
        ///);
        ///
        ///select
        ///	scope_identity();.
        /// </summary>
        internal static string Create {
            get {
                return ResourceManager.GetString("Create", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на delete
        ///	from dbo.ProductAttribute
        ///	where Id = @id;.
        /// </summary>
        internal static string Delete {
            get {
                return ResourceManager.GetString("Delete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на select top 1
        ///	pa.Id,
        ///    pa.StoreId,
        ///	s.[Name]	as StoreName,
        ///    pa.ProductId,
        ///	p.[Name]	as ProductName,
        ///    pa.AttributeId,
        ///	a.[Name]	as AttributeName,
        ///	pa.[Value],
        ///	pa.IsDeleted,
        ///    pa.CreateDate,
        ///    pa.ModifyDate
        ///	from dbo.ProductAttribute as pa
        ///		inner join dbo.Store as s on
        ///			pa.StoreId = s.Id
        ///		inner join dbo.Product as p on
        ///			pa.ProductId = p.Id
        ///		inner join dbo.Attribute as a on
        ///			pa.AttributeId = a.Id
        ///	where pa.Id = @id;.
        /// </summary>
        internal static string Get {
            get {
                return ResourceManager.GetString("Get", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на select
        ///	count(0)
        ///	from dbo.ProductAttribute as pa
        ///		inner join dbo.Store as s on
        ///			pa.StoreId = s.Id
        ///		inner join dbo.Product as p on
        ///			pa.ProductId = p.Id
        ///		inner join dbo.Attribute as c on
        ///			pa.AttributeId = c.Id
        ///	where @IsSearch = 0 or
        ///		((coalesce(@Id, 0) = 0 or pa.Id = @Id)
        ///			and (coalesce(@StoreId, 0) = 0 or pa.StoreId = @StoreId)
        ///			and (coalesce(@StoreName, &apos;&apos;) = &apos;&apos; or s.[Name] like @StoreName + &apos;%&apos;)
        ///			and (coalesce(@ProductId, 0) = 0 or pa.ProductId = @ProductId)
        ///			and (coalesce [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string GetPagedList {
            get {
                return ResourceManager.GetString("GetPagedList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на update dbo.ProductAttribute
        ///    set StoreId = @StoreId,
        ///		ProductId = @ProductId,
        ///		AttributeId = @AttributeId,
        ///		[Value] = @Value,
        ///		IsDeleted = @IsDeleted,
        ///		ModifyDate = getdate()
        ///    where Id = @Id;.
        /// </summary>
        internal static string Update {
            get {
                return ResourceManager.GetString("Update", resourceCulture);
            }
        }
    }
}