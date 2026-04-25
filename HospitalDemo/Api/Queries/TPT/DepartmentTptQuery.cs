namespace Api.Queries.TPT;

public class DepartmentQuery
{
    public string? FilterOn { get; init; }
    public string? FilterQuery { get; init; }
    public string? SortBy { get; init; }
    public bool SortAscending { get; init; } = false;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}