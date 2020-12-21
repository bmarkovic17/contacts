using ContactsApiTests.Fixtures;
using Xunit;

namespace ContactsApiTests.Collections
{
    [CollectionDefinition("Automapper collection")]
    public class AutoMapperCollection : ICollectionFixture<AutoMapperFixture>
    {
    }
}
