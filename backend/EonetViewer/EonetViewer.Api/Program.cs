using Eonet;
using EonetViewer.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding")));

builder.Services.AddControllers();
builder.Services.AddEonet(builder.Configuration);

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseGrpcWeb(new() { DefaultEnabled = true });
app.UseCors();
app.MapGrpcService<EventsService>().RequireCors("AllowAll");

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
