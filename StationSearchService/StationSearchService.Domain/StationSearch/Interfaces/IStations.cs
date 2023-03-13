using FluentResults;
using StationSearchService.Domain.StationSearch.ValueObjects;

namespace StationSearchService.Domain.StationSearch.Interfaces;

public interface IStations
{
    Result<SearchDisplay> Search(string searchPhrase);
    void AddStationNames(StationName[] stationNames);
}