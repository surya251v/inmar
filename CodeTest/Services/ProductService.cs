using System;
using System.Collections.Generic;
using System.Linq;
using CodeTest.Entities;
using CodeTest.Helpers;

namespace CodeTest.Services
{
    public interface IProductService
    {
        Product Create(Product product);
    }

    public class ProductService : IProductService
    {
        private DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }
        public Product Create(Product product)
        {
            if (_context.Products.Any(x => x.Name == product.Name))
                throw new AppException("Productname \"" + product.Name + "\" is already taken");
            
            _context.Products.Add(product);
            _context.SaveChanges();

            return product;
        }

    }
}