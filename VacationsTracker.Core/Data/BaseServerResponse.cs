using Newtonsoft.Json;

namespace VacationsTracker.Core.Data
{
    public class BaseServerResponse<T>
    {
        [JsonProperty("result")]
        public T Result { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public object Message { get; set; }
    }
}
