
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

    public class GlobalEnumDescriptionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsEnum || (Nullable.GetUnderlyingType(objectType)?.IsEnum ?? false);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            Type enumType = value.GetType();
            FieldInfo? field = enumType.GetField(value.ToString()!);
            string? description = field?.GetCustomAttribute<DescriptionAttribute>()?.Description;

            writer.WriteValue(description ?? value.ToString());
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            bool isNullable = Nullable.GetUnderlyingType(objectType) != null;
            Type enumType = Nullable.GetUnderlyingType(objectType) ?? objectType;
            string? stringValue = reader.Value?.ToString();

            if (stringValue == null)
            {
                return isNullable ? null : Activator.CreateInstance(enumType);
            }

            foreach (FieldInfo field in enumType.GetFields())
            {
                string? description = field.GetCustomAttribute<DescriptionAttribute>()?.Description;
                if (description == stringValue || field.Name == stringValue)
                {
                    return Enum.Parse(enumType, field.Name);
                }
            }

            throw new JsonSerializationException($"Cannot convert '{stringValue}' to {enumType.Name}");
        }
    }
}
