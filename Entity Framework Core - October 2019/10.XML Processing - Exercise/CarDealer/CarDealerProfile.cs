using CarDealer.Dtos.Import;
using CarDealer.Models;
using System.Linq;
using AutoMapper;
using CarDealer.Dtos.Export;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<ImportSupplierDto, Supplier>();

            this.CreateMap<ImportPartDto, Part>();

            this.CreateMap<Supplier ,ExportLocalSuppliersDto>();

            this.CreateMap<Part, ExportCarPartDto>();

            this.CreateMap<Car, ExportCarDto>()
                .ForMember(x => x.Parts, y => y.MapFrom(x => x.PartCars.Select(pc => pc.Part)));
        }
    }
}
