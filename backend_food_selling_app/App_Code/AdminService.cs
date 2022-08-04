//using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    public List<CustomerEntity> GetCustomerListByOldest()
    {
        try
        {
            return _context.Customer.OrderBy(c => c.CreatedAt).ToList<CustomerEntity>();
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
    public List<VoucherEntity> GetVoucherListByOldest()
    {
        try
        {
            return _context.Voucher.OrderBy(t => t.CreatedAt).ToList<VoucherEntity>();
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
            _context.SaveChanges();
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
    public List<SaleEntity> GetSaleListByOldest()
    {
        try
        {
            return _context.Sale.OrderBy(t => t.CreatedAt).ToList<SaleEntity>();
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
            _context.SaveChanges();
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
            customerByName.OrderByDescending(c => c.CreatedAt);
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
            List<SaleEntity> list = _context.Sale.Where(t => t.FoodType == foodType).OrderByDescending(t => t.CreatedAt).ToList<SaleEntity>();
            return list;
        }
        catch (Exception)
        {
            return new List<SaleEntity>();
        }
    }

    [WebMethod]
    public int getNumberOfFoodType()
    {
        Connection connection = new Connection();
        int count = connection.getCount("select count(*) from food_type");
        connection.closeConnection();
        return count;
    }

    [WebMethod]
    public List<FoodType> getFoodType()
    {
        List<FoodType> list = new List<FoodType>();
        Connection connection = new Connection();
        MySqlDataReader data = connection.getData("select * from food_type");

        while (data.Read())
        {
            FoodType foodType = new FoodType();
            foodType.id = data.GetInt32(0);
            foodType.name = data.GetString(1);
            list.Add(foodType);
        }

        connection.closeConnection();
        return list;
    }

    [WebMethod]
    public int addFoodType(string foodTypeName)
    {
        List<FoodType> foodTypeList = getFoodType();

        foreach (FoodType ft in foodTypeList)
        {
            if (ft.name.Equals(foodTypeName))
            {
                return -1;
            }
        }

        string sql = "insert into food_type(name) values ('" + foodTypeName + "')";
        Connection connection = new Connection();
        return connection.exeNonQuery(sql);
    }

    [WebMethod]
    public int getNumberOfFood()
    {
        Connection connection = new Connection();
        int count = connection.getCount("select count(*) from food");
        connection.closeConnection();
        return count;
    }

    [WebMethod]
    public List<Food> getFood()
    {
        List<Food> list = new List<Food>();
        Connection connection = new Connection();
        MySqlDataReader data = connection.getData("select * from food");

        while (data.Read())
        {
            Food food = new Food();
            food.id = data.GetInt32(0);
            food.food_type = data.GetInt32(1);
            food.name = data.GetString(2);
            food.image_url = data.GetString(3);
            food.description = data.GetString(4);
            food.price = data.GetDouble(5);
            list.Add(food);
        }

        connection.closeConnection();
        return list;
    }

    [WebMethod]
    public List<Food> getFoodByFoodType(int foodTypeId)
    {
        List<Food> list = new List<Food>();
        Connection connection = new Connection();
        MySqlDataReader data = connection.getData("select * from food where food_type = " + foodTypeId);

        while (data.Read())
        {
            Food food = new Food();
            food.id = data.GetInt32(0);
            food.food_type = data.GetInt32(1);
            food.name = data.GetString(2);
            food.image_url = data.GetString(3);
            food.description = data.GetString(4);
            food.price = data.GetDouble(5);
            list.Add(food);
        }

        connection.closeConnection();
        return list;
    }

    [WebMethod]
    public Food getFoodByFoodId(int foodId)
    {
        Food food = new Food();
        Connection connection = new Connection();
        MySqlDataReader data = connection.getData("select * from food where id = " + foodId);

        while (data.Read())
        {
            food.id = data.GetInt32(0);
            food.food_type = data.GetInt32(1);
            food.name = data.GetString(2);
            food.image_url = data.GetString(3);
            food.description = data.GetString(4);
            food.price = data.GetDouble(5);
        }

        connection.closeConnection();
        return food;
    }

    [WebMethod]
    public int addFood(string foodName, int foodTypeId, string foodImage, string foodDescription, int foodPrice)
    {
        List<Food> foodList = getFood();

        foreach (Food ft in foodList)
        {
            if (ft.name == foodName)
            {
                return -1;
            }
        }

        string sql = "insert into food(name, food_type, image_url, description, " +
            "price) values ('" + foodName + "', '" + foodTypeId + "', '" +
            foodImage + "', '" + foodDescription + "', '" + foodPrice + "')";
        Connection connection = new Connection();
        return connection.exeNonQuery(sql);
    }

    [WebMethod]
    public int delFood(int foodId)
    {
        List<Food> foodList = getFood();

        foreach (Food ft in foodList)
        {
            if (ft.id == foodId)
            {
                string sql = "delete from food where id = '" + foodId + "'";
                Connection connection = new Connection();
                return connection.exeNonQuery(sql);
            }
        }

        return -1;
    }

    [WebMethod]
    public int updateFood(int foodId, int foodTypeId, string foodName, string foodImage, string foodDescription, int foodPrice)
    {
        List<Food> foodList = getFood();

        foreach (Food ft in foodList)
        {
            if (ft.id == foodId)
            {
                string sql = "update food set " +
                    "food_type = '" + foodTypeId + "', " +
                    "name = '" + foodName + "', " +
                    "image_url = '" + foodImage + "', " +
                    "description = " + foodDescription + ", " +
                    "price = " + foodPrice + " " +
                    "where id = '" + foodId + "'";
                Connection connection = new Connection();
                return connection.exeNonQuery(sql);
            }
        }

        return -1;
    }

    [WebMethod]
    public List<FoodRating> getFoodRate()
    {
        List<FoodRating> list = new List<FoodRating>();
        Connection connection = new Connection();
        MySqlDataReader data = connection.getData("select * from review");

        while (data.Read())
        {
            FoodRating foodRating = new FoodRating();
            foodRating.id = data.GetInt32(0);
            foodRating.food_id = data.GetInt32(1);
            foodRating.rate = data.GetDouble(2);
            foodRating.comment = data.GetString(3);
            list.Add(foodRating);
        }

        connection.closeConnection();
        return list;
    }

    [WebMethod]
    public List<FoodRating> getFoodRateByFoodId(int foodId)
    {
        List<FoodRating> list = new List<FoodRating>();
        Connection connection = new Connection();
        MySqlDataReader data = connection.getData("select * from review " +
            "where id = '" + foodId + "';");

        while (data.Read())
        {
            FoodRating foodRating = new FoodRating();
            foodRating.id = data.GetInt32(0);
            foodRating.food_id = data.GetInt32(1);
            foodRating.rate = data.GetDouble(2);
            foodRating.comment = data.GetString(3);
            list.Add(foodRating);
        }

        connection.closeConnection();
        return list;
    }

    [WebMethod]
    public int addFoodRating(int foodId, float foodRate, string foodComment)
    {
        string sql = "insert into review(food_id, rate, comment) " +
            "values ('" + foodId + "', " + foodRate + ", '" + foodComment + "');";
        Connection connection = new Connection();
        return connection.exeNonQuery(sql);
    }

    [WebMethod]
    public int delFoodRating(int rateId)
    {
        List<FoodRating> foodRateList = getFoodRate();

        foreach (FoodRating ft in foodRateList)
        {
            if (ft.id == rateId)
            {
                string sql = "delete from review where id = '" + rateId + "'";
                Connection connection = new Connection();
                return connection.exeNonQuery(sql);
            }
        }

        return -1;
    }

    [WebMethod]
    public double avgFoodRate(int foodId)
    {
        Connection connection = new Connection();
        double avgRate = connection.getDouble("select avg(review) AvgRate from review " +
            "where id = '" + foodId + "';");
        connection.closeConnection();
        return avgRate;
    }

    [WebMethod]
    public List<Food> getSearchedFood(string query)
    {
        List<Food> list = new List<Food>();
        Connection connection = new Connection();
        string sql = "select * from food where name like '%" + query + "%';";
        MySqlDataReader data = connection.getData(sql);

        while (data.Read())
        {
            Food food = new Food();
            food.id = data.GetInt32(0);
            food.food_type = data.GetInt32(1);
            food.name = data.GetString(2);
            food.image_url = data.GetString(3);
            food.description = data.GetString(4);
            food.price = data.GetDouble(5);
            list.Add(food);
        }

        connection.closeConnection();
        return list;
    }

    [WebMethod]
    public UserEntity Login(string username, string password)
    {
        try
        {
            UserEntity user = _context.User.Where(t => (t.Username.Equals(username) && t.Password.Equals(password))).FirstOrDefault<UserEntity>();
            if (user.Role.Equals("admin"))
            {
                return user;
            }
            else
            {
                return null;
            }
        }
        catch (Exception)
        {
            return null;
        }

    }
    [WebMethod]
    public Boolean ForgotPassword(String username)
    {
        try
        {
            UserEntity user = _context.User.Where(t => t.Username.Equals(username)).FirstOrDefault<UserEntity>();
            if (user != null && user.Role.Equals("admin"))
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                smtpClient.Credentials = new NetworkCredential("devwebchichoo@gmail.com", "btguqgaesartwahq");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                MailMessage mail = new MailMessage();

                mail.Body = "Xin chào bạn , chúng tôi có nhận được yêu cầu quên mật khẩu từ bạn .\n Mật khẩu của bạn là " + user.Password;
                mail.From = new MailAddress("devwebchichoo@gmail.com");
                mail.To.Add(new MailAddress(user.Email));
                mail.Subject = "Quên mật khẩu - Admin Food Selling App";
                smtpClient.Send(mail);
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
}
