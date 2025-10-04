using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ProjectR.Backend.Shared.Mappers
{
    public static class Mapper
    {
        private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            // Prevent self-referencing loop exceptions by ignoring back-references
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            // Keep property names predictable when mapping between domain and DTOs
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
            // Ignore nulls to reduce noise during mapping
            NullValueHandling = NullValueHandling.Ignore
        };

        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            if (source == null)
            {
                return default!;
            }

            string serialized = JsonConvert.SerializeObject(source, _settings);
            return JsonConvert.DeserializeObject<TDestination>(serialized, _settings)!;
        }
    }
}