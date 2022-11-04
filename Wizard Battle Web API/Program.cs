var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Add services to the container.
builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IAccountService, AccountService>();

builder.Services.AddTransient<IPlayerRepository, PlayerRepository>();
builder.Services.AddTransient<IPlayerService, PlayerService>();

builder.Services.AddTransient<IFriendshipRepository, FriendshipRepository>();
builder.Services.AddTransient<IFriendshipService, FriendshipService>();

builder.Services.AddTransient<IIconRepository, IconRepository>();
builder.Services.AddTransient<IIconService, IconService>();
#endregion

builder.Services.AddDbContext<DatabaseContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(Program));

#region Configuration
// Configure application confugartion settings
IConfigurationSection appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

// Encode the secret key
AppSettings appSettings = appSettingsSection.Get<AppSettings>();
byte[] key = Encoding.ASCII.GetBytes(appSettings.Secret);
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(builder => {
    builder.SetIsOriginAllowed(option => true)
    .AllowCredentials()
    .AllowAnyHeader()
    .AllowAnyMethod();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
