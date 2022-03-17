using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static HomeCareApp.Model.Enums;

namespace HomeCareApp.Model
{
    public class UserRole
    {

        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
