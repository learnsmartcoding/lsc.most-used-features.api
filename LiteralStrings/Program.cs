
string 
    shortString 
    = """Microsoft "Azure", often referred to as Azure""";

string longString 
    = """
    Build cloud-connected mobile experiences based on customer 
       interests and behaviors using AI and cognitive services . 
        
    Deploy Windows and Linux virtual machines, 
            modernize applications, 
            and develop apps across cloud and hybrid environments.
    Create workloads in Azure that scale with your business, host domains, 
    and deploy faster with DevOps tools.
    """;



Console.WriteLine(shortString);
Console.WriteLine(longString);
Console.WriteLine();


int Latitude = 10;
int Longitude = 20;


var location = $$"""
   GPS coordinates: {{{Longitude}}, {{Latitude}}}
   """;

Console.WriteLine(location);
Console.WriteLine();


List<WeatherForecast> weatherForecasts = new List<WeatherForecast>()
{
    new WeatherForecast(){  Date = DateTime.Now, Summary = "Snow", TemperatureC = -3},
    new WeatherForecast(){  Date = DateTime.Now.AddDays(1), Summary = "Sunny and cold", TemperatureC = -5},
    new WeatherForecast(){  Date = DateTime.Now.AddDays(2), Summary = "Snow showers", TemperatureC = -1},

};

var json = $$"""
    [
      {
        "date": "{{weatherForecasts[0].Date}}",
        "temperatureC": {{weatherForecasts[0].TemperatureC}},
        "summary": "{{weatherForecasts[0].Summary}}"
      },
      {
        "date": "{{weatherForecasts[1].Date}}",
        "temperatureC": {{weatherForecasts[1].TemperatureC}},
        "summary": "{{weatherForecasts[1].Summary}}"
      },{
        "date": "{{weatherForecasts[2].Date}}",
        "temperatureC": {{weatherForecasts[2].TemperatureC}},
        "summary": "{{weatherForecasts[2].Summary}}"
      },
    ]
    """;

Console.WriteLine(json);

Console.ReadLine();

class WeatherForecast
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public string? Summary { get; set; }

}


