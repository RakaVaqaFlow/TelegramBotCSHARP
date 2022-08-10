using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp15
{
    internal class Product
    {
        public string Name;
        public string ProductId;
        public int Count;
        public double Price;
        public Product(string name_, string productId_, int count_, double price_)
        {
            Name = name_;
            ProductId = productId_;
            Count = count_;
            Price = price_;
        }
    }
}
