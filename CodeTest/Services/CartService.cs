using System;
using System.Collections.Generic;
using System.Linq;
using CodeTest.Entities;
using CodeTest.Helpers;

namespace CodeTest.Services
{
    public interface ICartService
    {
        CartItem Create(CartItem product);
        List<CartItem> GetUserCart(int userId);
        CartItem Update(CartItem cartItem);
    }

    public class CartService : ICartService
    {
        private DataContext _context;

        public CartService(DataContext context)
        {
            _context = context;
        }
        public CartItem Create(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();

            return cartItem;
        }

        public List<CartItem> GetUserCart(int userId)
        {
            return _context.CartItems.Where(x => x.UserId == userId ).ToList();
        }

        public CartItem Update(CartItem cartItem)
        {
            var itm = _context.CartItems.FirstOrDefault(x => x.UserId == cartItem.UserId && x.ProductId == cartItem.ProductId && x.Quantity >= cartItem.Quantity);
            if (itm != null)
            {
                itm.Quantity = itm.Quantity - cartItem.Quantity;
                if (itm.Quantity > 0)
                    _context.CartItems.Update(itm);
                else
                    _context.CartItems.Remove(itm);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Invalid Request");
            }
            return cartItem;
        }

    }
}