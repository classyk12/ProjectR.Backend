
using Newtonsoft.Json;
using System.ComponentModel;
using System.Reflection;

namespace ProjectR.Backend.Shared.Helpers
{
    public static class EnumExtensions
    {
        public static bool TryParseEnum<T>(this string value, out T result) where T : struct
        {
            try
            {

                bool canParse = Enum.TryParse(value, true, out T enumResult);
                result = enumResult;
                return canParse;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static string ToEnumString<T>(this T value) where T : Enum
        {
            return Enum.GetName(typeof(T), value)!;
        }

        public static string GetEnumDescription<T>(this T value) where T : Enum
        {
            Type type = typeof(T);
            string? name = Enum.GetName(typeof(T), value);

            if (name == null)
            {
                return string.Empty;
            }

            System.Reflection.FieldInfo? field = type.GetField(name);
            object[] customAttribute = field!.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }
    }

    /// <summary>
    /// Convert Enums into their string Description equivalent. Can be used in form of annotation
    /// </summary>
    public class EnumDescriptionConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is Enum enumValue)
            {
                string description = enumValue
                    .GetType()
                    .GetField(enumValue.ToString())?
                    .GetCustomAttribute<DescriptionAttribute>()?
                    .Description ?? enumValue.ToString();

                writer.WriteValue(description);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Deserialization not implemented");
        }

        public override bool CanConvert(Type objectType) => objectType.IsEnum;
    }
}
