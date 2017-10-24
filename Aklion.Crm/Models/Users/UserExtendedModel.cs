using System;
using Aklion.Crm.Enums;
using Aklion.Crm.Models.Base.Enums;

namespace Aklion.Crm.Models.Users
{
    public class UserExtendedModel : User
    {
        #region Id
        public SortingOrder IdSortingOrder { get; set; }

        public int IdSortingPriority { get; set; }

        public FilterOperation IdFilterOperation { get; set; }

        public int IdFilterOperand1 { get; set; }

        public int IdFilterOperand2 { get; set; }
        #endregion

        #region Login
        public SortingOrder LoginSortingOrder { get; set; }

        public int LoginSortingPriority { get; set; }

        public FilterOperation LoginFilterOperation { get; set; }

        public int LoginFilterOperand { get; set; }
        #endregion

        #region Email
        public SortingOrder EmailSortingOrder { get; set; }

        public int EmailSortingPriority { get; set; }

        public FilterOperation EmailFilterOperation { get; set; }

        public int EmailFilterOperand { get; set; }
        #endregion

        #region Phone
        public SortingOrder PhoneSortingOrder { get; set; }

        public int PhoneSortingPriority { get; set; }

        public FilterOperation PhoneFilterOperation { get; set; }

        public int PhoneFilterOperand { get; set; }
        #endregion

        #region Surname
        public SortingOrder SurnameSortingOrder { get; set; }

        public int SurnameSortingPriority { get; set; }

        public FilterOperation SurnameFilterOperation { get; set; }

        public int SurnameFilterOperand { get; set; }
        #endregion

        #region Name
        public SortingOrder NameSortingOrder { get; set; }

        public int NameSortingPriority { get; set; }

        public FilterOperation NameFilterOperation { get; set; }

        public int NameFilterOperand { get; set; }
        #endregion

        #region Patronymic
        public SortingOrder PatronymicSortingOrder { get; set; }

        public int PatronymicSortingPriority { get; set; }

        public FilterOperation PatronymicFilterOperation { get; set; }

        public int PatronymicFilterOperand { get; set; }
        #endregion

        #region Gender
        public SortingOrder GenderSortingOrder { get; set; }

        public int GenderSortingPriority { get; set; }

        public FilterOperation GenderFilterOperation { get; set; }

        public byte GenderFilterOperand { get; set; }
        #endregion

        #region BirthDate
        public SortingOrder BirthDateSortingOrder { get; set; }

        public int BirthDateSortingPriority { get; set; }

        public FilterOperation BirthDateFilterOperation { get; set; }

        public DateTime BirthDateFilterOperand1 { get; set; }

        public DateTime BirthDateFilterOperand2 { get; set; }
        #endregion

        #region IsEmailConfirmed
        public SortingOrder IsEmailConfirmedSortingOrder { get; set; }

        public int IsEmailConfirmedSortingPriority { get; set; }

        public FilterOperation IsEmailConfirmedFilterOperation { get; set; }

        public bool IsEmailConfirmedFilterOperand { get; set; }
        #endregion

        #region IsPhoneConfirmed
        public SortingOrder IsPhoneConfirmedSortingOrder { get; set; }

        public int IsPhoneConfirmedSortingPriority { get; set; }

        public FilterOperation IsPhoneConfirmedFilterOperation { get; set; }

        public bool IsPhoneConfirmedFilterOperand { get; set; }
        #endregion

        #region IsLocked
        public SortingOrder IsLockedSortingOrder { get; set; }

        public int IsLockedSortingPriority { get; set; }

        public FilterOperation IsLockedFilterOperation { get; set; }

        public bool IsLockedFilterOperand { get; set; }
        #endregion

        #region IsDeleted
        public SortingOrder IsDeletedSortingOrder { get; set; }

        public int IsDeletedSortingPriority { get; set; }

        public FilterOperation IsDeletedFilterOperation { get; set; }

        public bool IsDeletedFilterOperand { get; set; }
        #endregion

        #region CreateDate
        public SortingOrder CreateDateSortingOrder { get; set; }

        public int CreateDateSortingPriority { get; set; }

        public FilterOperation CreateDateFilterOperation { get; set; }

        public DateTime CreateDateFilterOperand1 { get; set; }

        public DateTime CreateDateFilterOperand2 { get; set; }
        #endregion

        #region ModifyDate
        public SortingOrder ModifyDateSortingOrder { get; set; }

        public int ModifyDateSortingPriority { get; set; }

        public FilterOperation ModifyDateFilterOperation { get; set; }

        public DateTime ModifyDateFilterOperand1 { get; set; }

        public DateTime ModifyDateFilterOperand2 { get; set; }
        #endregion

        #region Paging/Sizing
        public int Page { get; set; }

        public int Size { get; set; }

        #endregion
    }
}