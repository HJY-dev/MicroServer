using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTest.Model
{
    [Table(Name = "AspNetUsers")]
    public class AspNetUsers
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
