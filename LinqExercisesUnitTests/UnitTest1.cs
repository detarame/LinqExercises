using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinqExercises;
using Moq;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Castle.Core.Internal;

namespace LinqExercisesUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        Mock<IDataSource> DataSourceMock;
        LinqTasks TasksClass;
        public UnitTest1()
        {
            DataSourceMock = new Mock<IDataSource>();            
            TasksClass = new LinqTasks(DataSourceMock.Object);

            var originalDataSource = new DataSource();

            DataSourceMock.Setup(d => d.Customers).Returns(new List<Customer> {
                        new Customer{
                                CustomerID = "ASDA",
                                CompanyName = "METRO",
                                Address = "Kukuevo st. 24",
                                City = "Osaka",
                                Region = " ",
                                PostalCode = "234142",
                                Country = "Japan",
                                Phone = "(8)8805553535",
                                Fax = "fox",
                                Orders = null
                        },
                        new Customer{
                                CustomerID = "POWR",
                                CompanyName = "CeReal",
                                Address = "Seevo st. 24",
                                City = "Göteborg",
                                Region = "region",
                                PostalCode = "234142",
                                Country = "Germany",
                                Phone = "(8)8805553535",
                                Fax = "23422423",
                                Orders =  new Order[]
                                {
                                    new Order{
                                    OrderID = 6234,
                                    OrderDate = DateTime.Parse("08/01/1999", CultureInfo.InvariantCulture),
                                    Total = 2M
                                    },
                                    new Order{
                                    OrderID = 6234,
                                    OrderDate = DateTime.Parse("07/02/1999",  CultureInfo.InvariantCulture),
                                    Total = 2M
                                    }
                                }
                        },
                        new Customer{
                                CustomerID = "SMTH",
                                CompanyName = "Backpack",
                                Address = "Wonder st. 7",
                                City = "Göteborg",
                                Region = "region",
                                PostalCode = "234142",
                                Country = "NotGermany",
                                Phone = " ",
                                Fax = "23422423",
                                Orders =  new Order[]
                                {
                                    new Order{
                                    OrderID = 6234,
                                    OrderDate = DateTime.Parse("06/03/1999", CultureInfo.InvariantCulture),
                                    Total = 2M
                                    },
                                    new Order{
                                    OrderID = 6234,
                                    OrderDate = DateTime.Parse("05/04/1999",  CultureInfo.InvariantCulture),
                                    Total = 2M
                                    }
                                }
                        },
                        new Customer{
                                CustomerID = "SAMPLE",
                                CompanyName = "Sample",
                                Address = "Sample st. 2",
                                City = "Sample",
                                Region = "region",
                                PostalCode = "234142o",
                                Country = "USA",
                                Phone = "(8)8805553535",
                                Fax = "23422423",
                                Orders =  new Order[]
                                {
                                    new Order{
                                    OrderID = 6234,
                                    OrderDate = DateTime.Parse("04/05/1999", CultureInfo.InvariantCulture),
                                    Total = 2M
                                    },
                                    new Order{
                                    OrderID = 6234,
                                    OrderDate = DateTime.Parse("03/06/1999",  CultureInfo.InvariantCulture),
                                    Total = 2M
                                    }
                                }
                        },
                        new Customer{
                                CustomerID = "AAAA",
                                CompanyName = "Random",
                                Address = "Saint Spring st. 666",
                                City = "New Orleans",
                                Region = "region",
                                PostalCode = "",
                                Country = "USA",
                                Phone = "(8)8805553535",
                                Fax = "23422423",
                                Orders =  new Order[]
                                {
                                    new Order{
                                    OrderID = 6234,
                                    OrderDate = DateTime.Parse("02/07/1999", CultureInfo.InvariantCulture),
                                    Total = 2M
                                    },
                                    new Order{
                                    OrderID = 6234,
                                    OrderDate = DateTime.Parse("01/08/1999",  CultureInfo.InvariantCulture),
                                    Total = 2M
                                    }
                                }
                        }

            }) ;

            DataSourceMock.Setup(s => s.Products).Returns( new List<Product> {
                    new Product { ProductID = 1, ProductName = "Chai", Category = "Tea", UnitPrice = 18.0000M, UnitsInStock = 39 },
                    new Product { ProductID = 2, ProductName = "Chang", Category = "Tea", UnitPrice = 19.0000M, UnitsInStock = 17 },
                    new Product { ProductID = 3, ProductName = "Aniseed Syrup", Category = "Sweets", UnitPrice = 10.0000M, UnitsInStock = 13 },
                    new Product { ProductID = 4, ProductName = "Cajun Seasoning", Category = "Sweets", UnitPrice = 22.0000M, UnitsInStock = 13 },
                    new Product { ProductID = 5, ProductName = "Gumbo Mix", Category = "Sweets", UnitPrice = 21.3500M, UnitsInStock = 0 }
            });
            DataSourceMock.Setup(s => s.Suppliers).Returns(new List<Supplier>(){
                    new Supplier {SupplierName = "Exotic Liquids", Address = "49 Gilbert St.", City = "Göteborg", Country = "Germany"},
                    new Supplier {SupplierName = "New Orleans Cajun Delights", Address = "P.O. Box 78934", City = "New Orleans", Country = "USA"},
                    new Supplier {SupplierName = "Grandma Kelly's Homestead", Address = "707 Oxford Rd.", City = "Ann Arbor", Country = "USA"},
                    new Supplier {SupplierName = "Tokyo Traders", Address = "9-8 Sekimai Musashino-shi", City = "Tokyo", Country = "Japan"},
                    new Supplier {SupplierName = "Cooperativa de Quesos 'Las Cabras'", Address = "Calle del Rosal 4", City = "Oviedo", Country = "Spain"},
                    new Supplier {SupplierName = "Mayumi's", Address = "92 Setsuko Chuo-ku", City = "Osaka", Country = "Japan"}
            });
        }
        [TestMethod]
        public void SuppliersForCustomersByCountryNCity_EqualOnlyByCity_False()
        {
            //arrange
            //act 
            var Result = TasksClass.SuppliersForCustomersByCountryNCity().Where(w => !w.All(a => a.IsNullOrEmpty())).ToList();

            Assert.IsTrue(Result.Where(w =>w.Key.City.Equals("Göteborg")).Count() == 1);
        }
        [TestMethod]
        public void SuppliersForCustomersByCountryNCity_EqualOnlyByCountry_False()
        {
            //arrange
            //act 
            var Result = TasksClass.SuppliersForCustomersByCountryNCity().Where(w => !w.All(a => a.IsNullOrEmpty())).ToList();

            Assert.IsTrue(Result.Where(w => w.Key.Country.Equals("USA")).Count() == 1);
        }
        [TestMethod]
        public void CustomersExceedOrderValue_ContainsAllWithoutNull()
        {
            //arrange
            var Expected = DataSourceMock.Object.Customers.Where(w => !w.Orders.IsNullOrEmpty()).ToList();
            //act 
            var Result = TasksClass.CustomersExceedOrderValue(0M).ToList();

            Assert.IsTrue(Result.Count == Expected.Count);
        }
        [TestMethod]
        public void CustomersExceedTotalOrdersValue_DoesntContainNull()
        {
            //arrange
            var NullItems = DataSourceMock.Object.Customers.Where(w => w.Orders.IsNullOrEmpty()).ToList();

            //act 
            var Result = TasksClass.CustomersExceedTotalOrdersValue(0M).ToList();
            
            Assert.IsFalse(Result.Any(a => NullItems.Contains(a)));
        }
        [TestMethod]
        public void CustomersWithWrongData_DoesntContainCorrectData()
        {
            //arrange
            var Expected = DataSourceMock.Object.Customers.Find(w =>w.CustomerID.Equals("POWR"));
            //act 
            var Result = TasksClass.CustomersWithWrongData().ToList();

            Assert.IsTrue(!Result.Contains(Expected));
        }
        [TestMethod]
        public void OrderedRegistrationDate_CorrectDateCast()
        {
            //arrange
            //act 
            var Result = TasksClass.OrderedRegistrationDate().ToList();

            Assert.IsTrue(Result.First().Key.Day.Equals(8));
        }
        [TestMethod]
        public void RegistrationDate_DoesntContainNullDate()
        {
            //arrange
            var Expected = new Customer
            {
                CustomerID = "ASDA",
                CompanyName = "METRO",
                Address = "Kukuevo st. 24",
                City = "Osaka",
                Region = " ",
                PostalCode = "234142",
                Country = "Japan",
                Phone = "(8)8805553535",
                Fax = "fox",
                Orders = null
            };
            //act 
            var Result = TasksClass.RegistrationDate().ToList();

            Assert.IsTrue(Result.Where(s => s.Contains(Expected)).IsNullOrEmpty());
        }
        [TestMethod]
        public void GroupedProducts_GetsGroupList_Equals()
        {
            //arrange
            List<string> Expected = new List<string> { "Tea", "Sweets" };
            //act 
            var Result = TasksClass.GroupedProducts().ToList();

            Assert.IsTrue(Result.Select(c => c.Key).ToList().SequenceEqual(Expected));
        }
        [TestMethod]
        public void ProductCategories_NoExpensiveItems()
        {
            //act 
            var Result = TasksClass.ProductCategories().ToList();

            Assert.IsTrue(Result.Where(w => w.Key > 30M).IsNullOrEmpty());
        }
        [TestMethod]
        public void MarginNIntensity_()
        {
            Assert.Fail("Not implemented");
        }
    }
}
