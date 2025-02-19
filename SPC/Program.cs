using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SPC.DataAccess;
using SPC.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddScoped<FacilityDAL>();
builder.Services.AddScoped<FacilityService>();
builder.Services.AddScoped<UserDAL>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DrugDAL>();
builder.Services.AddScoped<DrugService>();
builder.Services.AddScoped<TenderDAL>();
builder.Services.AddScoped<TenderService>();
builder.Services.AddScoped<TenderSubmissionDAL>();
builder.Services.AddScoped<TenderSubmissionService>();
builder.Services.AddScoped<StockDAL>();
builder.Services.AddScoped<StockService>();
builder.Services.AddScoped<SupplierOrderDAL>();
builder.Services.AddScoped<SupplierOrderService>();
builder.Services.AddScoped<PharmacyOrderDAL>();
builder.Services.AddScoped<PharmacyOrderService>();

// Configure JWT Authentication
var key = Encoding.ASCII.GetBytes("SuperStrongJWTSecretKeyMustBe32Chars!");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Enable authentication & authorization
builder.Services.AddAuthorization();

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

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
