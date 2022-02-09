using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public class CartItem {
        public string sku;
        public byte qty;
    }

    public class Terminal : ITerminal
    {
        protected ICollection<CartItem> _cart = new List<CartItem>();

        public void Scan(string item)
        {
            var cartItem = _cart.FirstOrDefault((c) => c.sku == item);
            if (cartItem == null)
            {
                cartItem = new CartItem() { sku = item, qty = 0 };
                _cart.Add(cartItem);
            }
            cartItem.qty++;
        }

        public decimal Total()
        {
            return _cart.Select((c) => ProductsHelper.GetTotalByProduct(c.sku, c.qty)).Sum();
        }
    }
}
