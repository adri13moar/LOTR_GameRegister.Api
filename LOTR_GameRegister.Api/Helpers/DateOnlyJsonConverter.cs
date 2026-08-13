using System.Text.Json;
using System.Text.Json.Serialization;

namespace LOTR_GameRegister.Api.Helpers
{
    /// <summary>
    /// Custom JSON converter that serializes <see cref="DateOnly"/> values using
    /// the <c>dd-MM-yyyy</c> format expected by the API contract.
    /// </summary>
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private const string Format = "dd-MM-yyyy";

        /// <inheritdoc />
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.ParseExact(reader.GetString()!, Format);
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }
    }
}