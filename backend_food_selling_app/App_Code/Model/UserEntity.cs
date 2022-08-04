using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserEntity
/// </summary>
/// 
[Table("user")]
public class UserEntity
{
    [Key]
    [Column("username", TypeName = "varchar(255)")]
    public string Username { get; set; }
    [Column("password", TypeName = "varchar(255)")]
    public string Password { get; set; }
    [Column("email", TypeName = "varchar(255)")]
    public string Email { get; set; } 
    [Column("role", TypeName = "varchar(20)")]
    public string Role { get; set; }

}