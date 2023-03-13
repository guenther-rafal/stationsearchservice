using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;
using StationSearchService.Domain.StationSearch.ValueObjects;

namespace StationSearchService.Application.Tests.StationSearch.SpecimenBuilders;

internal sealed class SearchDisplaySpecimenBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is not TypeInfo ti)
        {
            return new NoSpecimen();
        }
        if (ti.Name != nameof(SearchDisplay))
        {
            return new NoSpecimen();
        }
        var fixture = new Fixture();
        var stationUniqueCharacter = fixture.Create<StationUniqueCharacter>();
        return new SearchDisplay(stationUniqueCharacter);
    }
}