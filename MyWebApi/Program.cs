var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//TODO: Make sure to add the AzureAppConfig connection string in the user secrets
//https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=windows#set-a-secret 
// use dotnet user-secrets set ConnectionStrings:AzureAppConfig "<connection string>"
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(builder.Configuration.GetConnectionString("AzureAppConfig"))
        .Select(KeyFilter.Any)
        .Select(KeyFilter.Any, builder.Environment.EnvironmentName)
        .ConfigureKeyVault(kv =>
        {
            _ = kv.SetCredential(new DefaultAzureCredential());
        });
});

builder.Services.AddOptions<WeatherForecastSettings>()
    .BindConfiguration(WeatherForecastSettings.ConfigurationSection)
    .ValidateDataAnnotations()
    .ValidateOnStart();

//TODO: Remove because it's just a sample of typed service
builder.Services.AddHttpClient<WeatherForecastService>((serviceProvider, httpClient) =>
{
    var weatherForecastSettings = serviceProvider
        .GetRequiredService<IOptions<WeatherForecastSettings>>().Value;

    httpClient.BaseAddress = new Uri(weatherForecastSettings.BaseAddress);
    httpClient.DefaultRequestHeaders.Add("Authorization", weatherForecastSettings.AccessToken);
});

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();