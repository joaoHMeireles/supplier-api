using AutoMapper;
using SupplierAPI.DTOs;
using SupplierAPI.Helpers;
using SupplierAPI.Models;

namespace SupplierAPI;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SupplierInputDto, Supplier>();
        CreateMap<Supplier, SupplierOutputDto>()
            .ForMember(
                dest => dest.CNPJ,
                opt => opt.MapFrom(src => DataMaskHelper.MaskInformation(src.CNPJ))
            );
    }
}