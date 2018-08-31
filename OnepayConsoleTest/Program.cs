using System;
using System.IO;
using Transbank.Onepay;
using Transbank.Onepay.Model;

namespace OnePayConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setting comerce data
            Onepay.SharedSecret = "?XW#WOLG##FBAGEAYSNQ5APD#JF@$AYZ";
            Onepay.ApiKey = "dKVhq1WGt_XapIYirTXNyUKoWTDFfxaEV63-O5jcsdw";
            Onepay.IntegrationType = Transbank.Onepay.Enums.OnepayIntegrationType.Test;

            // Setting items to the shopping cart
            ShoppingCart cart = new ShoppingCart();
            cart.Add(new Item(
                description: "Zapatos",
                quantity: 1,
                amount: 10000,
                additionalData: null,
                expire: 10));

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
