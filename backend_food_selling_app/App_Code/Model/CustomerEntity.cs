using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


[Table("customer")]
public class CustomerEntity
{
    [Column("id", TypeName = "int")]
    public int Id { get; set; }

    [Column("name", TypeName = "varchar(255)")]
    public string Name { get; set; }
    [Column("address", TypeName = "varchar(255)")]
    public string Address { get; set; }
    [Column("phone_number", TypeName = "varchar(10)")]
    public string Phone { get; set; }
    [Column("username", TypeName = "varchar(255)")]
    public string Username { get; set; }
    [Column("createdAt", TypeName = "timestamp")]
    public string CreatedAt { get; set; }
}