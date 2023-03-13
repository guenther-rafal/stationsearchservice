using StationSearchService.Domain.StationSearch.Interfaces;

namespace StationSearchService.Domain.StationSearch.ValueObjects;

public record StationUniqueCharacter : IStationUniqueCharacter
{
    private readonly Dictionary<char, StationUniqueCharacter> _connectedCharacters = new();
    private readonly List<StationName> _stationNames = new();
    public char Value { get; }
    public int DepthLevel { get; }

    public StationUniqueCharacter(char value, int depthLevel, StationName stationName)
    {
        Value = value;
        DepthLevel = depthLevel;
        _stationNames.Add(stationName);
    }

    public void AddStationName(StationName stationName)
    {
        _stationNames.Add(stationName);
    }

    public void ConnectCharacter(StationUniqueCharacter stationUniqueCharacter, char key)
    {
        _connectedCharacters.Add(key, stationUniqueCharacter);
    }

    public bool HasConnectedCharacter(char key) => _connectedCharacters.ContainsKey(key);

    public StationUniqueCharacter GetConnectedCharacter(char key) => _connectedCharacters[key];

    public char[] GetConnectedCharacters() => _connectedCharacters.Keys.ToArray();

    public string[] GetStationNames() => _stationNames.Select(z => z.Name).ToArray();
}