using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MultiUserMVC.Models
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }

        public string Symbol { set; get; }
        public string Last_Price { set; get; }
        public string Change { set; get; }
        public string Change_Percentage { set; get; }
        public string Currency { set; get; }
        public string Market_Time { set; get; }
        public string Volume { set; get; }
        public string Average_Volume { set; get; }
        public string Market_Cap { set; get; }
        public string ScrapeDate { set; get; }

    }
}
