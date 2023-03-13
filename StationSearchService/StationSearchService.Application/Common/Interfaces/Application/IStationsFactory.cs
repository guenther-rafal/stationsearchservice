using StationSearchService.Domain.StationSearch.ValueObjects;

namespace StationSearchService.Application.Common.Interfaces.Application;

public interface IStationsFactory
{
    Stations Create(params string[] stationNames);
}