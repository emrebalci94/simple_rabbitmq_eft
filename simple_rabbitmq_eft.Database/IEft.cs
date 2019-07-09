using System;
using System.Collections.Generic;
using System.Text;
using simple_rabbitmq_eft.Database.Models;

namespace simple_rabbitmq_eft.Database
{
    public interface IEft
    {
        void Send(SendingEftModel model);

        Customer GetCustomer(int userId);
    }
}
