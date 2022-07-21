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
    public int idProduct { get; set; }
    public string nameProduct { get; set; }
    public double priceProduct { get; set; }
    public int amount { get; set; }
    public string dateOrdered { get; set; }
}