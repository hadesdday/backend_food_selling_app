using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[Serializable]
public class Food
{
    public int id { get; set; }
    public int food_type { get; set; }
    public string name { get; set; }
    public string image_url { get; set; }
    public string description { get; set; }
    public double price { get; set; }

    }