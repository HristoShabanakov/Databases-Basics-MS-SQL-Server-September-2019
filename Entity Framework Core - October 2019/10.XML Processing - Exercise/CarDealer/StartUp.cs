using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using CarDealer.Data;
using CarDealer.Dtos.Import;
using CarDealer.Models;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Dtos.Export;
using System.Collections.Generic;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<CarDealerProfile>());

            using (var db = new CarDealerContext())
            {
                //var inputXml = File.ReadAllText("./../../../Datasets/cars.xml");

                var result = GetCarsWithTheirListOfParts(db);

                Console.WriteLine(result);
            }
        }

        //09 Problem - Import the suppliers from the provided file suppliers.xml.

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

        //10 Problem - Import the parts from the provided file parts.xml. If the supplierId doesn’t exists, skip the record.

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = 
                new XmlSerializer(typeof(ImportPartDto[]),
                new XmlRootAttribute("Parts"));

            ImportPartDto[] partDtos;

            //returns an object. Collection is needed to store the data.
            using (var reader = new StringReader(inputXml))
            {
               partDtos = ((ImportPartDto[])xmlSerializer
                    .Deserialize(reader))
                    .Where(p => context.Suppliers.Any(s => s.Id == p.SupplierId))
                    .ToArray(); 
            }

            var parts = Mapper.Map<Part[]>(partDtos);

            context.Parts.AddRange(parts);
            context.SaveChanges();


            return $"Successfully imported {parts.Length}";
        }

        //11 Problem - Import cars from the provided file cars.xml. Select unique car part ids. If part id doesn’t exists, skip the part record.

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = 
                new XmlSerializer(typeof(ImportCarDto[]), 
                new XmlRootAttribute("Cars"));

            ImportCarDto[] carDtos;

            using (var reader = new StringReader(inputXml))
            {
                carDtos = ((ImportCarDto[])xmlSerializer.Deserialize(reader));
            }

            List<Car> cars = new List<Car>();
            List<PartCar> partCars = new List<PartCar>();

            foreach (var carDto in carDtos)
            {
                var car = new Car()
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TravelledDistance = carDto.TraveledDistance
                };

                var parts = carDto
                    .Parts
                    .Where(pdto => context.Parts.Any(p => p.Id == pdto.Id))
                    .Select(p => p.Id)
                    .Distinct();

                foreach (var partId in parts)
                {
                    var partCar = new PartCar()
                    {
                        PartId = partId,
                        Car = car
                    };

                    partCars.Add(partCar);
                }

                cars.Add(car);
            }

            context.Cars.AddRange(cars);
            context.PartCars.AddRange(partCars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }


        //16 Get all suppliers that do not import parts from abroad. Get their id, name and the number of parts they can offer to supply.
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var suppliers = context
                .Suppliers
                .Where(s => !s.IsImporter)
                .ProjectTo<ExportLocalSuppliersDto>()
                .ToArray();

            var xmlSerializer = 
                new XmlSerializer(typeof(ExportLocalSuppliersDto[]), 
                new XmlRootAttribute("suppliers"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);
            
            using(var writer = new StringWriter(stringBuilder))
            {
                xmlSerializer.Serialize(writer, suppliers, namespaces);
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //17 Problem  - Get all cars along with their list of parts.

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            StringBuilder sb = new StringBuilder();

            //Get all cars from database
            var cars = context
                .Cars
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .ProjectTo<ExportCarDto>()
                .Take(5)
                .ToArray();

            foreach (var car in cars)
            {
                car.Parts = car.Parts
                    .OrderByDescending(c => c.Price)
                    .ToArray();
            }

            //Always start from the outer (first Dto)
            var xmlSerializer = new XmlSerializer(typeof(ExportCarDto[]),
                new XmlRootAttribute("cars"));

            //Fixing namespaces in xml document. Don't forget to add them in xmlSerializer.Serialize().
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);

            using (var writer = new StringWriter(sb))
            {
                xmlSerializer.Serialize(writer, cars, namespaces);
            }
           

            return sb.ToString().TrimEnd();
        }



    }
}