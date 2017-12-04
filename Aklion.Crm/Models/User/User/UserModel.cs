using System;
using Aklion.Crm.Enums;

namespace Aklion.Crm.Models.User.User
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }
        
        public string Phone { get; set; }
        
        public string Surname { get; set; }
        
        public string Name { get; set; }
        
        public string Patronymic { get; set; }
        
        public Gender Gender { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public DateTime CreateDate { get; set; }
    }
}