using AutoMapper;
using ContactsApi.Helpers;

namespace ContactsApiTests.Helpers
{
    public class AutoMapperFixture
    {
        public IMapper Mapper { get; private set; }
        
        public AutoMapperFixture()
        {
            var config = new MapperConfiguration(configure => configure.AddProfile(typeof(AutoMapperProfiles)));

            Mapper = config.CreateMapper();
        }
    }
}
