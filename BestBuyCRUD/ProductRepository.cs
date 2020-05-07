using System;
using System.Data; // Lets us use IDbConnection
using Dapper;

namespace BestBuyCRUD
{
    public class ProductRepository : IProductRepository //ERROR because we need to implement stubbed-out GetAllProducts()
    {
        private readonly IDbConnection _connection; // _private indicator
        // constructor here will do some setup work for us
        public ProductRepository(IDbConnection connection)
        {
            _connection = connection; //taking this _connection and hiding it from outside world (encapsulation)
        }

        public IEnumerable<Product> GetAllProducts() //***instance of Object Relational Mapping (SEE VIDEO FOR BREAKDOWN)
        {
           return _connection.Query<Product>("SELECT * FROM Products;");
        }//Method takes in a Query, sends to database, returns info to us (SEE VIDEO)

    }
    // GetAllProducts() will need an instance of the ProductRepository class

    //Next, we do CREATE, so we'll use Execute
    public void CreateProduct(string name, double price, int categoryID)
    {
        _connection.Execute("INSERT INTO Products (Name, Price, CategoryID) VALUES (@name, @price, @categoryID);",
            new { name = name, price = price, categoryID = categoryID });
    }
}
