using System.Text.Json;

namespace Swolesome_vip.Utils
{
    public static class RenderJsonValue
    {
        public static string Render(JsonElement value)
        {
            return value.ValueKind switch
            {
                JsonValueKind.String => value.GetString() ?? "",
                JsonValueKind.Number => value.GetRawText(),
                JsonValueKind.True or JsonValueKind.False => value.GetBoolean().ToString(),
                JsonValueKind.Null => "",
                JsonValueKind.Array => string.Join(", ", value.EnumerateArray().Select(e =>
                    e.ValueKind == JsonValueKind.String ? e.GetString() ?? "" : e.GetRawText())),
                JsonValueKind.Object => value.GetRawText(),
                _ => ""
            };
        }
    }
}