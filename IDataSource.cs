using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExercises
{
    public interface IDataSource
    {
        List<Product> Products { get;  }
        List<Supplier> Suppliers { get; }
        List<Customer> Customers { get; }
    }
}
