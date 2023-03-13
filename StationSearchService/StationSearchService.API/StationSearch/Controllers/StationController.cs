using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StationSearchService.Application.StationSearch.Commands;
using StationSearchService.Common.Extensions;
using StationSearchService.Domain.StationSearch.Errors;

namespace StationSearchService.StationSearch.Controllers;

[ApiController]
[Route("v1/[controller]")]
public sealed class StationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<SearchRequest> _searchRequestValidator;

    public StationController(IMediator mediator, IValidator<SearchRequest> searchRequestValidator)
    {
        _mediator = mediator;
        _searchRequestValidator = searchRequestValidator;
    }

    [HttpPost("search")]
    public async Task<IActionResult> Search(SearchRequest request, CancellationToken ct = default)
    {
        var validationResult = await _searchRequestValidator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            ModelState.AddValidationResult(validationResult);
            return BadRequest(ModelState);
        }
        var command = SearchStationsCommand.Create(request.SearchPhrase);
        var searchResult = await _mediator.Send(command, ct);
        return searchResult.HasError<CorruptedSearchPhraseError>()
            ? BadRequest(ModelState.AddErrors(searchResult.Errors))
            : Ok(searchResult.Value);
    }
}