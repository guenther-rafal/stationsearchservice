using AutoFixture;
using FluentAssertions;
using StationSearchService.Application.StationSearch.Services;

namespace StationSearchService.Application.Tests.StationSearch.Services;

[TestFixture]
public sealed class StationsFactoryTests
{
    private StationsFactory _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new StationsFactory();
    }

    [Test]
    public void Create_Succeeds_ReturnsStations()
    {
        var fixture = new Fixture();
        var stations = fixture.CreateMany<string>().ToArray();

        var result = _sut.Create(stations);

        result.Should().NotBeNull();
    }
}