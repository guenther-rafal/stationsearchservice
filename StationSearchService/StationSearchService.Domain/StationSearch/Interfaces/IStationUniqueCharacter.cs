using StationSearchService.Domain.StationSearch.ValueObjects;

namespace StationSearchService.Domain.StationSearch.Interfaces;

public interface IStationUniqueCharacter
{
    char Value { get; }
    int DepthLevel { get; }
    void AddStationName(StationName stationName);
    void ConnectCharacter(StationUniqueCharacter stationUniqueCharacter, char key);
    bool HasConnectedCharacter(char key);
    StationUniqueCharacter GetConnectedCharacter(char key);
    char[] GetConnectedCharacters();
    string[] GetStationNames();
}