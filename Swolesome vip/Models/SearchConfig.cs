namespace Swolesome_vip.Models;

public class SearchConfig
{
    public List<string> SourceTypes { get; set; } = new()
    {
        "twitter.com", "Wattpad", "Deezer", "Zynga", "Collection #1", 
        "myspace.com", "last.fm", "myfitnesspal.com", "AnimalJam",
        // ... add all defaults here ...
    };

    public List<string> InputTypes { get; set; } = new()
    {
        "username", "email", "password", "domain", "ip", "name",
        "uuid", "steamid", "phone", "discordid"
    };

    public Dictionary<string, List<string>> Synonyms { get; set; } = new()
    {
        { "email", new() { "e-mail", "facebook_email", "email_address" } },
        { "password", new() { "pass", "pass_plain", "crypted_password", "encrypt_pass", "hash", "hash1", "hash2", "m_password", "plain_pass", "members_pass_hash", "member_login_key", "passwd" } },
        { "domain", new() { "site", "website", "host", "adomain", "access_domain", "server" } },
        { "username", new() { "user", "login", "nick", "handle", "uname", "username2", "members_l_username", "members_l_display_name", "display_name", "member_url", "usergroupid" } },
        { "ip", new() { "ipaddress", "hostip", "second_ip", "lastip", "logged_ip", "regip", "last_modified_ip" } },
        { "name", new() { "fullname", "person", "firstname", "lastname", "first_name", "last_name", "middle_name", "name1", "name2" } },
        { "uuid", new() { "guid", "id", "userid", "user_id", "ncid", "pub_id", "uid", "id_member" } },
        { "steamid", new() { "steam", "steamid64", "steamid" } },
        { "phone", new() { "telephone", "phone", "contact_number" } },
        { "discordid", new() { "discord", "discordid64" } },
    };

    public int MaxDepth { get; set; } = 5;
    public int Timeout { get; set; } = 10; // seconds
    public int RequestDelay { get; set; } = 4000; // milliseconds
    public int DiscardThreshold { get; set; } = 50;
    public string ApiBaseUrl { get; set; } = "https://breach.vip/api/search";
    public string ProxyUrl { get; set; } = "/api/proxy";
}