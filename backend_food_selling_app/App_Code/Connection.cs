using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

    public class Connection
    {
        private string conn =
            "Server=localhost;Database=android;uid=root;password=";
        private MySqlConnection mysqlConnection = null;

        public Connection()
        {
            mysqlConnection = new MySqlConnection(conn);
        }

        public void openConnection()
        {
            if (mysqlConnection.State == ConnectionState.Closed)
                mysqlConnection.Open();
        }

        public void closeConnection()
        {
            if (mysqlConnection.State == ConnectionState.Open)
                mysqlConnection.Close();
        }

        public MySqlDataReader getData(string sql)
        {
            MySqlCommand command = new MySqlCommand(sql);
            command.Connection = mysqlConnection;
            openConnection();

            MySqlDataReader dataReader = command.ExecuteReader();
            return dataReader;
        }

        public int getCount(string sql)
        {
            MySqlCommand command = new MySqlCommand(sql);
            command.Connection = mysqlConnection;
            openConnection();

            object obj = command.ExecuteScalar();
            return Convert.ToInt32(obj);
        }

        public double getDouble(string sql)
        {
            MySqlCommand command = new MySqlCommand(sql);
            command.Connection = mysqlConnection;
            openConnection();

            object obj = command.ExecuteScalar();
            return Convert.ToDouble(obj);
        }

        public int exeNonQuery(string sql)
        {
            MySqlCommand command = new MySqlCommand(sql);
            command.Connection = mysqlConnection;
            openConnection();

            object obj = command.ExecuteNonQuery();
            closeConnection();
            return (int) obj;
        }
    }