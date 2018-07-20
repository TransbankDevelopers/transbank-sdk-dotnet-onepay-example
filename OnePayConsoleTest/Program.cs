using System;
using Transbank.Model;
using Transbank.Net;

namespace OnePayConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var EXTERNAL_UNIQUE_NUMBER_TO_COMMIT_TRANSACTION_TEST = "8934751b-aa9a-45be-b686-1f45b6c45b02";
            var OCC_TO_COMMIT_TRANSACTION_TEST = "1807419329781765";

            // Setting comerce data
            Transbank.OnePay.SharedSecret = "P4DCPS55QB2QLT56SQH6#W#LV76IAPYX";
            Transbank.OnePay.ApiKey = "mUc0GxYGor6X8u-_oB3e-HWJulRG01WoC96-_tUA3Bg";
            //OnePay.OnePay.IntegrationType = OnePay.Enums.IntegrationType.MOCK;

            // Setting items to the shopping cart
            ShoppingCart cart = new ShoppingCart();
            cart.Add(new Item("Zapatos", 1, 10000, null, -1));
            cart.Add(new Item("Pantalon", 1, 5000, null, -1));

            // Send transaction to Transbank
            TransactionCreateResponse response = Transaction.Create(cart);

            Console.WriteLine(response.ToString());

            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
        }
    }
}
