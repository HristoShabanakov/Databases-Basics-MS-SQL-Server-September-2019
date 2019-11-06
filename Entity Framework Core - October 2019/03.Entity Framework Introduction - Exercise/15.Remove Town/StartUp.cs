namespace SoftUni
{
    using SoftUni.Data;
    using SoftUni.Models;
    using System;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using (var context = new SoftUniContext())
            {
                string result = RemoveTown(context);

                Console.WriteLine(result);
            }
        }


        public static string RemoveTown(SoftUniContext context)
        {
            var seattle = context
                .Towns
                .First(t => t.Name == "Seattle");

            var addressesInTown = context
                .Addresses
                .Where(a => a.Town == seattle);

            var employeesToRemoveAddress = context
                .Employees
                .Where(e => addressesInTown.Contains(e.Address));

            foreach (var e in employeesToRemoveAddress)
            {
                e.AddressId = null;
            }

            context.Addresses.RemoveRange(addressesInTown);

            int addressesCount = addressesInTown.Count();

            context.Towns.Remove(seattle);

            return $"{addressesCount} addresses in Seattle were deleted";
        }
    }
}
