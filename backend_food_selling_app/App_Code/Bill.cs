using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Bill
/// </summary>
[Serializable]
public class Bill 
{
  
    public int idBill { get; set; }
    public string dateBill { get; set; } 
    public string phoneNumber { get; set; }
    public string address { get; set; }
    public int billPrice { get; set; }
    public string payment { get;set; }
}
