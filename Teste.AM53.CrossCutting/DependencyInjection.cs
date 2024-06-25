using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Teste.AM53.Domain.Interfaces.Repository;
using Teste.AM53.Infrastructure;
using Teste.AM53.Infrastructure.Context;

namespace Teste.AM53.CrossCutting
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {

            services.AddDbContext<Am53DbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")
                  )
                );
             services.AddScoped<IProdutoRespository, ProdutoRepository>();
             services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            return services;
        }
    }
}
