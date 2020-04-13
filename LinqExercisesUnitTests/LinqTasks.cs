using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Castle.Core.Internal;
using LinqExercises;

namespace LinqExercisesUnitTests
{
    class LinqTasks
    {
        IDataSource dataSource;
        public LinqTasks(IDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        //Task 1
        public IEnumerable<Customer> CustomersExceedTotalOrdersValue(decimal OrderValue = 1234M)
        {
            return dataSource.Customers.Where(customer => 
                        customer.Orders != null && customer.Orders.Length > 0)
                        .Where(customer => customer.Orders.Sum(h => h.Total) > OrderValue);
        }
        //Task 2
        public IEnumerable<IGrouping<Customer, IEnumerable<Supplier>>> SuppliersForCustomersByCountryNCity()
        {
            return dataSource.Customers.GroupJoin(dataSource.Suppliers,
                                            outer => (outer.City, outer.Country),
                                            inner => (inner.City, inner.Country),
                                            (outer, inner) => new
                                            {
                                                customer = outer,
                                                supplier = inner
                                            }).GroupBy(gr => gr.customer, gr => gr.supplier); 
        }
        //task 3
        public IEnumerable<Customer> CustomersExceedOrderValue(decimal OrderValue = 123M)
        {
            return dataSource.Customers.Where(w => w.Orders != null && w.Orders.Length > 0).Where(w => w.Orders.Any(s => s.Total > OrderValue)); 
        }
        //task 4
        public IEnumerable<IGrouping<DateTime, Customer>> RegistrationDate()
        {
            var res = dataSource.Customers.Where(w => w.Orders != null && w.Orders.Length > 0)
                .GroupBy(gr => gr.Orders.Min(o => o.OrderDate));
            //anonymous type without grouping
            //var res2 = dataSource.Customers.Where(w => w.Orders.Length > 0)
            //    .Select(s => new { customer = s, registered = s.Orders.Min(o => o.OrderDate) });
            return res;
        }
        //task 5
        public IEnumerable<IGrouping<DateTime, Customer>> OrderedRegistrationDate()
        {
            var res = dataSource.Customers.Where(w => w.Orders != null && w.Orders.Length > 0)
                .OrderBy(orderby => orderby.Orders.Min(o => o.OrderDate).Year)
                .ThenBy(t => t.Orders.Min(o => o.OrderDate).Month)
                .ThenBy(t => t.Orders.Sum(sum => sum.Total))
                .ThenBy(t => t.CompanyName)
                .GroupBy(gr => gr.Orders.Min(o => o.OrderDate));
            return res;
        }
        //task 6
        public IEnumerable<Customer> CustomersWithWrongData()
        {
            return dataSource.Customers.Where(w => !w.Phone.StartsWith("(")
                                                 || w.PostalCode.Any(a => !Char.IsDigit(a))
                                                 || String.IsNullOrWhiteSpace(w.Region));
        }
        //task 7
        public IEnumerable<IGrouping<string, IEnumerable<IGrouping<int, Product>>>> GroupedProducts()
        {
            return dataSource.Products
                .GroupBy(g => g.Category)
                .Select(s => new
                {
                    key = s.Key,
                    items = s.OrderBy(o=>o.UnitPrice).GroupBy(g => g.UnitsInStock)
                })
                .GroupBy(gr => gr.key, gr => gr.items);
        }
        //task 8
        public IEnumerable<IGrouping<decimal, Product>> ProductCategories() 
        {
            var priceComparer = new PriceComparer();
            var res = dataSource.Products.GroupBy(g => g.UnitPrice, priceComparer);
            return res;
        }
        static void MarginNIntensity()
        {
            var dataSource = new DataSource();
            var result = dataSource.Customers.GroupBy(g => g.City).Select(s => new
            {
                city = s.Key,
                margin = s.Where(w => !(w.Orders.Length > 0)).Select(y => y.Orders.Average(i => i.Total)).Average(),
                intensity = s.Average(f => f.Orders.Count())
            });
        }
    }
}
