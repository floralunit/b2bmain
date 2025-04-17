using B2BWebService.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using NLog;
using NLog.Web;
using System.Text.Json;
using NLog.Extensions.Logging;
using Microsoft.AspNetCore.HttpLogging;



var builder = WebApplication.CreateBuilder(args);


builder.Logging.ClearProviders();
var logger = LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
builder.Logging.AddNLog();

builder.Services.AddHttpLogging(logging =>

{

    logging.LoggingFields = HttpLoggingFields.All;

    logging.CombineLogs = true;

});

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(16161);
    serverOptions.ListenAnyIP(1616, listenOptions =>
    {
        listenOptions.UseHttps("/app/certificates/CertificateForB2B.pfx", "QLP5r5WzNIs23KI8FKIg4Yh3kjNjqDRerc13AXxOuFA=");
        //listenOptions.UseHttps("C:\\Users\\adm.yaskunova\\Documents\\floralunit-dir\\b2bwebservice\\Certificates\\CertificateForB2B.pfx", "QLP5r5WzNIs23KI8FKIg4Yh3kjNjqDRerc13AXxOuFA=");

    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMemoryCache();


builder.Services.AddScoped<AstraService>();
builder.Services.AddScoped<IB2BService, B2BService>();
builder.Services.AddHttpClient<MTService>(client =>
{
    client.BaseAddress = new Uri("http://mtapi.avtodom.ru/mttest/");
});

builder.Services.AddControllers().
    AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = new CustomJsonNamingPolice();
    }

    );
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "B2B API", Version = "v1" });
});

var app = builder.Build();

app.UseHttpLogging();

app.Use(async (context, next) =>
{
    var logger = LogManager.GetCurrentClassLogger();
    logger.Info($"Incoming request: {context.Request.Method} {context.Request.Path}");

    await next.Invoke();

    logger.Info($"Outgoing response: {context.Response.Body}");
});

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public class CustomJsonNamingPolice : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        return char.ToUpper(name[0]) + name.Substring(1);
    }
}