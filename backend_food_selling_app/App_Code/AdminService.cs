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
        try
        {
            List<BillDetailsEntity> list = new List<BillDetailsEntity>();
            list = _context.Bill_Details.Where(t => t.BillId == BillId).ToList<BillDetailsEntity>();
            return list;
        }
        catch (Exception)
        {
            return new List<BillDetailsEntity>();
        }
    }

    [WebMethod]
    public List<CustomerEntity> GetCustomerList()
    {
        try
        {
            return _context.Customer.OrderByDescending(c => c.CreatedAt).ToList<CustomerEntity>();
        }
        catch (Exception)
        {
            return null;
        }
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
            _context.SaveChanges();
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
    public List<VoucherEntity> GetVoucherList()
    {
        try
        {
            return _context.Voucher.OrderByDescending(t => t.CreatedAt).ToList<VoucherEntity>();
        }
        catch (Exception)
        {
            return null;
        }
    }
    [WebMethod]
    public List<VoucherEntity> GetVoucherById(string id)
    {
        try
        {
            return _context.Voucher.Where(t => t.Id.Equals(id)).OrderByDescending(t => t.CreatedAt).ToList<VoucherEntity>();
        }
        catch (Exception)
        {
            return null;
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
    public Boolean DeleteVoucher(string id)
    {
        try
        {
            VoucherEntity voucher = _context.Voucher.Where(t => t.Id.Equals(id)).FirstOrDefault<VoucherEntity>();
            if (voucher != null)
            {
                _context.Entry(voucher).State = EntityState.Deleted;
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
    public List<SaleEntity> GetSaleList()
    {
        try
        {
            return _context.Sale.OrderByDescending(t => t.CreatedAt).ToList<SaleEntity>();
        }
        catch (Exception)
        {
            return null;
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
    public Boolean EditSale(SaleEntity sale)
    {
        try
        {
            SaleEntity s = _context.Sale.Where(t => t.Id == sale.Id).FirstOrDefault<SaleEntity>();
            s.Rate = sale.Rate;
            s.EndTime = sale.EndTime;
            s.Description = sale.Description;
            s.Active = sale.Active;
            _context.SaveChanges();
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
            sale.Active = 1;
            _context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    [WebMethod]
    public List<BillEntity> GetBillList()
    {
        try
        {
            List<BillEntity> billList = null;
            billList = _context.Bill.OrderByDescending(c => c.CreatedAt).ToList();
            return billList;
        }
        catch (Exception)
        {
            return null;
        }
    }
    [WebMethod]
    public List<BillEntity> GetBillListByOldest()
    {
        try
        {
            List<BillEntity> billList = null;
            billList = _context.Bill.OrderBy(c => c.CreatedAt).ToList();
            return billList;
        }
        catch (Exception)
        {
            return null;
        }
    }
    [WebMethod]
    public List<BillEntity> GetBillListByCustomerId(int id)
    {
        try
        {
            List<BillEntity> billList = null;
            billList = _context.Bill.Where(t => t.CustomerId == id).OrderByDescending(c => c.CreatedAt).ToList();
            return billList;
        }
        catch (Exception)
        {
            return null;
        }
    }
    [WebMethod]
    public List<CustomerEntity> GetCustomerListByQueries(string key)
    {
        key = "%" + key + "%";
        try
        {
            List<CustomerEntity> customerByName = (from m in _context.Customer
                                                   where EF.Functions.Like(m.Name, key)
                                                   select m).ToList<CustomerEntity>();
            List<CustomerEntity> customerById = (from m in _context.Customer
                                                 where EF.Functions.Like(m.Id.ToString(), key)
                                                 select m).ToList<CustomerEntity>();
            List<CustomerEntity> customerByPhone = (from m in _context.Customer
                                                    where EF.Functions.Like(m.Phone, key)
                                                    select m).ToList<CustomerEntity>();

            foreach (CustomerEntity c in customerById)
            {
                if (!customerByName.Contains(c))
                {
                    customerByName.Add(c);
                }
            }

            foreach (CustomerEntity c in customerByPhone)
            {
                if (!customerByName.Contains(c))
                {
                    customerByName.Add(c);
                }
            }
            customerByName.OrderBy(c => c.CreatedAt);
            return customerByName;
        }
        catch (Exception)
        {
            return null;
        }
    }
    [WebMethod]
    public List<SaleEntity> GetSaleListByFoodType(int foodType)
    {
        try
        {
            List<SaleEntity> list = _context.Sale.Where(t => t.FoodType == foodType).ToList<SaleEntity>();
            return list;
        }
        catch (Exception)
        {
            return new List<SaleEntity>();
        }
    }
}
