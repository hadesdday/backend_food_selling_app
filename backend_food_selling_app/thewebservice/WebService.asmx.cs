using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace thewebservice
{
    /// <summary>
    /// Summary description for webservice
    /// </summary>
    [WebService(Namespace = "http://192.168.0.177:44321/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class webservice : System.Web.Services.WebService
    {
        public webservice() { }

        [WebMethod]
        public int getNumberOfFoodType()
        {
            Connection connection = new Connection();
            int count = connection.getCount("select count(*) from FoodType");
            connection.closeConnection();
            return count;
        }

        [WebMethod]
        public List<FoodType> getFoodType()
        {
            List<FoodType> list = new List<FoodType>();
            Connection connection = new Connection();
            MySqlDataReader data = connection.getData("select * from FoodType");

            while (data.Read())
            {
                FoodType foodType = new FoodType();
                foodType.FoodTypeId = data.GetInt32(0);
                foodType.FoodTypeName = data.GetString(1);
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
                if (ft.FoodTypeName.Equals(foodTypeName))
                {
                    return -1;
                }
            }

        string sql = "insert into FoodType(FoodTypeName) values ('" + foodTypeName + "')";
        Connection connection = new Connection();
        return connection.exeNonQuery(sql);
        }

        [WebMethod]
        public int delFoodType(int foodTypeId)
        {
            List<FoodType> foodTypeList = getFoodType();

            foreach (FoodType ft in foodTypeList)
            {
                if (ft.FoodTypeId == foodTypeId)
                {
                    string sql = "delete from FoodType where FoodTypeId = '" + foodTypeId + "'";
                    Connection connection = new Connection();
                    return connection.exeNonQuery(sql);
                }
            }

            return -1;
        }

        [WebMethod]
        public int updateFoodType(int foodTypeId, string foodTypeName)
        {
            List<FoodType> foodTypeList = getFoodType();

            foreach (FoodType ft in foodTypeList)
            {
                if (ft.FoodTypeId == foodTypeId)
                {
                    string sql = "update FoodType" +
                        " set FoodTypeName = '" + foodTypeName +
                        "' where FoodTypeId = '" + foodTypeId + "'";
                    Connection connection = new Connection();
                    return connection.exeNonQuery(sql);
                }
            }

            return -1;
        }

        [WebMethod]
        public int getNumberOfFood()
        {
            Connection connection = new Connection();
            int count = connection.getCount("select count(*) from Food");
            connection.closeConnection();
            return count;
        }

        [WebMethod]
        public List<Food> getFood()
        {
            List<Food> list = new List<Food>();
            Connection connection = new Connection();
            MySqlDataReader data = connection.getData("select * from Food");

            while (data.Read())
            {
                Food food = new Food();
                food.FoodId = data.GetInt32(0);
                food.FoodName = data.GetString(1);
                food.FoodImage = data.GetString(2);
                food.FoodDescription = data.GetString(3);
                food.FoodPrice = data.GetInt32(4);
                food.FoodTypeId = data.GetString(5);
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
            MySqlDataReader data = connection.getData("select * from Food where FoodTypeId = " + foodTypeId);

            while (data.Read())
            {
                Food food = new Food();
                food.FoodId = data.GetInt32(0);
                food.FoodName = data.GetString(1);
                food.FoodImage = data.GetString(2);
                food.FoodDescription = data.GetString(3);
                food.FoodPrice = data.GetInt32(4);
                food.FoodTypeId = data.GetString(5);
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
            MySqlDataReader data = connection.getData("select * from Food where FoodId = " + foodId);

            while (data.Read())
            {
                food.FoodId = data.GetInt32(0);
                food.FoodName = data.GetString(1);
                food.FoodImage = data.GetString(2);
                food.FoodDescription = data.GetString(3);
                food.FoodPrice = data.GetInt32(4);
                food.FoodTypeId = data.GetString(5);
            }

            connection.closeConnection();
            return food;
        }

        [WebMethod]
        public int addFood(string foodName, string foodImage, string foodDescription,
            int foodPrice, int foodTypeId)
        {
            List<Food> foodList = getFood();

            foreach (Food ft in foodList)
            {
                if (ft.FoodName == foodName)
                {
                    return -1;
                }
            }

            string sql = "insert into Food(FoodName, FoodImage, FoodDescription, " +
                "FoodPrice, FoodTypeId) values ('" +
                foodName + "', '" +
                foodImage + "', '" +
                foodDescription + "', " +
                foodPrice + ", '" + 
                foodTypeId + "')";
            Connection connection = new Connection();
            return connection.exeNonQuery(sql);
        }

        [WebMethod]
        public int delFood(int foodId)
        {
            List<Food> foodList = getFood();

            foreach (Food ft in foodList)
            {
                if (ft.FoodId == foodId)
                {
                    string sql = "delete from Food where FoodId = '" + foodId + "'";
                    Connection connection = new Connection();
                    return connection.exeNonQuery(sql);
                }
            }

            return -1;
        }

        [WebMethod]
        public int updateFood(int foodId, string foodName, string foodImage, string foodDescription, int foodPrice)
        {
            List<Food> foodList = getFood();

            foreach (Food ft in foodList)
            {
                if (ft.FoodId == foodId)
                {
                    string sql = "update Food" +
                        " set FoodName = '" + foodName +
                        "', FoodImage = '" + foodImage +
                        "', FoodDescription = '" + foodDescription + 
                        "', FoodPrice = " + foodPrice + 
                        " where FoodId = '" + foodId + "'";
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
            MySqlDataReader data = connection.getData("select * from FoodRating");

            while (data.Read())
            {
                FoodRating foodRating = new FoodRating();
                foodRating.RateId = data.GetInt32(0);
                foodRating.FoodId = data.GetString(1);
                foodRating.FoodRate = data.GetDouble(2);
                foodRating.FoodComment = data.GetString(3);
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
            MySqlDataReader data = connection.getData("select * from FoodRating " +
                "where FoodId = '" + foodId + "';");

            while (data.Read())
            {
                FoodRating foodRating = new FoodRating();
                foodRating.RateId = data.GetInt32(0);
                foodRating.FoodId = data.GetString(1);
                foodRating.FoodRate = data.GetDouble(2);
                foodRating.FoodComment = data.GetString(3);
                list.Add(foodRating);
            }

            connection.closeConnection();
            return list;
        }

        [WebMethod]
        public int addFoodRating(int foodId, float foodRate, string foodComment)
        {
            string sql = "insert into FoodRating(FoodId, FoodRate, FoodComment) " +
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
                if (ft.RateId == rateId)
                {
                    string sql = "delete from FoodRating where RateId = '" + rateId + "'";
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
            double avgRate = connection.getDouble("select avg(FoodRate) AvgRate from FoodRating " +
                "where FoodId = '" + foodId + "';");
            connection.closeConnection();
            return avgRate;
        }
    }
}
