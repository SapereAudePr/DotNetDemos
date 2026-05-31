namespace Application.Queries.TPT;

public class DepartmentQuery
{
    public string? FilterOn { get; init; }
    public string? FilterQuery { get; init; }
    public string? SortBy { get; init; }
    public bool SortAscending { get; } = false;
    public int PageNumber { get; } = 1;
    public int PageSize { get; } = 10;
}