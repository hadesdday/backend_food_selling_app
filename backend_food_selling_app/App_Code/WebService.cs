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
        string strConnection = "server=localhost;uid=root;pwd=;database=android;";

        MySqlConnection conn = new MySqlConnection(strConnection);
        conn.Open();
        string sql = "select a.masp,a.tensp,a.gia,c.soluong from sanpham a,hoadon b,chitiethoadon c where b.mahoadon=c.mahd and a.masp=c.masp and c.mahd=@mahoadon";
        MySqlCommand cmd = conn.CreateCommand();
        MySqlParameter mahoadon = new MySqlParameter("@mahoadon", MySqlDbType.Int32);
        mahoadon.Value = idBill;
        cmd.CommandText = sql;
        cmd.Parameters.Add(mahoadon);

        MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Product product = new Product();
            product.masp = reader.GetInt32(0);
            product.tensp = reader.GetString(1);
            product.gia = reader.GetDouble(2);
            product.soluongmua = reader.GetInt32(3);
            products.Add(product);
        }
        conn.Close();
        return products;
    }
    [WebMethod]
    public List<Bill> getBillList(int trangthai)
    {
        List<Bill> bills = new List<Bill>();
        string strConnection = "server=localhost;uid=root;pwd=;database=android;";

        MySqlConnection conn = new MySqlConnection(strConnection);
        conn.Open();
        string sql = "select mahoadon,ngayhoadon from hoadon where trangthai = @trangthai";
        MySqlCommand cmd = conn.CreateCommand();
        MySqlParameter tt = new MySqlParameter("@trangthai", MySqlDbType.Int32);
        tt.Value = trangthai;
        cmd.CommandText = sql;
        cmd.Parameters.Add(tt);

        MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Bill b = new Bill();
            b.mahoadon = reader.GetInt32(0);
            b.dateBill = reader.GetString(1);
            bills.Add(b);
        }
        conn.Close();
        return bills;
    }
    [WebMethod]
    public int deleteIteminBill(int mahd, int masp)
    {
        List<Bill> bills = new List<Bill>();
        string strConnection = "server=localhost;uid=root;pwd=;database=android;";

        MySqlConnection conn = new MySqlConnection(strConnection);
        conn.Open();
        string sql = "delete  from chitiethoadon where mahd = @mahd and masp=@masp";
        MySqlCommand cmd = conn.CreateCommand();
        MySqlParameter hd = new MySqlParameter("@mahd", MySqlDbType.Int32);
        hd.Value = mahd;
        MySqlParameter sp = new MySqlParameter("@masp", MySqlDbType.Int32);
        sp.Value = masp;
        cmd.CommandText = sql;
        cmd.Parameters.Add(hd);
        cmd.Parameters.Add(sp);
        return cmd.ExecuteNonQuery();
    }
}
