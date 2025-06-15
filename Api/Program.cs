using Microsoft.EntityFrameworkCore;
using StarPingData.Models.Context;

var builder = WebApplication.CreateBuilder(args);

// Add database context
builder.Services.AddDbContext<StarPingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StarPingDBConnection")
        ?? throw new InvalidOperationException("Connection string not found.")));

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();
app.UseDeveloperExceptionPage();
app.MapControllers();

app.Run();
