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

builder.Configuration.AddJsonFile("repositories.json");

IConfigurationSection repositoryConfigsSection = builder.Configuration.GetSection("GitHubRepositoryConfigs");
List<OptionatorGitHubRepositoryConfig> repositoryConfigs = 
    repositoryConfigsSection.Get<List<OptionatorGitHubRepositoryConfig>>() ?? new List<OptionatorGitHubRepositoryConfig>(); // TODO

// Register the repository configs as a singleton
builder.Services.AddSingleton<List<OptionatorGitHubRepositoryConfig>>(repositoryConfigs);

// Add optionator services to the container.
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<OptionatorGitHubRepositoryClient>();
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
