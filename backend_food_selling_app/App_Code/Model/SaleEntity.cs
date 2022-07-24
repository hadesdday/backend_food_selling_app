using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SaleEntity
/// </summary>

[Table("sale")]
public class SaleEntity
{
    [Column("id", TypeName = "int")]
    public int Id { get; set; }

    [Column("food_type", TypeName = "int")]
    public int FoodType { get; set; }
    [Column("rate", TypeName = "double")]
    public double Rate { get; set; }
    [Column("end_time", TypeName = "datetime")]
    public string EndTime { get; set; }
    [Column("description", TypeName = "text")]
    public string Description { get; set; }
    [Column("active", TypeName = "tinyint")]
    public int Active { get; set; }
    [Column("createdAt", TypeName = "timestamp")]
    public string CreatedAt { get; set; }
}