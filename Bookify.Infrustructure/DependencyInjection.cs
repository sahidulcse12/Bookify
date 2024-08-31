using Dapper;
using Bookify.Domain.Users;
using Bookify.Domain.Bookings;
using Bookify.Domain.Apartments;
using Bookify.Domain.Abstractions;
using Bookify.Infrustructure.Data;
using Bookify.Infrustructure.Email;
using Microsoft.Extensions.Options;
using Bookify.Infrustructure.Clock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Bookify.Infrustructure.Repositories;
using Bookify.Infrustructure.Authentication;
using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Abstractions.Email;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Bookify.Application.Abstractions.Authentication;

namespace Bookify.Infrustructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrustructure(
            this IServiceCollection service,
            IConfiguration configuration)
        {
            service.AddTransient<IDateTimeProvider, DateTimeProvider>();
            service.AddTransient<IEmailService, EmailServices>();

            AddPersistance(service, configuration);
            AddAuthentication(service, configuration);

            return service;
        }

        public static void AddPersistance(IServiceCollection service, IConfiguration configuration) 
        {
            var connectionString =
                configuration.GetConnectionString("Database") ??
                throw new ArgumentNullException(nameof(configuration));

            service.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
            });

            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IApartmentRepository, ApartmentRepository>();
            service.AddScoped<IBookingRepository, BookingRepository>();
            service.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            service.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString));

            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        }

        private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));

            services.ConfigureOptions<JwtBearerOptionsSetup>();

            services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));

            services.AddTransient<AdminAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpClient) =>
            {
                KeycloakOptions keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

                httpClient.BaseAddress = new Uri(keycloakOptions.AdminUrl);
            })
            .AddHttpMessageHandler<AdminAuthorizationDelegatingHandler>();

            //services.AddHttpClient<IJwtService, JwtService>((serviceProvider, httpClient) =>
            //{
            //    KeycloakOptions keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

            //    httpClient.BaseAddress = new Uri(keycloakOptions.TokenUrl);
            //});

            services.AddHttpContextAccessor();

            //services.AddScoped<IUserContext, UserContext>();
        }

    }
}
