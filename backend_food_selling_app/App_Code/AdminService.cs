using Microsoft.EntityFrameworkCore;
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
        try
        {
            if (bill.Voucher == null)
            {
                bill.Voucher = "";
            }
            _context.Database.EnsureCreated();
            _context.Bill.Add(bill);
            _context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    [WebMethod]
    public Boolean CancelOrder(int id)
    {
        try
        {
            BillEntity b = _context.Bill.Where(t => t.Id == id).FirstOrDefault<BillEntity>();
            b.Status = 3;
            _context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    [WebMethod]
    public List<BillDetailsEntity> GetOrderDetails(int BillId)
    {
        List<BillDetailsEntity> list = new List<BillDetailsEntity>();
        list = _context.Bill_Details.Where(t => t.BillId == BillId).ToList<BillDetailsEntity>();
        return list;
    }
    [WebMethod]
    public Boolean AddCustomer(CustomerEntity customer)
    {
        try
        {
            _context.Customer.Add(customer);
            _context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    [WebMethod]
    public CustomerEntity GetCustomer(int id)
    {
        try
        {
            CustomerEntity customer = _context.Customer.Where(t => t.Id == id).FirstOrDefault<CustomerEntity>();
            return customer;
        }
        catch (Exception)
        {
            return null;
        }
    }

    [WebMethod]
    public Boolean UpdateCustomerInformation(CustomerEntity customer)
    {
        try
        {
            CustomerEntity oldCustomer = _context.Customer.Where(t => t.Id == customer.Id).FirstOrDefault<CustomerEntity>();
            oldCustomer.Name = customer.Name;
            oldCustomer.Address = customer.Address;
            oldCustomer.Phone = customer.Phone;
            int rows = _context.SaveChanges();
            System.Diagnostics.Debug.WriteLine("entries : " + rows);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    [WebMethod]
    public Boolean DeleteCustomer(int id)
    {
        try
        {
            var current = _context.Customer.Where(z => z.Id == id).FirstOrDefault<CustomerEntity>();
            if (current != null)
            {
                _context.Entry(current).State = EntityState.Deleted;
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
    [WebMethod]
    public Boolean AddVoucher(VoucherEntity voucher)
    {
        try
        {
            _context.Voucher.Add(voucher);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    [WebMethod]
    public Boolean DeactivateVoucher(string id)
    {
        try
        {
            VoucherEntity voucher = _context.Voucher.Where(t => t.Id.Equals(id)).FirstOrDefault<VoucherEntity>();
            voucher.Active = 0;
            _context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    [WebMethod]
    public Boolean AddSale(SaleEntity sale)
    {
        try
        {
            _context.Sale.Add(sale);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    [WebMethod]
    public Boolean DeactivateSale(int id)
    {
        try
        {
            SaleEntity sale = _context.Sale.Where(t => t.Id == id).FirstOrDefault<SaleEntity>();
            sale.Active = 0;
            _context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
