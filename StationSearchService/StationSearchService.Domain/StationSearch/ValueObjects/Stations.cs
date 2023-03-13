using FluentResults;
using StationSearchService.Domain.StationSearch.Errors;
using StationSearchService.Domain.StationSearch.Interfaces;

namespace StationSearchService.Domain.StationSearch.ValueObjects;

public sealed class Stations : IStations
{
    private readonly ICollection<StationUniqueCharacter> _stationUniqueCharacters = new List<StationUniqueCharacter>();

    public Result<SearchDisplay> Search(string searchPhrase)
    {
        var firstCharacter = searchPhrase[0];
        var nextVertex = _stationUniqueCharacters
            .FirstOrDefault(z => z.Value == firstCharacter && z.DepthLevel == 0);
        if (nextVertex is null)
        {
            return Result.Fail(CorruptedSearchPhraseError.Create());
        }
        return searchPhrase.Length == 1 
            ? Result.Ok(new SearchDisplay(nextVertex)) 
            : SearchBeyondFirstCharacter(searchPhrase, nextVertex);
    }

    private static Result<SearchDisplay> SearchBeyondFirstCharacter(
        ReadOnlySpan<char> searchPhrase, 
        StationUniqueCharacter nextStationUniqueCharacter)
    {
        var splitInput = searchPhrase[1..];
        foreach (var character in splitInput)
        {
            if (!nextStationUniqueCharacter.HasConnectedCharacter(character))
            {
                return Result.Fail(CorruptedSearchPhraseError.Create());
            }
            nextStationUniqueCharacter = nextStationUniqueCharacter.GetConnectedCharacter(character);
        }
        return new SearchDisplay(nextStationUniqueCharacter);
    }

    public void AddStationNames(params StationName[] stationNames)
    {
        foreach (var stationName in stationNames)
        {
            AddStation(stationName);
        }
    }

    private void AddStation(StationName stationName)
    {
        var chars = stationName.Name.AsSpan();
        StationUniqueCharacter? parent = null;
        for (var i = 0; i < chars.Length; i++)
        {
            var letter = chars[i];
            var existingVertex = _stationUniqueCharacters
                .FirstOrDefault(z => z.DepthLevel == i && z.Value == letter);
            if (existingVertex is not null)
            {
                UpdateExistingVertex(existingVertex, ref parent, stationName, letter);
                continue;
            }
            var vertex = new StationUniqueCharacter(letter, i, stationName);
            parent?.ConnectCharacter(vertex, letter);
            parent = vertex;
            _stationUniqueCharacters.Add(vertex);
        }
    }
    
    private static void UpdateExistingVertex(StationUniqueCharacter existingStationUniqueCharacter, 
        ref StationUniqueCharacter? parent, StationName stationName, char key)
    {
        existingStationUniqueCharacter.AddStationName(stationName);
        if (parent is not null && !parent.HasConnectedCharacter(key))
        {
            parent.ConnectCharacter(existingStationUniqueCharacter, key);
        }
        parent = existingStationUniqueCharacter;
    }
}