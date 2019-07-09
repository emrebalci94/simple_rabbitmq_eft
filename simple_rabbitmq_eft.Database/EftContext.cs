using Newtonsoft.Json;
using simple_rabbitmq_eft.Database.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace simple_rabbitmq_eft.Database
{
    public class EftContext : IEft
    {
        private readonly List<Customer> _customers;
        public EftContext()
        {
            var file = File.ReadAllText("Database.json");

            if (string.IsNullOrEmpty(file))
            {
                var serialize = JsonConvert.SerializeObject(SampleCustomers);
                File.WriteAllText("Database.json", serialize);
                file = serialize;
            }

            _customers = JsonConvert.DeserializeObject<List<Customer>>(file);
        }


        #region İlk Datalar

        private List<Customer> SampleCustomers => new List<Customer>
        {
            new Customer
            {
                Id = 1,
                Name = "Test",
                Balance = 20.2
            },
            new Customer
            {
                Id = 2,
                Name = "Test2",
                Balance = 30.5
            },
            new Customer
            {
                Id = 3,
                Name = "Test3",
                Balance = 220
            },  new Customer
            {
                Id = 4,
                Name = "Test4",
                Balance =420
            },
            new Customer
            {
                Id = 5,
                Name = "Test5",
                Balance = 122.522
            }
        };

        #endregion


        public Customer GetCustomer(int userId)
        {
            return _customers.FirstOrDefault(p => p.Id == userId);
        }

        public void Send(SendingEftModel model)
        {
            var fromCustomer = _customers.FirstOrDefault(p => p.Id == model.FromId);
            if (fromCustomer == null)
            {
                throw new ArgumentNullException("From Customer Not Found");
            }

            var toCustomer = _customers.FirstOrDefault(p => p.Id == model.ToId);
            if (toCustomer == null)
            {
                throw new ArgumentNullException("To Customer Not Found");
            }

            if (fromCustomer.Balance < model.Money)
            {
                throw new ArgumentNullException("Insufficient balance");
            }

            fromCustomer.Balance -= model.Money;
            toCustomer.Balance += model.Money;

            var serialize = JsonConvert.SerializeObject(_customers);
            File.WriteAllText("Database.json", serialize);
        }
    }
}
