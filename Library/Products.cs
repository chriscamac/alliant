using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /**
     * used to hold a volume price with the qty required to receive the price
     * also will be used for pricing of a single unit as well
     */
    public class VolumePricing
    {
        public readonly byte qtyBreak;
        public readonly decimal priceForQtyBreak;

        public VolumePricing(byte _qtyBreak, decimal _priceForQtyBreak)
        {
            // do not allow a qty of zero
            if (_qtyBreak == 0) qtyBreak = 1;
            qtyBreak = _qtyBreak;
            priceForQtyBreak = _priceForQtyBreak;
        }
    }

    public class Product {
        public readonly string sku;
        public readonly IEnumerable<VolumePricing> volumePricing;

        public Product(string _sku, IEnumerable<VolumePricing> _volumePricing)
        {
            sku = _sku;
            // TODO CCC: create guard against multiple volume pricings with the same qtyBreak
            // and guard against there not being a qty of 1 present
            volumePricing = _volumePricing.OrderByDescending((vp) => vp.qtyBreak);
        }

        /**
         * uses total qty to provide a total price for the product, taking into considerations any available
         * volume pricing discounts
         */
        public decimal GetTotalPrice(int totalQty)
        {
            decimal totalPrice = 0;

            // loop through volume pricing, which is already sorted by qtyBreak descending
            // and give the best price by volume
            // last volume should always be qtyBreak 1, so any remainders are priced appropriately
            foreach (var pricing in volumePricing)
            {
                var intQuotient = totalQty / pricing.qtyBreak; // int / int creates an integer quotient that is always rounded down to whole number
                if (intQuotient >= 1)
                {
                    var units = intQuotient * pricing.qtyBreak;
                    totalPrice += intQuotient * pricing.priceForQtyBreak;
                    totalQty -= units;
                }
            }

            return totalPrice;
        }
    }

    public static class ProductsHelper
    {
        private static IEnumerable<Product> _products = new[] {
            new Product("A", new[] { new VolumePricing(1, 2.00m), new VolumePricing(4, 7.00m) }),
            new Product("B", new[] { new VolumePricing(1, 12.00m) }),
            new Product("C", new[] { new VolumePricing(1, 1.25m), new VolumePricing(6, 6.00m) }),
            new Product("D", new[] { new VolumePricing(1, 0.15m) }),
        };

        /**
         * finds the correct product by sku and uses total qty to provide a total price for the product
         * taking into considerations any available volume pricing discounts
         */
        public static decimal GetTotalByProduct(string sku, int qty)
        {
            var product = _products.First((p) => p.sku == sku);
            return product.GetTotalPrice(qty);
        }
    }
}
