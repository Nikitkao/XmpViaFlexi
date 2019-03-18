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
        private readonly ISecureStorage _storage;


        public VacationsApi(ISecureStorage storage)
        {
            _storage = storage;
            var token = _storage.GetAsync(Constants.TokenStorageKey).Result;
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public Task<IEnumerable<VacationDto>> GetVacationsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Constants.VacationApiUrl);

            return SendRequest<IEnumerable<VacationDto>>(request);
        }

        public Task<VacationDto> GetVacationAsync(string id)
        {
            var requestUri = Constants.VacationApiUrl + $"/{id}";
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            return SendRequest<VacationDto>(request);
        }

        public Task AddOrUpdateAsync(VacationDto vacation)
        {
            var requestUri = Constants.VacationApiUrl;
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            var serializedObject = JsonConvert.SerializeObject(vacation);
            request.Content = new StringContent(serializedObject, Encoding.UTF8, "application/json");

            return SendRequest<VacationDto>(request);
        }

        public Task DeleteAsync(string id)
        {
            var requestUri = Constants.VacationApiUrl + $"/{id}";
            var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);

            return SendRequest<VacationDto>(request);
        }

        public async Task<T> SendRequest<T>(HttpRequestMessage request)
        {
            var response = await _client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _storage.Remove(Constants.TokenStorageKey);
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
