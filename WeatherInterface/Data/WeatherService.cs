using Marten;
using WeatherInterface.Data;

public class WeatherService
{
    private readonly IDocumentStore _documentStore;

    public WeatherService(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Response> GetWeatherDataWithLatestDate()
    {
        using var session = _documentStore.QuerySession();

        // Lade den Datensatz mit der höchsten ID
        var weatherData = session.Query<Response>().OrderByDescending(x => x.dateInserted).FirstOrDefault();

        return weatherData;
    }

    public async Task<Response> GetWeatherDataByLocation(string location)
    {
        using var session = _documentStore.QuerySession();

        // Lade die Wetterdaten für den angegebenen Standort
        var weatherData = session.Query<Response>().FirstOrDefault(x => x.Location.Name.ToLower() == location.ToLower());

        return weatherData;
    }
}
