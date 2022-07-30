using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    [Serializable]
    public class FoodRating
    {
        public int RateId { get; set; }
        public string FoodId { get; set; }
        public double FoodRate { get; set; }
        public string FoodComment { get; set; }
    }