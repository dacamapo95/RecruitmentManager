using RecruitmentManager.Shared;

namespace RecruitmentManager.Web.ViewModels;

public class CandidatesViewModel
{
    public PaginatedList<CandidateItemResponse>? Candidates { get; set; }
    public IEnumerable<MasterEntityResponse<int>>? States { get; set; }
    
    public string? SearchTerm { get; set; }
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int? StateId { get; set; }

    public string GetSortIcon(string column)
    {
        if (SortColumn != column)
        {
            return string.Empty;
        }

        return SortOrder?.ToLower() == "desc" ? "▼" : "▲";
    }

    public string GetSortOrder(string column)
    {
        if (SortColumn != column)
        {
            return "asc";
        }

        return SortOrder?.ToLower() == "desc" ? "asc" : "desc";
    }
}