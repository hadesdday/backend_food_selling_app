using MySql.EntityFrameworkCore.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

[Table("bill")]
public class BillEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id", TypeName = "int")]
    public int Id { get; set; }

    [Column("voucher_id", TypeName = "varchar(255)")]
    public string Voucher { get; set; }

    [Required(ErrorMessage = "- Vui long nhap ma khach hang")]
    [Column("customer_id", TypeName = "int")]
    public int CustomerId { get; set; }
    [Required(ErrorMessage = "- Vui long nhap tong gia hoa don")]
    [Column("price", TypeName = "double")]
    public double Price { get; set; }
    [Required(ErrorMessage = "- Vui long nhap phuong thuc thanh toan")]
    [Column("payment_method", TypeName = "varchar(255)")]
    public string PaymentMethod { get; set; }
    [Required(ErrorMessage = "- Thieu trang thai hoa don")]
    [Column("status", TypeName = "int")]
    public int Status { get; set; }
}