using Moq;
using SupplierAPI.Models.DTOs;
using SupplierAPI.Models.Entities;
using SupplierAPI.Models.Enums;
using SupplierAPI.Repositories.Interfaces;
using SupplierAPI.Services;
using SupplierAPI.Tests.Fakers;

namespace SupplierAPI.Tests.Services;

public class SupplierServiceTests
{
    private readonly Mock<ISupplierRepository> _supplierRepositoryMock = new();

    private SupplierService _service;

    public SupplierServiceTests()
    {
        _service = new(
            _supplierRepositoryMock.Object,
            SettingsFakers.GetMapper()
        );
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3)]
    public async Task WhenGettingSupplier_ShouldReturnSupplierOutput(int index)
    {
        //Given
        var supplier = SupplierFakers.DefaultSuppliersList[index];

        _supplierRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(supplier);

        //When
        var supplierOutput = await _service.GetSupplierById(1);

        //Then
        AssertCorrectSupplierOutput(supplier, supplierOutput);
    }

    [Fact]
    public async Task WhenGettingSupplier_AndRepositoryRetusnNull_ShouldReturnNull()
    {
        //Given
        _supplierRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Supplier?)null);

        //When
        var supplierOutput = await _service.GetSupplierById(1);

        //Then
        Assert.Null(supplierOutput);
    }

    [Fact]
    public async Task WhenCreatingSupplier_ShouldReturnSupplierOutput()
    {
        //Given
        var supplier = SupplierFakers.DefaultSupplier;
        var supplierInput = supplier.ToInputDto();

        _supplierRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Supplier>()))
            .ReturnsAsync(supplier);

        //When
        var supplierOutput = await _service.AddSupplier(supplierInput);

        //Then
        AssertCorrectSupplierOutput(supplier, supplierOutput);
    }

    [Fact]
    public async Task WhenCreatingSupplier_AndRepositoryReturnsNull_ShouldReturnNull()
    {
        //Given
        var supplierInput = SupplierFakers.DefaultSupplier.ToInputDto();

        _supplierRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Supplier?)null);

        //When
        var supplierOutput = await _service.AddSupplier(supplierInput);

        //Then
        Assert.Null(supplierOutput);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public async Task WhenUpdatingSupplier_ShouldReturnUpdatedSupplierOutput(int index)
    {
        //Given
        var initialSupplier = SupplierFakers.DefaultSupplier;
        var supplier = SupplierFakers.DefaultSuppliersList[index];
        var supplierInput = supplier.ToInputDto();

        _supplierRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(initialSupplier);

        _supplierRepositoryMock.Setup(x => x.UpdateAsync(initialSupplier))
            .ReturnsAsync(supplier);

        //When
        var supplierOutput = await _service.UpdateSupplier(initialSupplier.Id, supplierInput);

        //Then
        AssertCorrectSupplierOutput(supplier, supplierOutput);
    }

    [Fact]
    public async Task WhenUpdatingSupplier_AndRepositoryReturnsNullOnGetById_ShouldReturnNull()
    {
        //Given
        var supplierInput = SupplierFakers.DefaultSuppliersList[1].ToInputDto();

        _supplierRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Supplier?)null);

        //When
        var supplierOutput = await _service.UpdateSupplier(1, supplierInput);

        //Then
        Assert.Null(supplierOutput);
    }

    [Fact]
    public async Task WhenPurgingDeleteSUppliers_AndOperationWorks_ShouldReturnSuccessfulOperation()
    {
        //When
        var operationResult = await _service.PurgeDeletedSuppliers();

        //Then
        Assert.Equal(OperationResult.Successfull, operationResult);
    }

    [Fact]
    public async Task WhenPurgingDeleteSUppliers_AndOperationFailes_ShouldReturnFailedOperation()
    {
        //Given
        _supplierRepositoryMock.Setup(x => x.PurgeDeletedEntities())
            .ThrowsAsync(new Exception("Error on Database"));

        //When
        var operationResult = await _service.PurgeDeletedSuppliers();

        //Then
        Assert.Equal(OperationResult.Failed, operationResult);
    }

    private static void AssertCorrectSupplierOutput(Supplier expectedSupplier, SupplierOutputDto? supplierOutput)
    {
        Assert.NotNull(supplierOutput);
        Assert.Equal(expectedSupplier.Id, supplierOutput.Id);
        Assert.Equal(expectedSupplier.CEP, supplierOutput.CEP);
        Assert.Equal(expectedSupplier.Email, supplierOutput.Email);
        Assert.Equal(expectedSupplier.Nome, supplierOutput.Nome);
        Assert.Equal(expectedSupplier.RazaoSocial, supplierOutput.RazaoSocial);
        Assert.Matches(@"(\d{2})\.(\d{3})\.(\d{3})\/(\d{4})-(\d{2})", supplierOutput.CNPJ);
    }
}