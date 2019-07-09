using System;
using System.Collections.Generic;
using System.Text;

namespace simple_rabbitmq_eft.Database.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
    }
}
