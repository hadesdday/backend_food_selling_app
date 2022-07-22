using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://localhost/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    [WebMethod]
    public List<Product> getProductInBill(int idBill)
    {
        List<Product> products = new List<Product>();
        string strConnection = "server=localhost;uid=root;pwd=;database=android2;";

        MySqlConnection conn = new MySqlConnection(strConnection);
        conn.Open();
        string sql = "select a.id,a.name,a.price,c.amount from food a,bill b,bill_details c where b.id=c.bill_id and a.id=c.food_id and c.bill_id=@idBill";
        MySqlCommand cmd = conn.CreateCommand();
        MySqlParameter mahoadon = new MySqlParameter("@idBill", MySqlDbType.Int32);
        mahoadon.Value = idBill;
        cmd.CommandText = sql;
        cmd.Parameters.Add(mahoadon);

        MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Product product = new Product();
            product.idProduct = reader.GetInt32(0);
            product.nameProduct = reader.GetString(1);
            product.priceProduct = reader.GetDouble(2);
            product.amount = reader.GetInt32(3);
            products.Add(product);
        }
        conn.Close();
        return products;
    }
    [WebMethod]
    public List<Bill> getBillList(int status)
    {
        List<Bill> bills = new List<Bill>();
        string strConnection = "server=localhost;uid=root;pwd=;database=android2;";

        MySqlConnection conn = new MySqlConnection(strConnection);
        conn.Open();
        string sql = "select a.id,a.createdAt,a.price,b.phone_number,b.address from bill a,customer b where a.customer_id=b.id and a.status = @status";
        MySqlCommand cmd = conn.CreateCommand();
        MySqlParameter tt = new MySqlParameter("@status", MySqlDbType.Int32);
        tt.Value = status;
        cmd.CommandText = sql;
        cmd.Parameters.Add(tt);

        MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Bill b = new Bill();
            b.idBill = reader.GetInt32(0);
            b.dateBill = reader.GetDateTime(1).ToString();
            b.billPrice = 0;
            b.phoneNumber = reader.GetString(3);
            b.address = reader.GetString(4);
            bills.Add(b);
        }
        conn.Close();
        return bills;
    }
    [WebMethod]
    public int deleteIteminBill(int billId, int foodId)
    {
        string strConnection = "server=localhost;uid=root;pwd=;database=android2;";

        MySqlConnection conn = new MySqlConnection(strConnection);
        conn.Open();
        string sql = "delete  from bill_details where bill_id = @billId and food_id=@foodId";
        MySqlCommand cmd = conn.CreateCommand();
        MySqlParameter hd = new MySqlParameter("@billId", MySqlDbType.Int32);
        hd.Value = billId;
        MySqlParameter sp = new MySqlParameter("@foodId", MySqlDbType.Int32);
        sp.Value = foodId;
        cmd.CommandText = sql;
        cmd.Parameters.Add(hd);
        cmd.Parameters.Add(sp);

        return cmd.ExecuteNonQuery();
    }
    [WebMethod]
    public int changeQuantityProduct(int billId, int foodId,int amount)
    {
        List<Bill> bills = new List<Bill>();
        string strConnection = "server=localhost;uid=root;pwd=;database=android2;";

        MySqlConnection conn = new MySqlConnection(strConnection);
        conn.Open();
        string sql = "update bill_details set amount=@amount where bill_id = @billId and food_id=@foodId";
        MySqlCommand cmd = conn.CreateCommand();
        MySqlParameter hd = new MySqlParameter("@billId", MySqlDbType.Int32);
        hd.Value = billId;
        MySqlParameter sp = new MySqlParameter("@foodId", MySqlDbType.Int32);
        sp.Value = foodId;
        MySqlParameter sl = new MySqlParameter("@amount", MySqlDbType.Int32);
        sl.Value = amount;
        cmd.CommandText = sql;
        cmd.Parameters.Add(hd);
        cmd.Parameters.Add(sp);
        cmd.Parameters.Add(sl);
        return cmd.ExecuteNonQuery();
    }
    [WebMethod]
    public int deleteBill(int billID)
    {
        string strConnection = "server=localhost;uid=root;pwd=;database=android2;";

        MySqlConnection conn = new MySqlConnection(strConnection);
        conn.Open();
        string sql = "update bill set status='3' where id = @billID ";
        MySqlCommand cmd = conn.CreateCommand();
        MySqlParameter hd = new MySqlParameter("@billID", MySqlDbType.Int32);
        hd.Value = billID;

        cmd.CommandText = sql;
        cmd.Parameters.Add(hd);
    
        return cmd.ExecuteNonQuery();
    }
    [WebMethod]
    public int insertBill(string name, int billPrice, string phoneNumber,string address,string payment)
    {
        int count = 0;
        string strConnection = "server=localhost;uid=root;pwd=;database=android2;";

        MySqlConnection conn = new MySqlConnection(strConnection);
        conn.Open();
        string sql2 = "insert into customer (name,address,phone_number) values(@name,@address,@phone)";
        MySqlCommand cmd2 = conn.CreateCommand();
        MySqlParameter nameC = new MySqlParameter("@name", MySqlDbType.Int32);
        nameC.Value = name;
        MySqlParameter addr = new MySqlParameter("@address", MySqlDbType.Int32);
        addr.Value = address;
        MySqlParameter phone = new MySqlParameter("@phone", MySqlDbType.Int32);
        phone.Value = phoneNumber;
        cmd2.CommandText = sql2;
        cmd2.Parameters.Add(nameC);
        cmd2.Parameters.Add(addr);
        cmd2.Parameters.Add(phone);
        cmd2.ExecuteNonQuery();
        long customer_id = cmd2.LastInsertedId;

        //string sql = "insert into hoadon (ngayhoadon,tonghoadon,sodienthoai,diachi,thanhtoan,trangthai) values('"+b.ngayhoadon+"','"+b.tonghoadon + "','" + b.sodienthoai + "','" + b.diachi + "','" + b.thanhtoan+",'1')";
        string sql = "insert into bill (customer_id,price,payment_method,status) values(@customer_id,@billPrice,@payment,'1')";
        MySqlCommand cmd = conn.CreateCommand();
        MySqlParameter cid = new MySqlParameter("@customer_id", MySqlDbType.Int32);
        cid.Value = customer_id;
        MySqlParameter thd = new MySqlParameter("@billPrice", MySqlDbType.Int32);
        thd.Value = billPrice;
        MySqlParameter tt = new MySqlParameter("@payment", MySqlDbType.Int32);
        tt.Value = payment;
        cmd.CommandText = sql;
        cmd.Parameters.Add(cid);
        cmd.Parameters.Add(thd);
        cmd.Parameters.Add(tt);
        count = cmd.ExecuteNonQuery();

        if (count == 1)
        {
            long id = cmd.LastInsertedId;
            conn.Close();
            return (int)id;
        }
        conn.Close();
        return 0;
    }
    [WebMethod]
    public int addBillDetail(int billId,int foodId,int amount)
    {
        string strConnection = "server=localhost;uid=root;pwd=;database=android2;";
        MySqlConnection conn = new MySqlConnection(strConnection);
        conn.Open();
        string sql = "insert into bill_details (bill_id,food_id,amount) values (@billId,@foodId,@amount);";
        MySqlCommand cmd = conn.CreateCommand();
        MySqlParameter mahoadon = new MySqlParameter("@billId", MySqlDbType.Int32);
        mahoadon.Value = billId;
        MySqlParameter masanpham = new MySqlParameter("@foodId", MySqlDbType.Int32);
        masanpham.Value = foodId;
        MySqlParameter sl = new MySqlParameter("@amount", MySqlDbType.Int32);
        sl.Value = amount;
        cmd.CommandText = sql;
        cmd.Parameters.Add(mahoadon);
        cmd.Parameters.Add(masanpham);
        cmd.Parameters.Add(sl);
        return cmd.ExecuteNonQuery();
    }
    [WebMethod]
    public int getVoucher(string id_voucher)
    {
        double rate = 0;
        int count = 0;
        string strConnection = "server=localhost;uid=root;pwd=;database=android2;";

        MySqlConnection conn = new MySqlConnection(strConnection);
        conn.Open();
        string sql = "select *from voucher where id=@id_voucher and active=1;";
        MySqlCommand cmd = conn.CreateCommand();
        MySqlParameter id = new MySqlParameter("@id_voucher", MySqlDbType.Int32);
        id.Value = id_voucher;
        cmd.CommandText = sql;
        cmd.Parameters.Add(id);

        MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            count++;
            rate = reader.GetDouble(1);
        }
        conn.Close();
        if (count == 1) return (int)(rate * 100);
        else return 0;
        
    }
    [WebMethod]
    public int usedVoucher(string id_voucher)
    {
        string strConnection = "server=localhost;uid=root;pwd=;database=android2;";

        MySqlConnection conn = new MySqlConnection(strConnection);
        conn.Open();
        string sql = "update voucher set active=2 where id = @id_voucher ";
        MySqlCommand cmd = conn.CreateCommand();
        MySqlParameter id = new MySqlParameter("@id_voucher", MySqlDbType.Int32);
        id.Value = id_voucher;
        cmd.CommandText = sql;
        cmd.Parameters.Add(id);
      
        return cmd.ExecuteNonQuery();
    }
    
        [WebMethod (EnableSession = true)]
        public User login(string user, string pass)
        {
            string strConn = "server=localhost;database=webservice;user=root;pwd=123456";

            MySqlConnection conn = new MySqlConnection(strConn);
            conn.Open();

            string query = "select * from webservice.user where username ='" + user + "'and password = '" + pass + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader da = cmd.ExecuteReader();
            User u = new User();
            while (da.Read())
            {
                u.username = da.GetString(1);
                u.password = da.GetString(2);
                u.email = da.GetString(3);
                Session["user"] = user;
                return u;
            }
            return null;
        }
        
        [WebMethod(EnableSession = true)]
        public string logout()
        {
            if(Session["user"] != null)
            {
               return (string)(Session["user"] = null);
            }
            return "";
        }
        
        [WebMethod]
        public int register(string username, string password, string email)
        {
            string strConn = "server=localhost;database=webservice;user=root;pwd=123456";
            MySqlConnection conn = new MySqlConnection(strConn);
            conn.Open();

            string query = "insert into webservice.user(username, password, email)" +
                "values ('" + username + "','" + password + "','" + email + "')";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            return cmd.ExecuteNonQuery();
        }
        
        [WebMethod]
        public User checkUser(string username)
        {
            string strConn = "server=localhost;database=webservice;user=root;pwd=123456";
            MySqlConnection conn = new MySqlConnection(strConn);
            conn.Open();

            string query = "select * from webservice.user where username = '" + username + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                User u = new User();
                u.username = da.GetString(1);
                u.password = da.GetString(2);
                u.email = da.GetString(3);
                return u;
            }
            return null;
        }
        
        [WebMethod]
        public int changePassword(string username, string newpassword)
        {
            string strConn = "server=localhost;database=webservice;user=root;pwd=123456";
            MySqlConnection conn = new MySqlConnection(strConn);
            conn.Open();

            string query = "update webservice.user set password ='" + newpassword + "'where username ='" + username + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            return cmd.ExecuteNonQuery();
        }
}
