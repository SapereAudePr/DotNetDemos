using Application.Common;
using Application.Queries.TPT;
using Application.Requests.TPT;
using Application.Services.TPT;
using Domain.Entities.TPT;
using Domain.ValueObjects;
using Tests.Fake;
using Xunit.Abstractions;

namespace Tests.Services;

public class DepartmentTptServiceTests
{
    private readonly ITestOutputHelper _output;

    public DepartmentTptServiceTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task CreateAsync_WhenHospitalNotFound_ReturnsNotFound()
    {
        var repo = new FakeDepartmentTptRepo();
        var service = new DepartmentTptService(repo);
        var request = new CreateDepartmentRequest(
            "TestName",
            999,
            new PhoneNumberDto("1234567890", "TestLabel"),
            new EmailDto("testemail@test.com"));

        var result = await service.CreateAsync(request);


        _output.WriteLine($"Status: {result.Status}");
        _output.WriteLine($"Success: {result.IsSuccess}");

        if (result.Error is not null)
            _output.WriteLine($"Error: {result.Error}");


        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }


    [Fact]
    public async Task GetAllAsync_WhenDepartmentsExist_ReturnsAllDepartments()
    {
        var repo = new FakeDepartmentTptRepo(hospitalIds: [1, 2, 3],
            departments:
            [
                new Department(
                    1,
                    [new PhoneNumber("1234567890", "Test")],
                    [new EmailAddress("testemail@test.com")]),
                new Department(
                    2,
                    [new PhoneNumber("1234567292", "Test2")],
                    [new EmailAddress("testemail2@test.com")])
            ]);

        var service = new DepartmentTptService(repo);

        var request = new CreateDepartmentRequest(
            "TestName",
            1,
            new PhoneNumberDto("1234567893", "TestLabel"),
            new EmailDto("testemail@test.com"));

        await service.CreateAsync(request);

        var result = await service.GetAllAsync(new DepartmentQuery());

        foreach (var d in await repo.GetAllAsync())
        {
            _output.WriteLine($"Name:{d.Name}\nDepartmentId:{d.Id}\nHospitalId:{d.HospitalId}");
        }

        _output.WriteLine($"Status: {result.Status}");
        _output.WriteLine($"Success: {result.IsSuccess}");

        if (result.Error is not null)
            _output.WriteLine($"Error: {result.Error}");

        Assert.Contains(
            result.Value!, x => x.PhoneNumbers.Any(p => p.Number == "1234567890"));
        Assert.Contains(
            result.Value!, x => x.PhoneNumbers.Any(p => p.Number == "1234567292"));
        Assert.Contains(
            result.Value!, x => x.PhoneNumbers.Any(p => p.Number == "1234567893"));

        Assert.NotNull(result.Value);
        Assert.Equal(3, result.Value!.Count());

        _output.WriteLine($"resultValue: {result.Value.GetType()}");

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.Ok, result.Status);
    }

    [Fact]
    public async Task GetAllAsync_WhenDepartmentsNotExist_ReturnsEmptyList()
    {
        var repo = new FakeDepartmentTptRepo(departments: []);
        var service = new DepartmentTptService(repo);

        var result = await service.GetAllAsync(new DepartmentQuery());

        foreach (var d in await repo.GetAllAsync())
        {
            _output.WriteLine($"Name:{d.Name}\nDepartmentId:{d.Id}\nHospitalId:{d.HospitalId}");
        }

        _output.WriteLine($"Status: {result.Status}");
        _output.WriteLine($"Success: {result.IsSuccess}");

        if (result.Error is not null)
            _output.WriteLine($"Error: {result.Error}");

        Assert.Empty(result.Value!);
        Assert.NotNull(result.Value);
    }


    [Fact]
    public async Task GetByIdAsync_WhenDepartmentDoesNotExist_ReturnsNotFound()
    {
        var repo = new FakeDepartmentTptRepo(
            hospitalIds: [1, 2, 3],
            departments: []);

        var service = new DepartmentTptService(repo);

        var result = await service.GetByIdAsync(0);

        _output.WriteLine($"Status: {result.Status}");
        _output.WriteLine($"Success: {result.IsSuccess}");

        if (result.Error is not null)
            _output.WriteLine($"Error: {result.Error}");

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }


    [Fact]
    public async Task GetByIdAsync_WhenDepartmentExist_ReturnsDepartment()
    {
        var repo = new FakeDepartmentTptRepo(
            hospitalIds: [1]
        );

        var service = new DepartmentTptService(repo);

        var request = new Department(
            1,
            [new PhoneNumber("1234567890", "TestLabel")],
            [new EmailAddress("testemail@test.com")]);

        await repo.CreateAsync(request);

        var result = await service.GetByIdAsync(1);

        _output.WriteLine($"Status: {result.Status}");
        _output.WriteLine($"Success: {result.IsSuccess}");

        if (result.Error is not null)
            _output.WriteLine($"Error: {result.Error}");

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.Contains(result.Value!.PhoneNumbers, x => x.Number == "1234567890");
        Assert.NotNull(result.Value);
    }


    [Theory]
    [InlineData("")]
    [InlineData("a")]
    [InlineData("    ")]
    public async Task CreateAsync_WhenNameIsNotValid_ReturnValidationFailure(string name)
    {
        var repo = new FakeDepartmentTptRepo();
        var service = new DepartmentTptService(repo);
        var request = new CreateDepartmentRequest(
            name,
            1,
            new PhoneNumberDto("1234567890", "TestLabel"),
            new EmailDto("testemail@test.com"));

        var result = await service.CreateAsync(request);

        _output.WriteLine($"Status: {result.Status}");
        _output.WriteLine($"Success: {result.IsSuccess}");

        if (result.Error is not null)
            _output.WriteLine($"Error: {result.Error}");

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.ValidationFailure, result.Status);
    }

    [Theory]
    [InlineData("214512", "asfasg")]
    [InlineData("3129549125", "A")]
    [InlineData("5353", "25315")]
    [InlineData("124", "12345678901234545125215125125125125235346346")]
    [InlineData("000000", "12345678901234545125215125125125125235346346")]
    public async Task CreateAsync_WhenPhoneNumberIsNotValid_ReturnValidationFailure(string number, string label)
    {
        var repo = new FakeDepartmentTptRepo(hospitalIds: new List<int> { 1, 2, 3 });
        var service = new DepartmentTptService(repo);
        var request = new CreateDepartmentRequest(
            "TestHospital",
            1,
            new PhoneNumberDto(number, label),
            new EmailDto("testemail@test.com"));

        var result = await service.CreateAsync(request);

        _output.WriteLine($"Status: {result.Status}");
        _output.WriteLine($"Success: {result.IsSuccess}");

        if (result.Error is not null)
            _output.WriteLine($"Error: {result.Error}");

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.ValidationFailure, result.Status);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("a123")]
    [InlineData("12412f#^$#^$saw")]
    [InlineData("testemail@test.")]
    public async Task CreateAsync_WhenEmailIsNotValid_ReturnValidationFailure(string value)
    {
        var repo = new FakeDepartmentTptRepo(hospitalIds: [1, 2, 3]);
        var service = new DepartmentTptService(repo);
        var request = new CreateDepartmentRequest(
            "Test",
            1,
            new PhoneNumberDto("1234567890", "TestLabel"),
            new EmailDto(value));

        var result = await service.CreateAsync(request);

        _output.WriteLine($"Status: {result.Status}");
        _output.WriteLine($"Success: {result.IsSuccess}");

        if (result.Error is not null)
            _output.WriteLine($"Status: {result.Status}");

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.ValidationFailure, result.Status);
    }


    [Fact]
    public async Task CreateAsync_WhenRequestIsValid_ReturnOk()
    {
        var repo = new FakeDepartmentTptRepo(hospitalIds: [1]);
        var service = new DepartmentTptService(repo);
        var request = new CreateDepartmentRequest
        (
            "Test",
            1,
            new PhoneNumberDto("1234567890", "TestLabel"),
            new EmailDto("testemail@test.com")
        );

        var result = await service.CreateAsync(request);

        _output.WriteLine($"Status: {result.Status}");
        _output.WriteLine($"Success: {result.IsSuccess}");

        if (result.Error is not null)
            _output.WriteLine($"Error: {result.Error}");

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.Equal("Test", result.Value!.Name);
        Assert.Contains(result.Value.EmailAddresses, x => x.Value == "testemail@test.com");
    }

    [Theory]
    [InlineData("")]
    [InlineData("a")]
    [InlineData("   ")]
    public async Task UpdateAsync_WhenNameIsNotValid_ReturnValidationFailure(string name)
    {
        var repo = new FakeDepartmentTptRepo(hospitalIds: [1]);
        var service = new DepartmentTptService(repo);
        var request = new UpdateDepartmentRequest(
            name,
            new PhoneNumberDto("1234567890", "TestLabel"),
            new EmailDto("testemail@test.com"));

        var result = await service.UpdateAsync(1, request);

        _output.WriteLine($"Status: {result.Status}");
        _output.WriteLine($"Success: {result.IsSuccess}");

        if (result.Error is not null)
            _output.WriteLine($"Error: {result.Error}");

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.ValidationFailure, result.Status);
    }

    [Fact]
    public async Task UpdateAsync_WhenNameIsValid_ReturnsUpdatedDepartment()
    {
        var repo = new FakeDepartmentTptRepo(hospitalIds: [1]);
        var service = new DepartmentTptService(repo);

        var department = new Department
        (
            1,
            [new PhoneNumber("1234567890", "Test")],
            [new EmailAddress("testemail@test.com")]
        );

        await repo.CreateAsync(department);

        var updateRequest = new UpdateDepartmentRequest(
            "Test",
            new PhoneNumberDto("1234567890", "TestLabel"),
            new EmailDto("testemail@test.com"));

        var result = await service.UpdateAsync(1, updateRequest);

        _output.WriteLine($"Status: {result.Status}");
        _output.WriteLine($"Success: {result.IsSuccess}");

        if (result.Error is not null)
            _output.WriteLine($"Error: {result.Error}");

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.Ok, result.Status);
    }

    [Fact]
    public async Task UpdateAsync_WhenDepartmentDoesNotExist_ReturnsNotFound()
    {
        var repo = new FakeDepartmentTptRepo();
        var service = new DepartmentTptService(repo);
        var request = new UpdateDepartmentRequest(
            "Test",
            new PhoneNumberDto("1234567890", "TestLabel"),
            new EmailDto("testemail@test.com"));

        var result = await service.UpdateAsync(1, request);

        _output.WriteLine($"Status: {result.Status}");
        _output.WriteLine($"Success: {result.IsSuccess}");

        if (result.Error is not null)
            _output.WriteLine($"Error: {result.Error}");

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }

    [Theory]
    [InlineData("931258", "test")]
    [InlineData("95258", "test")]
    [InlineData("   ", "test")]
    public async Task UpdateAsync_WhenPhoneNumberIsNotValid_ReturnValidationFailure(
        string number, string label)
    {
        var repo = new FakeDepartmentTptRepo();
        var service = new DepartmentTptService(repo);
        var request = new UpdateDepartmentRequest(
            "Test",
            new PhoneNumberDto(number, label),
            new EmailDto("testemail@test.com"));


        var result = await service.UpdateAsync(1, request);

        _output.WriteLine($"Status: {result.Status}");
        _output.WriteLine($"Success: {result.IsSuccess}");

        if (result.Error is not null)
            _output.WriteLine($"Error: {result.Error}");

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.ValidationFailure, result.Status);
    }

    [Theory]
    [InlineData("wqke29mdwq")]
    [InlineData("95258")]
    [InlineData("   ")]
    public async Task UpdateAsync_WhenEmailIsNotValid_ReturnValidationFailure(
        string email)
    {
        var repo = new FakeDepartmentTptRepo(hospitalIds: [1]);
        var service = new DepartmentTptService(repo);
        var request = new UpdateDepartmentRequest(
            "Test",
            new PhoneNumberDto("000123425", "TestLabel"),
            new EmailDto(email));

        var create = new Department
        (
            1,
            [new PhoneNumber("1234567890", "Test")],
            [new EmailAddress("testemail@test.com")]
        );

        await repo.CreateAsync(create);

        var result = await service.UpdateAsync(1, request);

        _output.WriteLine($"Status: {result.Status}");
        _output.WriteLine($"Success: {result.IsSuccess}");

        if (result.Error is not null)
            _output.WriteLine($"Error: {result.Error}");

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.ValidationFailure, result.Status);
    }

    [Fact]
    public async Task DeleteAsync_WhenIdIsNotFound_ReturnNotFound()
    {
        var repo = new FakeDepartmentTptRepo();
        var service = new DepartmentTptService(repo);
        var result = await service.DeleteAsync(1);

        _output.WriteLine($"Status: {result.Status}");
        _output.WriteLine($"Success: {result.IsSuccess}");

        if (result.Error is not null)
            _output.WriteLine($"Error: {result.Error}");

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }

    [Fact]
    public async Task DeleteAsync_WhenIdIsValid_ReturnNoContent()
    {
        var repo = new FakeDepartmentTptRepo(
            hospitalIds: [1],
            departments:
            [
                new Department
                (
                    1,
                    [new PhoneNumber("123214425", "Test")],
                    [new EmailAddress("testemail@test.com")]
                )
            ]);

        var service = new DepartmentTptService(repo);
        var result = await service.DeleteAsync(0);

        _output.WriteLine($"Status: {result.Status}");
        _output.WriteLine($"Success: {result.IsSuccess}");

        if (result.Error is not null)
            _output.WriteLine($"Error: {result.Error}");

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.Ok, result.Status);
    }
}