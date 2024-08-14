using Bookify.Domain.Users;
using Bookify.Domain.Bookings;
using Bookify.Domain.Apartments;
using Bookify.Infrustructure.Email;
using Bookify.Infrustructure.Clock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Bookify.Infrustructure.Repositories;
using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Abstractions.Email;
using Microsoft.Extensions.DependencyInjection;
using Bookify.Domain.Abstractions;
using Bookify.Application.Abstractions.Data;
using Bookify.Infrustructure.Data;
using Dapper;

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

            var connectionString = 
                configuration.GetConnectionString("Database") ??
                throw new ArgumentNullException(nameof(configuration));

            service.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
            });

            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IApartmentRepository, ApartmentRepository>();
            service.AddScoped<IBookingRepository,BookingRepository>();
            service.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            service.AddSingleton<ISqlConnectionFactory>(_=>
            new SqlConnectionFactory(connectionString));

            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

            return service;
        }
    }
}
