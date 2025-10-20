
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(Options =>     
{
    Options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});



builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();



WebApplication app = builder.Build();

await app.BootUmbracoAsync();


app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
