using RecruitmentManager.Domain.Results;

namespace RecruitmentManager.Domain.Errors;

public static class SharedErrors
{
  public static Error MasterEntityNotFound(string entityName) =>
        Error.NotFound($"Master {entityName} not found.");
}