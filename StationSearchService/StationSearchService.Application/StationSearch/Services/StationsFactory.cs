using StationSearchService.Application.Common.Interfaces.Application;
using StationSearchService.Domain.StationSearch.ValueObjects;

namespace StationSearchService.Application.StationSearch.Services;

public sealed class StationsFactory : IStationsFactory
{
    public Stations Create(params string[] stationNames)
    {
        var cities = stationNames.Select(z => new StationName(z)).ToArray();
        var graph = new Stations();
        graph.AddStationNames(cities);
        return graph;
    }
}