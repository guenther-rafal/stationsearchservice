namespace StationSearchService.Domain.StationSearch.ValueObjects;

public class SearchDisplay
{
    public char[] AvailableCharacters { get; }
    public string[] StationNames { get; }

    public SearchDisplay(StationUniqueCharacter stationUniqueCharacter)
    {
        AvailableCharacters = stationUniqueCharacter.GetConnectedCharacters();
        StationNames = stationUniqueCharacter.GetStationNames();
    }
}