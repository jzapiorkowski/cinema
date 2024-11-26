using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace Cinema.API.Core.Converters;

public class Iso8601TimeSpanConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var durationString = reader.GetString();

        if (durationString == null)
        {
            throw new JsonException("Duration cannot be null");
        }
        
        try
        {
            return XmlConvert.ToTimeSpan(durationString);
        }
        catch (FormatException)
        {
            throw new JsonException("Invalid ISO 8601 duration format.");
        }

    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(XmlConvert.ToString(value));
    }
}
