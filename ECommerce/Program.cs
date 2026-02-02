using BL.Contracts;
using BL.Data;
using BL.Services;
using ECommerce.ApiJwtService;
using ECommerce.BL.Contracts;
using ECommerce.BL.Services;
using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services for pages call and configure MVC services
builder.Services.AddControllersWithViews();

#region Serilog
// configure serilog for logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        tableName: "Log",
        autoCreateSqlTable: true)
    .CreateLogger();
builder.Host.UseSerilog(); 
#endregion

#region Entity Framwork, Identity & JWT 
//configure database connection string (add as a service to the DI container)
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Identity configorations
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true; //specail character
    options.Password.RequireUppercase = true;
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppDbContext>();
  //.AddDefaultTokenProviders();  //JWT

//JWT
//builder.Services.AddAuthorization();

// Configure JWT Authentication
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    var jwtSettings = builder.Configuration.GetSection("JwtSettings");
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = jwtSettings["Issuer"],
//        ValidAudience = jwtSettings["Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
//    };
//});
#endregion

#region Custom Services
//add the services to the DI container
builder.Services.AddScoped<ICategory, ClsCategory>();
builder.Services.AddScoped<IItem, ClsItem>();
builder.Services.AddScoped<IOs, ClsOs>();
builder.Services.AddScoped<IItemType, ClsItemType>();
builder.Services.AddScoped<IItemImage, ClsItemImages>();
builder.Services.AddScoped<ISlider, ClsSliders>();
builder.Services.AddScoped<ISalesInvoice, ClsSalesInvoice>();
builder.Services.AddScoped<ISalesInvoiceItem, ClsSalesInvoiceItems>();
builder.Services.AddScoped<ISetting, ClsSettings>();
builder.Services.AddScoped<IPage,ClsPages>();
builder.Services.AddScoped<JwtTokenService>();
#endregion

#region Session & Cookies
//State Management (cookies & sessions)
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
//Configure Cookie for (login)
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Users/AccessDenied";
    options.Cookie.Name = "Cookie";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(720); // 12 hours
    options.LoginPath = "/Users/Login";
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
}); 
#endregion

#region Swagger
//builder.Services.AddSwaggerGen(options =>
//{
//options.SwaggerDoc("v1", new OpenApiInfo
//{
//    Version = "v1",
//    Title = "Lao Shop Api",
//    Description = "api to access items and related categories",
//    TermsOfService = new Uri("https://www.google.com"),
//    Contact = new OpenApiContact
//    {
//        Email = "mohamed.alaa.poss@gmail.com",
//        Name = "Mohamed Alaa",
//        Url = new Uri("https://www.google.com")
//    },
//    License = new OpenApiLicense
//    {
//        Name = "It Boss Licence",
//        Url = new Uri("https://www.google.com")
//    }
//});
//options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//{
//    In = ParameterLocation.Header,
//    Description = "Enter 'Bearer' [space] and then your token",
//    Name = "Authorization",
//    Type = SecuritySchemeType.ApiKey
//});

//options.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            Array.Empty<string>()
//        }
//    });
    

    //var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var fullPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
    //options.IncludeXmlComments(fullPath);
//});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

#region Swagger UI
//app.UseSwagger();
//app.UseSwaggerUI(options =>
//{
//    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//});
#endregion

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

#region Routing
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}");

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

}
);
#endregion

app.Run();
