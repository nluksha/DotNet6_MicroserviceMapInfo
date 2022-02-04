using Microsoft.OpenApi.Models;
using GoogleMapInfo;
using MicroserviceMapInfo.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<GoogleDistanceApi>();
builder.Services.AddScoped(typeof(IDistanceInfoSvc), typeof(DistanceInfoSvc));

builder.Services.AddControllers();
builder.Services.AddGrpc();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
   c.SwaggerDoc("v1", new OpenApiInfo { Title = "My map API", Version = "v1" });
});

var distanceMicroserviceUrl = builder.Configuration.GetSection("DistanceMicroservice:Location").Value;
builder.Services.AddHttpClient("DistanceMicroservice", client =>
{
    client.BaseAddress = new Uri(distanceMicroserviceUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DistanceInfoService>();
    endpoints.MapControllers();
});

app.Run();
