using FluentResults;
using MediatR;
using StationSearchService.Domain.StationSearch.Interfaces;
using StationSearchService.Domain.StationSearch.ValueObjects;

namespace StationSearchService.Application.StationSearch.Commands.Handlers;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class SearchStationsCommandHandler : IRequestHandler<SearchStationsCommand, Result<SearchDisplay>>
{
    private readonly IStations _stations;

    public SearchStationsCommandHandler(IStations stations)
    {
        _stations = stations;
    }

    public Task<Result<SearchDisplay>> Handle(SearchStationsCommand request, CancellationToken cancellationToken)
    {
        var searchResult = _stations.Search(request.SearchPhrase);
        return Task.FromResult(searchResult);
    }
}