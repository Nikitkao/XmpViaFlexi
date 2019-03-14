using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VacationsTracker.Core.DataAccess;

namespace VacationsTracker.Core.Data
{
    public class VacationsApi : IVacationApi
    {
        private readonly HttpClient _client;

        public VacationsApi(ISecureStorage storage)
        {
            var token = storage.GetAsync(Settings.TokenStorageKey).Result;
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public Task<IEnumerable<VacationDto>> GetVacationsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Settings.VacationApiUrl);

            return SendRequest<IEnumerable<VacationDto>>(request);
        }

        public Task<VacationDto> GetVacationAsync(string id)
        {
            var requestUri = Settings.VacationApiUrl + $"/{id}";
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            return SendRequest<VacationDto>(request);
        }

        public Task AddOrUpdateAsync(VacationDto vacation)
        {
            var requestUri = Settings.VacationApiUrl;
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            var serializedObject = JsonConvert.SerializeObject(vacation);
            request.Content = new StringContent(serializedObject, Encoding.UTF8, "application/json");

            return SendRequest<VacationDto>(request);
        }

        public Task DeleteAsync(string id)
        {
            var requestUri = Settings.VacationApiUrl + $"/{id}";
            var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);

            return SendRequest<VacationDto>(request);
        }

        public async Task<T> SendRequest<T>(HttpRequestMessage request)
        {
            var response = await _client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }

            var content = await response.Content.ReadAsStringAsync();
            var baseServerResponse = JsonConvert.DeserializeObject<BaseServerResponse<T>>(content);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(baseServerResponse.Message.ToString());
            }

            return baseServerResponse.Result;
        }
    }
}
