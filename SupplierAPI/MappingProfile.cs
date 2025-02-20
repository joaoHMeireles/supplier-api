using AutoMapper;
using SupplierAPI.Helpers;
using SupplierAPI.Models.DTOs;
using SupplierAPI.Models.Entities;

namespace SupplierAPI;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SupplierInputDto, Supplier>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
                {
                    if (srcMember == null) return false;
                    if (srcMember is int intValue && intValue == 0) return false;
                    if (srcMember is long longValue && longValue == 0) return false;
                    return true;
                })
            );
        CreateMap<Supplier, SupplierOutputDto>()
            .ForMember(
                dest => dest.CNPJ,
                opt => opt.MapFrom(src => FormatHelper.FormatCnpj(src.CNPJ))
            )
            .ForMember(
                dest => dest.EntityStatus,
                opt => opt.MapFrom(src => src.EntityStatus.ToString())
            )
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}