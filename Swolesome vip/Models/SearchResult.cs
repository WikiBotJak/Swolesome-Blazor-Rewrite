namespace Swolesome_vip.Models;
using System.Text.Json;

public class SearchResult
{
    public JsonDocument jsonData { get; set; }
    public List<string> SelectedFields { get; set; }
    public string Term { get; set; }
}