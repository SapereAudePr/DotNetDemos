using Application.Common;
using Application.Requests.TPT;
using Application.Services.TPT;
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
}