using AutoMapper;
using SupplierAPI.Models.DTOs;
using SupplierAPI.Models.Entities;
using SupplierAPI.Models.Enums;

namespace SupplierAPI.Tests.Fakers;

public static class SupplierFakers
{
    private const string DEFAULT_CNPJ = "01234567891011";
    private const string DEFAULT_TELEFONE = "5547123456789";

    private static int GetNumberOfDigits(int number)
    {
        return (int)Math.Floor(Math.Log10(number) + 1);
    }

    private static string GenerateCNPJ(int index)
    {
        return $"{DEFAULT_CNPJ[..(14 - GetNumberOfDigits(index))]}{index}";
    }

    private static string GenerateTelefone(int index)
    {
        return $"{DEFAULT_TELEFONE[..(13 - GetNumberOfDigits(index))]}{index}";
    }

    private static string GetRazaoSocial(int index)
    {
        return (index % 4) switch
        {
            0 => "Empresa DM Ltda.",
            1 => "João Silva ME",
            2 => "Corrêa & Costa Comércio e Serviços S.A.",
            3 => "TechInova Tecnologia Ltda.",
            _ => "Razao Social"
        };
    }

    public static Supplier GenerateSupplier(int index, bool hasRazaoSocial = true, bool hasTelefone = true, bool isActive = false) => new()
    {
        Id = index,
        Nome = $"Supplier {index}",
        Email = $"supplier{index}@gmail.com",
        CEP = 10000000 + index,
        CNPJ = GenerateCNPJ(index),
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now,
        EntityStatus = isActive ? EntityStatus.Active : EntityStatus.Deleted,
        RazaoSocial = hasRazaoSocial ? GetRazaoSocial(index) : null,
        Telefone = hasTelefone ? GenerateTelefone(index) : null,
    };

    public static Supplier DefaultSupplier => GenerateSupplier(1);

    public static List<Supplier> DefaultSuppliersList =>
    [
        DefaultSupplier,
        GenerateSupplier(2, hasRazaoSocial: false),
        GenerateSupplier(3, isActive: false),
        GenerateSupplier(4, hasTelefone: false)
    ];

    public static SupplierInputDto ToInputDto(this Supplier Supplier) => new SupplierInputDto()
    {
        Nome = Supplier.Nome,
        Email = Supplier.Email,
        CEP = Supplier.CEP,
        CNPJ = Supplier.CNPJ,
        RazaoSocial = Supplier.RazaoSocial,
        Telefone = Supplier.Telefone,
    };
}