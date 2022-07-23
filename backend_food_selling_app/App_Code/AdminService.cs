using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for AdminService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class AdminService : System.Web.Services.WebService
{
    DatabaseContext _context;
    public AdminService()
    {
        _context = new DatabaseContext();
    }
    [WebMethod]
    public BillEntity GetBillById(int id)
    {
        BillEntity bill = null;
        bill = _context.Bill.Where(t => t.Id == id).FirstOrDefault<BillEntity>();
        return bill;
    }
    [WebMethod]
    public Boolean InsertBill(BillEntity bill)
    {
        //var bill = new BillEntity
        //{
        //    Voucher = Voucher != null ? Voucher : "",
        //    CustomerId = CustomerId,
        //    Price = Price,
        //    PaymentMethod = PaymentMethod,
        //    Status = Status
        //};
        if (bill.Voucher == null)
        {
            bill.Voucher = "";
        }
        _context.Database.EnsureCreated();
        _context.Bill.Add(bill);
        _context.SaveChanges();
        return true;
    }
}
