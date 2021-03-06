﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VacationsTracker.Core.Application;
using VacationsTracker.Core.DataAccess;
using VacationsTracker.Core.Exceptions;
using VacationsTracker.Core.Infrastructure.Connectivity;

namespace VacationsTracker.Core.Data
{
    public class VacationsApi : IVacationApi
    {
        private readonly HttpClient _client;
        private readonly ISecureStorage _storage;
        private readonly IConnectivity _connectivity;

        public VacationsApi(ISecureStorage storage, IConnectivity connectivity)
        {
            _storage = storage;
            _connectivity = connectivity;

            _client = new HttpClient
            {
                BaseAddress = new Uri(Constants.VacationApiUrl)
            };
            SetToken();
        }

        public Task<IEnumerable<VacationDto>> GetVacationsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Constants.VacationApiUrl);

            return SendRequest<IEnumerable<VacationDto>>(request);
        }

        public Task<VacationDto> GetVacationAsync(string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_client.BaseAddress}{id}");

            return SendRequest<VacationDto>(request);
        }

        public Task AddOrUpdateAsync(VacationDto vacation)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, string.Empty);
            var serializedObject = JsonConvert.SerializeObject(vacation);
            request.Content = new StringContent(serializedObject, Encoding.UTF8, "application/json");

            return SendRequest<VacationDto>(request);
        }

        public Task DeleteAsync(string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_client.BaseAddress}{id}");

            return SendRequest<VacationDto>(request);
        }

        public async Task<T> SendRequest<T>(HttpRequestMessage request)
        {
            if (!_connectivity.IsConnected)
            {
                throw  new InternetConnectionException("No internet connection");
            }

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

        public void SetToken()
        {
            var token = _storage.GetAsync(Constants.TokenStorageKey).Result;
            if (!string.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
