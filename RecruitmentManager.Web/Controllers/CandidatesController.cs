using Microsoft.AspNetCore.Mvc;
using RecruitmentManager.Shared;
using RecruitmentManager.Web.ApiClients;
using RecruitmentManager.Web.ViewModels;

namespace RecruitmentManager.Web.Controllers;

public class CandidatesController(
    ICandidatesApiClient candidatesApiClient,
    IStatesApiClient statesApiClient,
    ICountriesApiClient countriesApiClient) : Controller
{
    private readonly ICandidatesApiClient _candidatesApiClient = candidatesApiClient;
    private readonly IStatesApiClient _statesApiClient = statesApiClient;
    private readonly ICountriesApiClient _countriesApiClient = countriesApiClient;

    public async Task<IActionResult> Index(
        string? searchTerm = null,
        string? sortColumn = null,
        string? sortOrder = null,
        int page = 1,
        int pageSize = 10,
        int? stateId = null,
        CancellationToken cancellationToken = default)
    {
        var candidates = await _candidatesApiClient.GetCandidatesAsync(
            searchTerm, sortColumn, sortOrder, page, pageSize, stateId, cancellationToken);

        var states = await _statesApiClient.GetStatesAsync(cancellationToken);

        var viewModel = new CandidatesViewModel
        {
            Candidates = candidates,
            States = states,
            SearchTerm = searchTerm,
            SortColumn = sortColumn,
            SortOrder = sortOrder,
            Page = page,
            PageSize = pageSize,
            StateId = stateId
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken = default)
    {
        var candidate = await _candidatesApiClient.GetCandidateByIdAsync(id, cancellationToken);
        if (candidate == null)
        {
            return NotFound();
        }

        var states = await _statesApiClient.GetStatesAsync(cancellationToken);
        var countries = await _countriesApiClient.GetCountriesAsync(cancellationToken);
        
        List<MasterEntityResponse<Guid>>? cities = null;

        if (candidate.CountryId.HasValue)
        {
            var citiesResponse = await _countriesApiClient.GetCitiesByCountryIdAsync(candidate.CountryId.Value, cancellationToken);
            cities = citiesResponse;
        }

        var viewModel = new EditCandidateViewModel
        {
            Id = candidate.Id,
            FirstName = candidate.FirstName,
            SurName = candidate.SurName,
            Email = candidate.Email,
            PhoneNumber = candidate.PhoneNumber,
            DateOfBirth = candidate.DateOfBirth,
            CountryId = candidate.CountryId ?? Guid.Empty,
            CityId = candidate.CityId,
            ZipCode = candidate.ZipCode,
            Street = candidate.Street,
            StateId = candidate.StateId,
            States = states,
            Countries = countries,
            Cities = cities,
            Experiences = candidate.Experiences.Select(e => new ExperienceViewModel
            {
                Id = e.Id,
                Company = e.Company,
                Job = e.Job,
                Description = e.Description,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Salary = e.Salary,
                Currency = e.Currency
            }).ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditCandidateViewModel viewModel, CancellationToken cancellationToken = default)
    {
        if (ModelState.IsValid)
        {
            var request = new EditCandidateRequest(
                viewModel.FirstName,
                viewModel.SurName,
                viewModel.DateOfBirth,
                viewModel.Email,
                viewModel.PhoneNumber,
                viewModel.CityId,
                viewModel.ZipCode,
                viewModel.Street,
                viewModel.StateId,
                viewModel.Experiences.Select(e => new EditExperienceRequest(
                    e.Id,
                    e.Company,
                    e.Job,
                    e.Description,
                    e.StartDate,
                    e.EndDate,
                    e.Salary,
                    e.Currency
                )).ToList()
            );

            var success = await _candidatesApiClient.EditCandidateAsync(viewModel.Id, request, cancellationToken);

            if (success)
            {
                TempData["SuccessMessage"] = "Candidate updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "An error occurred while updating the candidate.");
            }
        }

        var states = await _statesApiClient.GetStatesAsync(cancellationToken);
        var countries = await _countriesApiClient.GetCountriesAsync(cancellationToken);
        
        IEnumerable<MasterEntityResponse<Guid>>? cities = null;
        if (viewModel.CountryId != Guid.Empty)
        {
            var citiesResponse = await _countriesApiClient.GetCitiesByCountryIdAsync(viewModel.CountryId, cancellationToken);
            cities = citiesResponse;
        }

        viewModel.States = states;
        viewModel.Countries = countries;
        viewModel.Cities = cities;

        return View(viewModel);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCitiesByCountry(Guid countryId, CancellationToken cancellationToken = default)
    {
        if (countryId == Guid.Empty)
        {
            return Json(new { success = false, message = "Invalid country ID" });
        }
        
        var citiesResponse = await _countriesApiClient.GetCitiesByCountryIdAsync(countryId, cancellationToken);
        if (citiesResponse == null || citiesResponse.Count == 0)
        {
            return Json(new { success = false, message = "No cities found" });
        }
        
        return Json(new { success = true, cities = citiesResponse });
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken = default)
    {
        var states = await _statesApiClient.GetStatesAsync(cancellationToken);
        var countries = await _countriesApiClient.GetCountriesAsync(cancellationToken);

        var viewModel = new CreateCandidateViewModel
        {
            States = states,
            Countries = countries,
            Experiences = new List<CreateExperienceViewModel> { new CreateExperienceViewModel() }
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCandidateViewModel viewModel, CancellationToken cancellationToken = default)
    {
        if (ModelState.IsValid)
        {
            var request = new CreateCandidateRequest(
                viewModel.FirstName,
                viewModel.SurName,
                viewModel.DateOfBirth,
                viewModel.Email,
                viewModel.PhoneNumber,
                viewModel.CityId,
                viewModel.ZipCode,
                viewModel.Street,
                viewModel.StateId,
                viewModel.Experiences.Select(e => new CreateExperienceRequest(
                    e.Company,
                    e.Job,
                    e.Description,
                    e.StartDate,
                    e.EndDate,
                    e.Salary,
                    e.Currency
                )).ToList()
            );

            var candidateId = await _candidatesApiClient.CreateCandidateAsync(request, cancellationToken);

            if (candidateId.HasValue)
            {
                TempData["SuccessMessage"] = "Candidate created successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "An error occurred while creating the candidate.");
            }
        }

        var states = await _statesApiClient.GetStatesAsync(cancellationToken);
        var countries = await _countriesApiClient.GetCountriesAsync(cancellationToken);

        IEnumerable<MasterEntityResponse<Guid>>? cities = null;
        if (viewModel.CountryId != Guid.Empty)
        {
            var citiesResponse = await _countriesApiClient.GetCitiesByCountryIdAsync(viewModel.CountryId, cancellationToken);
            cities = citiesResponse;
        }

        viewModel.States = states;
        viewModel.Countries = countries;
        viewModel.Cities = cities;

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var success = await _candidatesApiClient.DeleteCandidateAsync(id, cancellationToken);

        if (success)
        {
            TempData["SuccessMessage"] = "Candidate deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
        
        TempData["ErrorMessage"] = "An error occurred while deleting the candidate.";
        return RedirectToAction(nameof(Index));
    }
}