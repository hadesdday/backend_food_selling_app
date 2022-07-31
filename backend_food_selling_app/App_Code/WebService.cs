//using MySql.Data.MySqlClient;
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
        string strConnection = "server=localhost;uid=root;pwd=;database=android3;";

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
        string strConnection = "server=localhost;uid=root;pwd=;database=android3;";

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
        string strConnection = "server=localhost;uid=root;pwd=;database=android3;";

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
        string strConnection = "server=localhost;uid=root;pwd=;database=android3;";

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
        string strConnection = "server=localhost;uid=root;pwd=;database=android3;";

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
    public int insertBill(string name, int billPrice, string phoneNumber,string address,string payment,string username)
    {
        int count = 0;
        string strConnection = "server=localhost;uid=root;pwd=;database=android3;";

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
       
        if (username != "")
        {
            MySqlParameter user = new MySqlParameter("@username", MySqlDbType.Int32);
            user.Value = username;
            sql2 = "insert into customer (name,address,phone_number,username) values(@name,@address,@phone,@username)";
            cmd2.Parameters.Add(user);
        }
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
        string strConnection = "server=localhost;uid=root;pwd=;database=android3;";
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
        string strConnection = "server=localhost;uid=root;pwd=;database=android3;";

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
        string strConnection = "server=localhost;uid=root;pwd=;database=android3;";

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

    [WebMethod]
    public int customerRegister(string name, string address, string phoneNumber)
    {
        string strConn = "server=localhost;database=webservice;user=root;pwd=123456";
        MySqlConnection conn = new MySqlConnection(strConn);
        conn.Open();

        string query = "insert into  webservice.customer(name, address, phoneNumber) values ('" + name + "', '" + address + "', '" + phoneNumber + "')";
        MySqlCommand cmd = new MySqlCommand(query, conn);
        return cmd.ExecuteNonQuery();
    }

    [WebMethod]
    public int userRegister(string username, string password, string email)
    {
        string strConn = "server=localhost;database=webservice;user=root;pwd=123456";
        MySqlConnection conn = new MySqlConnection(strConn);
        conn.Open();

        string query = "insert into  webservice.user(username, password, email) values ('" + username + "', '" + password + "', '" + email + "')";
        MySqlCommand cmd = new MySqlCommand(query, conn);
        return cmd.ExecuteNonQuery();
    }


    [WebMethod]
    public User checkUser(string username)
    {
        //string strConn = "server=localhost;database=webservice;user=root;pwd=123456";
        string strConn = "server = localhost; uid = root; pwd =; database = android3; ";
        MySqlConnection conn = new MySqlConnection(strConn);
        conn.Open();

        string query = "select * from user where username = '" + username + "'";
        MySqlCommand cmd = new MySqlCommand(query, conn);
        MySqlDataReader da = cmd.ExecuteReader();
        while (da.Read())
        {
            User u = new User();
            u.username = da.GetString(0);
            u.password = da.GetString(1);
            u.email = da.GetString(2);
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

    [WebMethod]
    public User GetUser(string username)
    {
        string strConn = "server=localhost;database=webservice;user=root;pwd=123456";
        MySqlConnection conn = new MySqlConnection(strConn);
        conn.Open();

        string query = "select * from webservice.user where username ='" + username + "'";
        MySqlCommand cmd = new MySqlCommand(query, conn);
        MySqlDataReader da = cmd.ExecuteReader();
        User u = new User();
        while (da.Read())
        {
            u.username = da.GetString(1);
            u.password = da.GetString(2);
            u.email = da.GetString(3);
            return u;
        }
        return null;
    }

    [WebMethod]
    public List<User> getInfo(string user, string pass)
    {
        List<User> list = new List<User>();
        //string strConn = "server=localhost;database=webservice;user=root;pwd=123456";
        string strConn = "server = localhost; uid = root; pwd =; database = android3; ";
        MySqlConnection conn = new MySqlConnection(strConn);
        conn.Open();

        //string query = "select webservice.user.username, webservice.user.password, webservice.user.email, " +
        //                "webservice.customer.name, webservice.customer.address, webservice.customer.phoneNumber " +
        //               "from webservice.user, webservice.customer " +
        //               "where webservice.user.idCustomer = webservice.customer.idCustomer " +
        //               "and webservice.user.username = '" + user + "'and webservice.user.password = '" + pass + "'";
        string query = "select user.username, user.password, user.email, " +
                        "customer.name, customer.address, customer.phone_number " +
                       "from user, customer " +
                       "where user.username = customer.username " +
                       "and user.username = '" + user + "'and user.password = '" + pass + "'";

        MySqlCommand cmd = new MySqlCommand(query, conn);
        MySqlDataReader da = cmd.ExecuteReader();
        User u = new User();
        while (da.Read())
        {
            u.username = da.GetString(0);
            u.password = da.GetString(1);
            u.email = da.GetString(2);
            u.name = da.GetString(3);
            u.address = da.GetString(4);
            u.phoneNumber = da.GetString(5);
            list.Add(u);

        }
        return list;
    }

    [WebMethod]
    public int updateMail(string username, string newEmail)
    {
        string strConn = "server=localhost;database=webservice;user=root;pwd=123456";
        MySqlConnection conn = new MySqlConnection(strConn);
        conn.Open();

        string query = "update webservice.user, webservice.customer set webservice.user.email = '" + newEmail + "'" +
                       "where webservice.user.username = '" + username + "'" +
                       "and webservice.user.idCustomer = webservice.customer.idCustomer";

        MySqlCommand cmd = new MySqlCommand(query, conn);
        return cmd.ExecuteNonQuery();
    }


    [WebMethod]
    public int updateName(string username, string newName)
    {
        string strConn = "server=localhost;database=webservice;user=root;pwd=123456";
        MySqlConnection conn = new MySqlConnection(strConn);
        conn.Open();

        string query = "update webservice.customer, webservice.user set webservice.customer.name = '" + newName + "'" +
                       "where webservice.user.username = '" + username + "'" +
                       "and webservice.user.idCustomer = webservice.customer.idCustomer";

        MySqlCommand cmd = new MySqlCommand(query, conn);
        return cmd.ExecuteNonQuery();
    }

    [WebMethod]
    public int updateAddress(string username, string newAddress)
    {
        string strConn = "server=localhost;database=webservice;user=root;pwd=123456";
        MySqlConnection conn = new MySqlConnection(strConn);
        conn.Open();

        string query = "update webservice.customer, webservice.user set webservice.customer.address = '" + newAddress + "'" +
                       "where webservice.user.username = '" + username + "'" +
                       "and webservice.user.idCustomer = webservice.customer.idCustomer";

        MySqlCommand cmd = new MySqlCommand(query, conn);
        return cmd.ExecuteNonQuery();
    }


    [WebMethod]
    public int updatePhoneNumber(string username, string newPhone)
    {
        string strConn = "server=localhost;database=webservice;user=root;pwd=123456";
        MySqlConnection conn = new MySqlConnection(strConn);
        conn.Open();

        string query = "update webservice.customer, webservice.user set webservice.customer.phoneNumber = '" + newPhone + "'" +
                       "where webservice.user.username = '" + username + "'" +
                       "and webservice.user.idCustomer = webservice.customer.idCustomer";

        MySqlCommand cmd = new MySqlCommand(query, conn);
        return cmd.ExecuteNonQuery();
    }

    [WebMethod]
    public int getNumberOfFoodType()
    {
        Connection connection = new Connection();
        int count = connection.getCount("select count(*) from FoodType");
        connection.closeConnection();
        return count;
    }

    //[WebMethod]
    //public List<FoodType> getFoodType()
    //{
    //    List<FoodType> list = new List<FoodType>();
    //    Connection connection = new Connection();
    //    MySqlDataReader data = connection.getData("select * from FoodType");

    //    while (data.Read())
    //    {
    //        FoodType foodType = new FoodType();
    //        foodType.FoodTypeId = data.GetInt32(0);
    //        foodType.FoodTypeName = data.GetString(1);
    //        list.Add(foodType);
    //    }

    //    connection.closeConnection();
    //    return list;
    //}

    //[WebMethod]
    //public int getNumberOfFood()
    //{
    //    Connection connection = new Connection();
    //    int count = connection.getCount("select count(*) from Food");
    //    connection.closeConnection();
    //    return count;
    //}

    //[WebMethod]
    //public List<Food> getFood()
    //{
    //    List<Food> list = new List<Food>();
    //    Connection connection = new Connection();
    //    MySqlDataReader data = connection.getData("select * from Food");

    //    while (data.Read())
    //    {
    //        Food food = new Food();
    //        food.FoodId = data.GetInt32(0);
    //        food.FoodName = data.GetString(1);
    //        food.FoodImage = data.GetString(2);
    //        food.FoodDescription = data.GetString(3);
    //        food.FoodPrice = data.GetInt32(4);
    //        food.FoodTypeId = data.GetString(5);
    //        list.Add(food);
    //    }

    //    connection.closeConnection();
    //    return list;
    //}

    //[WebMethod]
    //public List<Food> getFoodByFoodType(int foodTypeId)
    //{
    //    List<Food> list = new List<Food>();
    //    Connection connection = new Connection();
    //    MySqlDataReader data = connection.getData("select * from Food where FoodTypeId = " + foodTypeId);

    //    while (data.Read())
    //    {
    //        Food food = new Food();
    //        food.FoodId = data.GetInt32(0);
    //        food.FoodName = data.GetString(1);
    //        food.FoodImage = data.GetString(2);
    //        food.FoodDescription = data.GetString(3);
    //        food.FoodPrice = data.GetInt32(4);
    //        food.FoodTypeId = data.GetString(5);
    //        list.Add(food);
    //    }

    //    connection.closeConnection();
    //    return list;
    //}

    //[WebMethod]
    //public Food getFoodByFoodId(int foodId)
    //{
    //    Food food = new Food();
    //    Connection connection = new Connection();
    //    MySqlDataReader data = connection.getData("select * from Food where FoodId = " + foodId);

    //    while (data.Read())
    //    {
    //        food.FoodId = data.GetInt32(0);
    //        food.FoodName = data.GetString(1);
    //        food.FoodImage = data.GetString(2);
    //        food.FoodDescription = data.GetString(3);
    //        food.FoodPrice = data.GetInt32(4);
    //        food.FoodTypeId = data.GetString(5);
    //    }

    //    connection.closeConnection();
    //    return food;
    //}

    //public List<FoodRating> getFoodRate()
    //{
    //    List<FoodRating> list = new List<FoodRating>();
    //    Connection connection = new Connection();
    //    MySqlDataReader data = connection.getData("select * from FoodRating");

    //    while (data.Read())
    //    {
    //        FoodRating foodRating = new FoodRating();
    //        foodRating.RateId = data.GetInt32(0);
    //        foodRating.FoodId = data.GetString(1);
    //        foodRating.FoodRate = data.GetDouble(2);
    //        foodRating.FoodComment = data.GetString(3);
    //        list.Add(foodRating);
    //    }

    //    connection.closeConnection();
    //    return list;
    //}

    //[WebMethod]
    //public List<FoodRating> getFoodRateByFoodId(int foodId)
    //{
    //    List<FoodRating> list = new List<FoodRating>();
    //    Connection connection = new Connection();
    //    MySqlDataReader data = connection.getData("select * from FoodRating " +
    //        "where FoodId = '" + foodId + "';");

    //    while (data.Read())
    //    {
    //        FoodRating foodRating = new FoodRating();
    //        foodRating.RateId = data.GetInt32(0);
    //        foodRating.FoodId = data.GetString(1);
    //        foodRating.FoodRate = data.GetDouble(2);
    //        foodRating.FoodComment = data.GetString(3);
    //        list.Add(foodRating);
    //    }

    //    connection.closeConnection();
    //    return list;
    //}

    //[WebMethod]
    //public int addFoodRating(int foodId, float foodRate, string foodComment)
    //{
    //    string sql = "insert into FoodRating(FoodId, FoodRate, FoodComment) " +
    //        "values ('" + foodId + "', " + foodRate + ", '" + foodComment + "');";
    //    Connection connection = new Connection();
    //    return connection.exeNonQuery(sql);
    //}

    //[WebMethod]
    //public int delFoodRating(int rateId)
    //{
    //    List<FoodRating> foodRateList = getFoodRate();

    //    foreach (FoodRating ft in foodRateList)
    //    {
    //        if (ft.RateId == rateId)
    //        {
    //            string sql = "delete from FoodRating where RateId = '" + rateId + "'";
    //            Connection connection = new Connection();
    //            return connection.exeNonQuery(sql);
    //        }
    //    }

    //    return -1;
    //}

    [WebMethod]
    public double avgFoodRate(int foodId)
    {
        Connection connection = new Connection();
        double avgRate = connection.getDouble("select avg(FoodRate) AvgRate from FoodRating " +
            "where FoodId = '" + foodId + "';");
        connection.closeConnection();
        return avgRate;
    }
}
