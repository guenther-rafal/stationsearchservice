using FluentAssertions;
using StationSearchService.Domain.StationSearch.ValueObjects;

namespace StationSearchService.Domain.Tests.StationSearch.ValueObjects;

[TestFixture]
public sealed class StationsTests
{
    [Test]
    public void Search_SuccessfulSearch_ReturnsDesiredLettersAndStationNames()
    {
        var sut = new Stations();
        var stationName1 = new StationName("station name");
        var stationName2 = new StationName("name station");
        sut.AddStationNames(stationName1, stationName2);

        var result = sut.Search("st");

        result.IsSuccess.Should().BeTrue();
        result.Value.StationNames.Should().ContainSingle(z => z == stationName1.Name);
        result.Value.AvailableCharacters.Should().ContainSingle(z => z == 'a');
    }
    
    [Test]
    public void Search_IncorrectFirstCharacter_ReturnsFail()
    {
        var sut = new Stations();
        var stationName1 = new StationName("station name");
        var stationName2 = new StationName("name station");
        sut.AddStationNames(stationName1, stationName2);

        var result = sut.Search("m");

        result.IsSuccess.Should().BeFalse();
    }
    
    [Test]
    public void Search_IncorrectNthCharacter_ReturnsFail()
    {
        var sut = new Stations();
        var stationName1 = new StationName("station name");
        var stationName2 = new StationName("name station");
        sut.AddStationNames(stationName1, stationName2);

        var result = sut.Search("stx");

        result.IsSuccess.Should().BeFalse();
    }
}