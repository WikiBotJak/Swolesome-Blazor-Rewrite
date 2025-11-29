namespace Swolesome_vip.Models;
using System.Text.Json;

public class SearchResult
{
    public string Key { get; set; } = string.Empty;
    public Dictionary<string, object> Data { get; set; } = new();
    public Dictionary<string, List<SearchResult>> ChildrenMap { get; set; } = new();
    public HashSet<string> ExpandedKeys { get; set; } = new();
    public int Depth { get; set; } = 0;
    public bool IsExpanded { get; set; } = false; 
    public bool IsDrilling { get; set; } = false;
    public string Term { get; set; } = string.Empty;
    public List<string> SelectedFields { get; set; } = new();
    public string? ActiveChildKey { get; set; }
    // public JsonDocument? JsonData { get; set; }
    public List<JsonElement> JsonData { get; set; } = new();
}