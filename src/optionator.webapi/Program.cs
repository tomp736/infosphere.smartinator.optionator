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

List<OptionatorRepository> _optionatorRepositories = new List<OptionatorRepository>();
_optionatorRepositories.Add(new OptionatorRepository("tomp736", "infosphere.smartinator.optionator", "main", "examples/acceptance-test-driven-development.json", new HttpClient()));
_optionatorRepositories.Add(new OptionatorRepository("tomp736", "infosphere.smartinator.optionator", "main", "examples/agile-manifesto.json", new HttpClient()));
_optionatorRepositories.Add(new OptionatorRepository("tomp736", "infosphere.smartinator.optionator", "main", "examples/lean-principles.json", new HttpClient()));
_optionatorRepositories.Add(new OptionatorRepository("tomp736", "infosphere.smartinator.optionator", "main", "examples/plan-do-check-act.json", new HttpClient()));
_optionatorRepositories.Add(new OptionatorRepository("tomp736", "infosphere.smartinator.optionator", "main", "examples/software-design-patterns.json", new HttpClient()));

// Add optionator services to the container.
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<List<OptionatorRepository>>(_optionatorRepositories);
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
