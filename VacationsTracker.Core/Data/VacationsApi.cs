using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<IEnumerable<VacationDto>> GetVacationsAsync()
        {
            var requestUri = Settings.VacationApiUrl;
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await _client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }

            var content = await response.Content.ReadAsStringAsync();
            var vacancies = JsonConvert.DeserializeObject<BaseServerResponse<VacationDto>>(content);

            return vacancies.Result;
        }

        public class BaseServerResponse<T>
        {
            [JsonProperty("result")]
            public List<T> Result { get; set; }
            [JsonProperty("code")]
            public int Code { get; set; }
            [JsonProperty("message")]
            public object Message { get; set; }
        }
    }
}
