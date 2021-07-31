using Client.DemoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DemoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        
        private async Task<AccessTokenResponse> GetAccessToken()
        {
            var credentials = new Dictionary<string, string>
            {
                {"client_id","MyClient" },
                {"client_secret", "MySecrets" },
                {"grant_type","client_credentials" }
            };
            HttpRequestMessage reqMsg = new HttpRequestMessage(HttpMethod.Post, "connect/token");
            reqMsg.Content = new FormUrlEncodedContent(credentials);

            var httpClient = _httpClientFactory.CreateClient("IdentityServer");
            var response = await httpClient.SendAsync(reqMsg);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AccessTokenResponse>(data);
        }

        [Route("consumer-protected-api")]
        [HttpGet]
        public async Task<IActionResult> ConsumeProtectedApi()
        {
            var tokenResponse = await GetAccessToken();

            HttpRequestMessage reqMsg = new HttpRequestMessage(HttpMethod.Get, "api/test/case2");
            reqMsg.Headers.Add("Authorization", $"{tokenResponse.TokenType} {tokenResponse.AccessToken}");

            var httpClient = _httpClientFactory.CreateClient("ProtectedAPI");
            var response = await httpClient.SendAsync(reqMsg);
            response.EnsureSuccessStatusCode();
            return Ok(await response.Content.ReadAsStringAsync());
        }
    }
}
