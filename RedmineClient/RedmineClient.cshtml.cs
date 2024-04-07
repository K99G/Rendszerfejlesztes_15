using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class RedmineClientModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;

    public RedmineClientModel(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddHttpClient();
    }

    public List<Project> Projects { get; private set; }

    public async Task OnGetAsync()
    {
        HttpClient client = _clientFactory.CreateClient();
        var response = await client.GetStringAsync("http://localhost:5300/api/Projects");
        Projects = JsonConvert.DeserializeObject<List<Project>>(response);
    }
}