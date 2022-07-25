using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace thewebservice
{
    [Serializable]
    public class Food
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public string FoodImage { get; set; }
        public string FoodDescription { get; set; }
        public int FoodPrice { get; set; }
        public string FoodTypeId { get; set; }

    }
}