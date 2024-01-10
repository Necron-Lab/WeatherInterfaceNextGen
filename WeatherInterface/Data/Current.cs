using Newtonsoft.Json;

namespace WeatherInterface.Data
{
    public class Current
    {
        [JsonProperty("last_updated")]
        public required string LastUpdated { get; set; }

        [JsonProperty("temp_c")]
        public decimal TempC { get; set; }

        [JsonProperty("condition")]
        public required Condition Condition { get; set; }

        [JsonProperty("wind_kph")]
        public decimal WindKph { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        [JsonProperty("feelslike_c")]
        public decimal FeelsLikeC { get; set; }
    }
}
