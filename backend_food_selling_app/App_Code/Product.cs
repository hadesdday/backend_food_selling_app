using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Product
/// </summary>
[Serializable]
public class Product
{
    public int masp { get; set; }
    public string tensp { get; set; }
    public double gia { get; set; }
    public int soluongmua { get; set; }
    public string ngaydathang { get; set; }
}