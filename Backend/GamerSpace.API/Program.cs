using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using GamerSpace.API.Middleware;
using GamerSpace.Application.Mappings;
using GamerSpace.Application.Services.Auth;
using GamerSpace.Application.UseCases.Categories.Commands;
using GamerSpace.Application.UseCases.Categories.Queries;
using GamerSpace.Application.UseCases.ClassificationTypes.Commands;
using GamerSpace.Application.UseCases.ClassificationTypes.Queries;
using GamerSpace.Application.UseCases.Orders.Commands;
using GamerSpace.Application.UseCases.Products.Commands;
using GamerSpace.Application.UseCases.Products.Queries;
using GamerSpace.Application.UseCases.Users.Commands;
using GamerSpace.Application.Validators.Product;
using GamerSpace.Domain.Entities;
using GamerSpace.Domain.Interfaces;
using GamerSpace.Infrastructure.Persistence;
using GamerSpace.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var allowFrontEnd = "AngularFrontEnd";

builder.Services.AddCors(allowedCors =>
{
    allowedCors.AddPolicy(allowFrontEnd, policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<GamerSpaceDbContext>(opt =>
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
        mySqlOptions =>
        {
            mySqlOptions.MigrationsAssembly(typeof(GamerSpaceDbContext).Assembly.FullName);
            mySqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null);
        })

);

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICreateProductCommand, CreateProductCommand>();
builder.Services.AddScoped<IGetAllProductsQuery, GetAllProductsQuery>();
builder.Services.AddScoped<IGetProductByIdQuery, GetProductByIdQuery>();
builder.Services.AddScoped<IGetVariantsByProductQuery, GetVariantsByProductQuery>();
builder.Services.AddScoped<IGetProductVariantByIdQuery, GetProductVariantByIdQuery>();
builder.Services.AddScoped<IAddProductVariantCommand, AddProductVariantCommand>();
builder.Services.AddScoped<IUpdateProductCommand, UpdateProductCommand>();
builder.Services.AddScoped<IUpdateProductVariantCommand, UpdateProductVariantCommand>();
builder.Services.AddScoped<IDeleteProductVariantCommand, DeleteProductVariantCommand>();
builder.Services.AddScoped<IDeleteProductCommand, DeleteProductCommand>();

builder.Services.AddScoped<IGetAllClassificationTypesQuery, GetAllClassificationTypesQuery>();
builder.Services.AddScoped<IGetClassificationTypeByIdQuery, GetClassificationTypeByIdQuery>();
builder.Services.AddScoped<ICreateClassificationTypeCommand, CreateClassificationTypeCommand>();
builder.Services.AddScoped<IUpdateClassificationTypeCommand, UpdateClassificationTypeCommand>();
builder.Services.AddScoped<IDeleteClassificationTypeCommand, DeleteClassificationTypeCommand>();
builder.Services.AddScoped<IGetAllCategoriesQuery, GetAllCategoriesQuery>();
builder.Services.AddScoped<IGetCategoryByIdQuery, GetCategoryByIdQuery>();
builder.Services.AddScoped<ICreateCategoryCommand, CreateCategoryCommand>();
builder.Services.AddScoped<IUpdateCategoryCommand, UpdateCategoryCommand>();
builder.Services.AddScoped<IDeleteCategoryCommand, DeleteCategoryCommand>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRegisterUserCommand, RegisterUserCommand>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IJwtTokenGeneratorService, JwtTokenGeneratorService>();
builder.Services.AddScoped<ILoginCommand, LoginCommand>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICheckoutCommand, CheckoutCommand>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductDtoValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath);
    opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "GamerSpace API", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference{
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        },
    });
});

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"]!,

        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"]!,

        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
    };
});

builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<GamerSpaceDbContext>();
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Um erro ocorreu ao aplicar as migrations no banco de dados.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors(allowFrontEnd);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();