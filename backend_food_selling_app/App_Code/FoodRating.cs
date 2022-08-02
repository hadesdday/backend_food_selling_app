using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[Serializable]
public class FoodRating
{
    public int id { get; set; }
    public int food_id { get; set; }
    public double rate { get; set; }
    public string comment { get; set; }
}