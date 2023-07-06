namespace KingsUsers.Models;

public class PaginationParameters
{
    internal readonly int Offset;

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

class PaginationParametersImpl : PaginationParameters
{
}