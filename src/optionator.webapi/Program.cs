using optionator.data;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*");
                      });
});

// Add optionator services to the container.
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<OptionatorRepository>(
    new OptionatorRepository(
        "tomp736",
        "infosphere.smartinator.optionator",
        "main",
        "examples/agile_manifesto.json",
        new HttpClient()));
builder.Services.AddSingleton<OptionatorDataProvider>();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
