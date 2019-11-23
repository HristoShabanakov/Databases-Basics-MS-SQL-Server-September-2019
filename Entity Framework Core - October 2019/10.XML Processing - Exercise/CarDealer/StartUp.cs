using System;
using System.IO;
using System.Xml.Serialization;

using CarDealer.Data;
using CarDealer.Dtos.Import;
using CarDealer.Models;

using AutoMapper;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<CarDealerProfile>());

            using (var db = new CarDealerContext())
            {
                var inputXml = File.ReadAllText("./../../../Datasets/suppliers.xml");

                var result = ImportSuppliers(db, inputXml);

                Console.WriteLine(result);
            }
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportSupplierDto[]),
                new XmlRootAttribute("Suppliers"));

            ImportSupplierDto[] supplierDtos;

            using (var reader = new StringReader(inputXml))
            {
                supplierDtos = (ImportSupplierDto[])xmlSerializer.Deserialize(reader);
            }

            var suppliers = Mapper.Map<Supplier[]>(supplierDtos);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();


            
            return $"Successfully imported {suppliers.Length}";
        }

    }
}