namespace Swolesome_vip.Services;
using Swolesome_vip.Models;
// For now, this just loaads the config from a model, but I plan to add to this service support for saving the config to local storage and of course loading from there.
public class SearchConfigService
{
    public SearchConfig Config { get; set; }

    public SearchConfigService()
    {
        // Initialize with defaults
        Config = new SearchConfig();
    }

    public void UpdateConfig(SearchConfig newConfig)
    {
        Config = newConfig;
        // Optionally save to local storage
    }
}