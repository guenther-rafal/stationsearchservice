using Autofac;
using StationSearchService.Application.StationSearch.Services;

namespace StationSearchService.Application.StationSearch;

public sealed class DependencyModule : Module
{
    protected override void Load(ContainerBuilder containerBuilder)
    {
        var stationsFactory = new StationsFactory();
        var stationNames = new[] { "dartford", "darton", "derby" };
        var stations = stationsFactory.Create(stationNames);
        containerBuilder.Register(z => stations).AsImplementedInterfaces().SingleInstance();
    }
}