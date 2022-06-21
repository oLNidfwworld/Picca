using System;
using System.Collections.Generic;
using System.Text;

namespace Picca.Models
{
    class Basket
    {
        public int user_id { get; set; }
        public int id { get; set; }
        public int price { get; set; }
        public string Name { get; set; }
        public string imgFood { get; set; }
        public int count { get; set; }
    }
}
