using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manual.Movement.Manager.Domain.Product
{
    public class Product
    {
        public Product(
            string description, 
            char status)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Status = status;
        }

        public string Id { get; }
        public string Description { get; private set; }
        public char Status { get; private set; }
    }
}
