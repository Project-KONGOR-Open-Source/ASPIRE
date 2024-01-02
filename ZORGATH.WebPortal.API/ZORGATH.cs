﻿namespace ZORGATH.WebPortal.API;

internal class ZORGATH
{
    internal static bool RunsInDevelopmentMode { get; set; }

    internal static void Main(string[] args)
    {
        // Create The Application Builder
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Set Static RunsInDevelopmentMode Property
        RunsInDevelopmentMode = builder.Environment.IsDevelopment();

        // Add Aspire Service Defaults
        builder.AddServiceDefaults();

        // Add The Database Context
        builder.Services.AddDbContext<MerrickContext>(options =>
        {
            // Set The Database Connection Options
            options.UseSqlServer("MERRICK Database", connection => connection.MigrationsAssembly("MERRICK.Database.Manager"));

            // Enable Comprehensive Database Query Logging
            options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
        });

        // Add Memory Cache
        builder.Services.AddMemoryCache();

        // Set CORS Policy Name
        const string corsPolicyName = "Allow-All";

        // Set CORS Origins
        string[] corsOrigins = builder.Environment.IsDevelopment()
            ? ["http://localhost:55500", "http://localhost:55501", "https://localhost:55502", "http://localhost:55503", "https://localhost:55504", "http://localhost:55505", "https://localhost:55506", "http://localhost:55507", "https://localhost:55508", "http://localhost:55509", "https://localhost:55510", "http://localhost:55511", "https://localhost:55512"]
            : ["http://telemetry.kongor.online", "http://aspire.kongor.online", "https://aspire.kongor.online", "http://database.kongor.online", "https://database.kongor.online", "http://master.kongor.online", "https://master.kongor.online", "http://chat.kongor.online", "https://chat.kongor.online", "http://portal.api.kongor.online", "https://portal.api.kongor.online", "http://portal.ui.kongor.online", "https://portal.ui.kongor.online"];

        // Add CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(corsPolicyName,
                configuration => configuration.WithOrigins(corsOrigins)
                    .AllowAnyHeader().AllowAnyMethod().AllowCredentials());
        });

        // Add Server-Sided Cache
        builder.Services.AddOutputCache(); // TODO: Use Redis

        // Add Identity 
        builder.Services.AddIdentityCore<User>()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<MerrickContext>()
            .AddDefaultTokenProviders();

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_`";

                options.Password.RequiredLength = 0;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.Zero;
            });
        }

        else
        {
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_`";

                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 4;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;

                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            });
        }

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"] ?? "TODO: Add Me")), // TODO: Put The Key In A Secrets File And Move That File To A Separate Repository
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"] ?? "TODO: Add Me",
                ValidateIssuer = true,
                ValidAudience = builder.Configuration["JWT:Audience"] ?? "TODO: Add Me",
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = true
            };
        });

        builder.Services.AddAuthorization(); // TODO: Do I Need This?

        // Add MVC Controllers
        builder.Services.AddControllers();

        // Add Comprehensive Error Responses
        if (builder.Environment.IsDevelopment())
            builder.Services.AddProblemDetails();

        // Add Swagger
        builder.Services.AddSwaggerGen();

        // TODO: Clean This Up

        //builder.Services.AddSwaggerGen(options =>
        //{
        //    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Project KONGOR", Version = "v1" });

        //    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        //    {
        //        In = ParameterLocation.Header,
        //        Description = "Insert A Valid JSON Web Token",
        //        Name = "Authorization",
        //        
        //        Type = SecuritySchemeType.ApiKey, // (SecuritySchemeType.Http) ?
        //        Scheme = JwtBearerDefaults.AuthenticationScheme // "Bearer" ?
        //    });

        //    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        //    {
        //        {
        //            new OpenApiSecurityScheme
        //            {
        //                Reference = new OpenApiReference
        //                {
        //                    Type = ReferenceType.SecurityScheme,
        //                    Id = JwtBearerDefaults.AuthenticationScheme
        //                },
        //                Scheme = "oauth2", // Is This Needed  ?
        //                Name = JwtBearerDefaults.AuthenticationScheme,
        //                In = ParameterLocation.Header
        //            },
        //            new List<string>()
        //        }
        //    });

        /*
         *
         *    options.SwaggerDoc("v1", new OpenApiInfo
           {
               Version = "v1",
               Title = "ToDo API",
               Description = "An ASP.NET Core Web API for managing ToDo items",
               TermsOfService = new Uri("https://example.com/terms"),
               Contact = new OpenApiContact
               {
                   Name = "Example Contact",
                   Url = new Uri("https://example.com/contact")
               },
               License = new OpenApiLicense
               {
                   Name = "Example License",
                   Url = new Uri("https://example.com/license")
               }
           });
         *
         */
        //});

        // builder.Services.AddAntiforgery(); ???

        // Add Email Service
        builder.Services.AddSingleton<IEmailService, EmailService>();

        // Build The Application
        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.InjectStylesheet("/swagger/custom.css");
                // https://github.com/Amoenus/SwaggerDark/releases
                // https://blog.elijahlopez.ca/posts/aspnet-swagger-dark-theme/
                // https://amoenus.dev/swagger-dark-theme
            });
        }

        else
        {
            app.UseExceptionHandler();
        }

        // User CORS
        app.UseCors(corsPolicyName);

        // Use Server-Sided Cache
        app.UseOutputCache();

        // Map Aspire Default Endpoints
        app.MapDefaultEndpoints();

        // Map MVC Controllers
        app.MapControllers();

        // Require Authentication To Access Non-Public Resources
        app.UseAuthentication();

        // Require Authentication To Access Role-Specific Resources
        app.UseAuthorization();

        // Automatically Redirect To HTTPS
        app.UseHttpsRedirection();

        // Run The Application
        app.Run();
    }
}
