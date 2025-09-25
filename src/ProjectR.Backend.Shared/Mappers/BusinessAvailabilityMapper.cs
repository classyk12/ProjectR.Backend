using Newtonsoft.Json;

namespace ProjectR.Backend.Shared.Mappers
{
    public static class Mapper
    {
        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            if (source == null)
            {
                return default!;
            }

            string serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<TDestination>(serialized)!;
        }
    }
}