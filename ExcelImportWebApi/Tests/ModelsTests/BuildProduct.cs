
using Domain.Models;
using System;
using Xunit;

namespace Tests.ModelsTests
{
    public class BuildProduct
    {
        [Fact]
        public void ReturnObjet()
        {
            //arrange
            var nameProduct = "Router";
            var quantity = 10;
            var price = 174.587m;
            var deliveryDate = new DateTime(2021, 02, 12);

            //act
            var product = new Product(name: nameProduct, quantity: quantity, priceUnit: price, deliveryDate: deliveryDate);

            //assert

            Assert.Equal(nameProduct, product.Name);
            Assert.Equal(quantity, product.Quantity);
            Assert.Equal(174.59m, product.PriceUnit);
            Assert.Equal(deliveryDate, product.DeliveryDate);

        }

        [Fact]
        public void ReturnInvalideNameSize()
        {
            //arrange
            var nameProduct = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.";
            var quantity = 10;
            var price = 174.587m;
            var deliveryDate = new DateTime(2021, 02, 12);

            //act
            Action act = () => new Product(name: nameProduct, quantity: quantity, priceUnit: price, deliveryDate: deliveryDate);


            //assert

            ArgumentException exception = Assert.Throws<ArgumentException>(act);

            Assert.Equal("Invalide name size", exception.Message);

        }

        [Fact]
        public void ReturnInvalideQuantityValue()
        {
            //arrange
            var nameProduct = "Router";
            var quantity = 0;
            var price = 174.587m;
            var deliveryDate = new DateTime(2021, 02, 12);

            //act
            Action act = () => new Product(name: nameProduct, quantity: quantity, priceUnit: price, deliveryDate: deliveryDate);


            //assert

            ArgumentException exception = Assert.Throws<ArgumentException>(act);

            Assert.Equal("Invalide quantity value", exception.Message);

        }

        [Fact]
        public void ReturnInvalidePriceUnitValue()
        {
            //arrange
            var nameProduct = "Router";
            var quantity = 10;
            var price = 0m;
            var deliveryDate = new DateTime(2021, 02, 12);

            //act
            Action act = () => new Product(name: nameProduct, quantity: quantity, priceUnit: price, deliveryDate: deliveryDate);


            //assert

            ArgumentException exception = Assert.Throws<ArgumentException>(act);

            Assert.Equal("Invalide price unit value", exception.Message);

        }
    }
}
