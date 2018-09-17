using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestJSONConvertWebApp.Model
{
    public class Customer
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Lastname { get; set; }
        public JSONData JSONData { get; set; }
    }
    public class JSONData
    {
        public int Age { get; set; }
        public string   JobTitle { get; set; }
        public string Comments { get; set; }
        public SubJSONData SubJSONData { get; set; }
    }
    public class SubJSONData
    {
        public string FavoriteColor { get; set; }
        public string FavoriteNUmber { get; set; }
    }
}
