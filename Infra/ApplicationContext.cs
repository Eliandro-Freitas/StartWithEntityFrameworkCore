using DominandoEfCore.Domain;
using DominandoEfCore.Infra.Maps;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace DominandoEfCore.Infra
{
    private readonly string _connectionString = "Data Source=localhost;Initial Catalog=Estudos;User Id=sa;Password=@Elifreitas0;";

    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = "Data Source=localhost;Initial Catalog=Estudos;User Id=sa;Password=@Elifreitas0;";
            optionsBuilder
                .UseSqlServer(connectionString, x => x.CommandTimeout(20) /*configura o tempo máximo de uma consulta*/
                .EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null)) /*Tenta se conectar ao banco*/
                // Mostra os parametros pelo console, sem esse método, os parametros ficam como "?"
                .EnableSensitiveDataLogging()
                // Mostra detalhadamente onde está o caso caso aconteça, mas faz uma sobrecarga na aplicação, é bom usar só quando dar erro em algo (EnableDetailesError)
                .EnableDetailedErrors()
                .LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //GlobalFilter "HasQueryFilter"
            //modelBuilder.Entity<Departamento>().HasQueryFilter(x => x.Ativo);

            //Determina uma coleção configurando globalmente
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CS_AS");

            // determina uma determinada coleção para uma propiedade na entidade
            modelBuilder.Entity<Departamento>()
                .Property(x => x.Descricao)
                .UseCollation("SQL_Latin1_General_CP1_CS_AS");

            // Cria um índice para a entidade e propiedades específicadas
            modelBuilder.Entity<Departamento>()
                .HasIndex(x => new { x.Id, x.Descricao }) //índice
                .IsUnique() // Específica que os campos são únicos
                .HasDatabaseName("idx_descricao") // Nome do índice
                .HasFilter("Descricao IS NOT NULL"); // Filtro do índice

            //modelBuilder.ApplyConfiguration(new DepartamentoMap());
            //modelBuilder.Entity<Departamento>().Property<DateTime>("UltimaAtualizacao");
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}