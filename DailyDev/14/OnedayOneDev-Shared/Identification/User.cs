using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnedayOneDev_Shared.Identification
{
    [Table("Users")]
    public class User
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column("UserName")]
        [StringLength(128)]
        public string UserName { get; set; } = string.Empty;
        [Column("Password")]
        [StringLength(128)]
        public string Password { get; set; } = string.Empty;

        [Column("Role")]
        [StringLength(128)]
        public UserRole Role { get; set; } = UserRole.USER;

        public User()
        {

        }
        public User(int id,string userName,string password, UserRole role = UserRole.USER)
        {
            this.id = id;
            this.UserName = userName;
            this.Password = password;
            this.Role = role;
        }
        public User( string userName, string password, UserRole role = UserRole.USER)
        {
            
            this.UserName = userName;
            this.Password = password;
            this.Role = role;
        }
    }
}
