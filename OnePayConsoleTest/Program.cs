using System;
using System.IO;
using Transbank.OnePay;
using Transbank.OnePay.Model;

namespace OnePayConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setting comerce data
            OnePay.SharedSecret = "P4DCPS55QB2QLT56SQH6#W#LV76IAPYX";
            OnePay.ApiKey = "mUc0GxYGor6X8u-_oB3e-HWJulRG01WoC96-_tUA3Bg";
            OnePay.IntegrationType = Transbank.OnePay.Enums.OnePayIntegrationType.TEST;

            // Setting items to the shopping cart
            ShoppingCart cart = new ShoppingCart();
            cart.Add(new Item("Zapatos", 1, 10000, null, -1));
            cart.Add(new Item("Pantalon", 1, 5000, null, -1));

            // Send transaction to Transbank
            TransactionCreateResponse response = Transaction.Create(cart);

            Console.WriteLine(response.ToString());

            var bytes = Convert.FromBase64String(response.QrCodeAsBase64);
            using (var imageFile = new FileStream(@"Qr.jpg", FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                imageFile.Flush();
            }

            Console.WriteLine("Pay with the app and then press any key to continue....");
            Console.ReadKey();

            TransactionCommitResponse commitResponse = Transaction.Commit(
               response.Occ, response.ExternalUniqueNumber);

            Console.WriteLine(commitResponse.ToString());

            Console.WriteLine("Press any key to Refund Transaction...");
            Console.ReadKey();

            RefundCreateResponse refundResponse = Refund.Create(commitResponse.Amount,
                commitResponse.Occ, response.ExternalUniqueNumber, 
                commitResponse.AuthorizationCode);

            Console.WriteLine(refundResponse.ToString());

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
