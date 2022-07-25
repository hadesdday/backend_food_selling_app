using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace thewebservice
{
    [Serializable]
    public class FoodType
    {
        public int FoodTypeId { get; set; }
        public string FoodTypeName { get; set; }
    }
}