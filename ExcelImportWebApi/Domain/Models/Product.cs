
using System;

namespace Domain.Models
{
    public class Product
    {
        public Product(string name, int quantity, decimal priceUnit, DateTime deliveryDate)
        {
            ValidateNameSize(name);
            ValidadeQuantityValue(quantity);
            ValidadePriceUnitValue(priceUnit);
            DeliveryDate = deliveryDate;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();

        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public decimal PriceUnit { get; private set; }
        public DateTime DeliveryDate { get; private set; }


        private void ValidateNameSize(string name)
        {
            if (name.Length > 50)
                throw new ArgumentException("Invalide name size");

            Name = name;

        }

        private void ValidadeQuantityValue(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Invalide quantity value");

            Quantity = quantity;
        }

        private void ValidadePriceUnitValue(decimal priceUnit)
        {
            if (priceUnit <= 0)
                throw new ArgumentException("Invalide price unit value");

            PriceUnit = Math.Round(priceUnit, 2);
        }
    }
}
