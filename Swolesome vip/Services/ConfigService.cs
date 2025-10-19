namespace Swolesome_vip.Services;
using Swolesome_vip.Models;

public class SearchConfigService
{
    public SearchConfig Config { get; private set; }

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