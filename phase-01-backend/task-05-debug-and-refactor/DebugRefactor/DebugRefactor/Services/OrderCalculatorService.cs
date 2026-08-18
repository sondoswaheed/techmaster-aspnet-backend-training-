using DebugRefactor.Models;

namespace DebugRefactor.Services
{
    public class OrderCalculatorService
    {
        private const decimal TaxRate = 0.14m;

        private const decimal ShippingCost = 50m;
        private const decimal FreeShippingThreshold = 1000m;

        private const decimal SilverDiscountRate = 0.05m;
        private const decimal GoldDiscountRate = 0.10m;
        private const decimal VipDiscountRate = 0.15m;

        public decimal CalculateSubtotal(Order order)
        {
            return order.Price * order.Quantity;
        }

        public decimal CalculateDiscount(Order order, Customer customer)
        {
            decimal subtotal = CalculateSubtotal(order);

            return customer.Type switch
            {
                CustomerType.Regular => 0,
                CustomerType.Silver => subtotal * SilverDiscountRate,
                CustomerType.Gold => subtotal * GoldDiscountRate,
                CustomerType.VIP => subtotal * VipDiscountRate,
                _ => 0
            };
        }

        public decimal CalculateTax(decimal amountAfterDiscount)
        {
            return amountAfterDiscount * TaxRate;
        }

        public decimal CalculateShipping(decimal amountAfterDiscount)
        {
            return amountAfterDiscount >= FreeShippingThreshold
                ? 0
                : ShippingCost;
        }

        public decimal CalculateFinalTotal(Order order, Customer customer)
        {
            decimal subtotal = CalculateSubtotal(order);

            decimal discount = CalculateDiscount(order, customer);

            decimal afterDiscount = subtotal - discount;

            decimal tax = CalculateTax(afterDiscount);

            decimal shipping = CalculateShipping(afterDiscount);

            return afterDiscount + tax + shipping;
        }
    }
}