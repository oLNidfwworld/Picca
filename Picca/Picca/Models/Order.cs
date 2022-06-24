using System;
using System.Collections.Generic;
using System.Text;

namespace Picca.Models
{
    public class Order
    {
        public int id_order { get; set; }
        public int order_number { get; set; }
        public int price { get; set; }
        public string date { get; set; }
        public string status { get; set; }
        public int user_id { get; set; }
        
        public string adress { get; set; }

        public string  card { get; set; }
    }
}
