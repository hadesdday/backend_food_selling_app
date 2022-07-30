using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


[Table("voucher")]
public class VoucherEntity
{
    [Column("id", TypeName = "varchar(255)")]
    public string Id { get; set; }

    [Column("rate", TypeName = "double")]
    public double Rate { get; set; }
    [Column("active", TypeName = "int")]
    public int Active { get; set; }
    [Column("createdAt", TypeName = "timestamp")]
    public string CreatedAt { get; set; }
}