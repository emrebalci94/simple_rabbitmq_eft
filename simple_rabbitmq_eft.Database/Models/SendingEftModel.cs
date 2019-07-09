using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simple_rabbitmq_eft.Database.Models
{
    public class SendingEftModel
    {
        public int FromId { get; set; }
        public int ToId { get; set; }
        public double Money { get; set; }
    }
}
