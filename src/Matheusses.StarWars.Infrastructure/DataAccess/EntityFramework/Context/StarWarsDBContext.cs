using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matheusses.StarWars.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Matheusses.StarWars.Infrastructure.DataAccess.EntityFramework.Context
{
    public class StarWarsDBContext : DbContext
    {
        public StarWarsDBContext(DbContextOptions<StarWarsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Planet> Planets { get; set; }
        public virtual DbSet<Film> Films { get; set; }
    }
}