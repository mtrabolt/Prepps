using Autofac;
using Autofac.Extensions.DependencyInjection;
using Marten;
using Marten.Services.Json;
using Prepps.Products;
using Prepps.Products.Queries;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsPolicyBuilder => corsPolicyBuilder
        //.WithOrigins("http://localhost:3000")
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .Build());
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterAssemblyTypes(typeof(GetProducts).Assembly)
        .AsImplementedInterfaces()
        .AsSelf()
        .InstancePerDependency();
});

builder.WebHost.ConfigureKestrel((_, options) =>
{
    options.AllowAlternateSchemes = true;
});

builder.Services.AddMarten(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Postgres");

    Console.WriteLine(connectionString);
    
    options.Connection(connectionString);
    
    // Opt into System.Text.Json serialization
    options.UseDefaultSerialization(serializerType: SerializerType.SystemTextJson);

    options.Schema.For<Product>().Index(x => x.Name);

    if (builder.Environment.IsDevelopment())
    {
        options.AutoCreateSchemaObjects = AutoCreate.All;
    }
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();