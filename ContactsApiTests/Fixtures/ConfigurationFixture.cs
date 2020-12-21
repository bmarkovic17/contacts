using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace ContactsApiTests.Fixtures
{
    public class ConfigurationFixture
    {
        public IConfiguration Configuration { get; private set; }

        public ConfigurationFixture() =>
            Configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string> { { "PageSize", "3" } })
                .Build();
    }
}
