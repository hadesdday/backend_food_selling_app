using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


[Table("bill_details")]
public class BillDetailsEntity
{
    [Column("food_id", TypeName = "int")]
    public int FoodId { get; set; }

    [Column("bill_id", TypeName = "int")]
    public int BillId { get; set; }
    [Column("amount", TypeName = "int")]
    public int Amount { get; set; }
}
