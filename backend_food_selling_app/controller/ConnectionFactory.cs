using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySqlConnector;
namespace backend_food_selling_app.controller
{
    public class ConnectionFactory
    {
        private string strConn = "Server=localhost;Database=android;User=root;Port=3306;Password=\"\";SSL Mode = None"; 
        private MySqlConnection sqlConn = null;
        public ConnectionFactory()
        {
            sqlConn = new MySqlConnection(strConn); 
        }
        public void openConnection()
        {
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }
        }

        public void closeConnection()
        {
            if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
            }
        }
        //public MySqlDataReader getData(string sql)
        //{
        //    MySqlCommand cmd = new MySqlCommand(sql);
        //    cmd.Connection = sqlConn;
        //    openConnection();
        //    SqlDataReader dataReader=cmd.ExecuteReader();
        //    return dataReader;
        //}
        //public int getCount(string sql)
        //{
        //    SqlCommand cmd = new SqlCommand(sql);
        //    cmd.Connection=sqlConn;
        //    openConnection();
        //    object o=cmd.ExecuteScalar();
        //    return (int)(o);
        //}
    }
}