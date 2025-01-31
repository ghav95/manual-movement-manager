﻿using System;
using System.Collections.Generic;

namespace Manual.Movement.Manager.Domain.Product
{
    public class Product
    {
        public Product(){}

        public Product(
            string id,
            string description, 
            string status)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Status = status ?? throw new ArgumentNullException(nameof(status));
        }

        public string Id { get; private set; }
        public string Description { get; private set; }
        public string Status { get; private set; }

        public virtual ICollection<ProductCosif.ProductCosif> ProductCosifs { get; private set; }


        public static Product CreateProduct(
            string id, 
            string description, 
            string status,
            List<ProductCosif.ProductCosif> productCosif)
        {
            return new Product(id, description, status)
            {
                ProductCosifs = productCosif
            };                        
        }

    }
}
