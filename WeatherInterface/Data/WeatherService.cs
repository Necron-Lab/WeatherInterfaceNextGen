using Marten;
using System.Xml.Linq;
using WeatherInterface.Data;

public class WeatherService
{
    private readonly IDocumentStore _documentStore;

    public WeatherService(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Response> GetWeatherDataWithLocationAndLatestDate(string location)
    {
        using var session = _documentStore.QuerySession();

        // Lade den neusten Datensatz eines bestimmten Standorts
        var weatherData = session.Query<Response>()
        .Where(x => x.Location.Name.ToLower() == location).OrderByDescending(x => x.dateInserted).FirstOrDefault();
        
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
