using FluentResults;
using MediatR;
using StationSearchService.Domain.StationSearch.ValueObjects;

namespace StationSearchService.Application.StationSearch.Commands;

public sealed class SearchStationsCommand : IRequest<Result<SearchDisplay>>
{
    public string SearchPhrase { get; }

    private SearchStationsCommand(string searchPhrase)
    {
        SearchPhrase = searchPhrase;
    }

    public static SearchStationsCommand Create(string searchPhrase) => new(searchPhrase);
}