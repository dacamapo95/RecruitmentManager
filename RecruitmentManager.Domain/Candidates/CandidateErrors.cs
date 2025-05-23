using RecruitmentManager.Domain.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentManager.Domain.Candidates;
public static class CandidateErrors
{
    public static Result<Guid> CandidateAlreadyExists(string email) =>
        Error.BadRequest($"Candidate with email {email} already exists.");

    public static Error CandidateNotFound(Guid id) =>
        Error.NotFound($"Candidate with id {id} was not found.");

    public static Error ExperienceNotFound(Guid id) =>
        Error.NotFound($"Experience with id {id} was not found.");
}
