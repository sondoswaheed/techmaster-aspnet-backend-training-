using DebugRefactor.Models;
using DebugRefactor.Services;

namespace DebugRefactor.UI
{
    public class ConsoleMenu
    {
        private readonly OrderCalculatorService _calculator;

        public ConsoleMenu(OrderCalculatorService calculator)
        {
            _calculator = calculator;
        }

        public void Start()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("        ORDER CALCULATOR SYSTEM");
            Console.WriteLine("========================================");

            Customer customer = ReadCustomer();
            Order order = ReadOrder();

            ShowReceipt(customer, order);
        }

        private Customer ReadCustomer()
        {
            string name;

            while (true)
            {
                Console.Write("Enter customer name: ");
                name = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(name))
                    break;

                Console.WriteLine("Customer name cannot be empty.");
            }

            CustomerType customerType = ReadCustomerType();

            return new Customer
            {
                Name = name,
                Type = customerType
            };
        }

        private CustomerType ReadCustomerType()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Customer Types:");
                Console.WriteLine("1. Regular");
                Console.WriteLine("2. Silver");
                Console.WriteLine("3. Gold");
                Console.WriteLine("4. VIP");

                Console.Write("Choose customer type: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        return CustomerType.Regular;

                    case "2":
                        return CustomerType.Silver;

                    case "3":
                        return CustomerType.Gold;

                    case "4":
                        return CustomerType.VIP;

                    default:
                        Console.WriteLine("Invalid customer type.");
                        break;
                }
            }
        }

        private Order ReadOrder()
        {
            string productName;

            while (true)
            {
                Console.Write("Enter product name: ");
                productName = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(productName))
                    break;

                Console.WriteLine("Product name cannot be empty.");
            }

            decimal price = ReadPositivePrice();
            int quantity = ReadPositiveQuantity();

            return new Order
            {
                ProductName = productName,
                Price = price,
                Quantity = quantity
            };
        }

        private decimal ReadPositivePrice()
        {
            while (true)
            {
                Console.Write("Enter product price: ");

                if (decimal.TryParse(Console.ReadLine(), out decimal price)
                    && price > 0)
                {
                    return price;
                }

                Console.WriteLine("Price must be a positive number.");
            }
        }

        private int ReadPositiveQuantity()
        {
            while (true)
            {
                Console.Write("Enter quantity: ");

                if (int.TryParse(Console.ReadLine(), out int quantity)
                    && quantity > 0)
                {
                    return quantity;
                }

                Console.WriteLine("Quantity must be a positive number.");
            }
        }

        private void ShowReceipt(Customer customer, Order order)
        {
            decimal subtotal = _calculator.CalculateSubtotal(order);

            decimal discount = _calculator.CalculateDiscount(
                order,
                customer);

            decimal afterDiscount = subtotal - discount;

            decimal tax = _calculator.CalculateTax(afterDiscount);

            decimal shipping = _calculator.CalculateShipping(afterDiscount);

            decimal finalTotal = _calculator.CalculateFinalTotal(
                order,
                customer);

            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("             ORDER RECEIPT");
            Console.WriteLine("========================================");

            Console.WriteLine($"Customer      : {customer.Name}");
            Console.WriteLine($"Customer Type : {customer.Type}");
            Console.WriteLine($"Product       : {order.ProductName}");
            Console.WriteLine($"Price         : {order.Price:F2}");
            Console.WriteLine($"Quantity      : {order.Quantity}");

            Console.WriteLine("----------------------------------------");

            Console.WriteLine($"Subtotal      : {subtotal:F2}");
            Console.WriteLine($"Discount      : {discount:F2}");
            Console.WriteLine($"Tax (14%)     : {tax:F2}");
            Console.WriteLine($"Shipping      : {shipping:F2}");

            Console.WriteLine("----------------------------------------");

            Console.WriteLine($"Final Total   : {finalTotal:F2}");

            Console.WriteLine("========================================");
        }
    }
}