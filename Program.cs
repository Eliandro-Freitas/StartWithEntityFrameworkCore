using DominandoEfCore.Domain;
using DominandoEfCore.Domain.Repositories;
using DominandoEfCore.Infra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DominandoEfCore
{
    public class Program
    {
        public readonly static ApplicationContext _context = new();
        public readonly static IDepartmentRepository _departamentoRepository = new DepartamentoRepository();
        public readonly static List<Department> _departamentos = new();
        public readonly static int _count = 0;

        static void Main(string[] args)
        {
            var uow = new Uow();
            var departamento = new Department 
            {
                Id = Guid.NewGuid(),
                Descricao = "Departamento 1",
                Ativo = true
            };

            _departamentoRepository.Add(departamento);
            uow.Commit();
        }

        static void ExecutarEstrategiaResiliencia()
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using var transaction = _context.Database.BeginTransaction();
                _context.Departamentos.Add(new Department
                {
                    Id = Guid.NewGuid(),
                    Descricao = "Departamento 7",
                    Ativo = false
                });

                transaction.Commit();
            });
        }

        static void PropiedadesDeSombra()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var departamento = new Department
            {
                Descricao = "Propiedade Sombra"
            };

            _context.Entry(departamento).Property("UltimaAtualizacao").CurrentValue = DateTime.Now.Date;
            _context.SaveChanges();
        }

        static void AplicarMigracaoTempoExecucao()
            => _context.Database.Migrate();

        static void MigracoesPendentes()
        {
            var migracoesPendentes = _context.Database.GetPendingMigrations();
            Console.WriteLine($"Migrações pendentes: {migracoesPendentes.Count()}");

            foreach (var migracao in migracoesPendentes)
            {
                Console.WriteLine($"Migração: {migracao}");
            }
        }

        static void HealthCheckDataBase()
        {
            // CanConnect testa a conexão com o banco
            var canConnect = _context.Database.CanConnect();
            if (canConnect)
            {
                Console.WriteLine("Conectado");
            }
            else
            {
                Console.WriteLine("Não conectado");
            }
        }

        static void GerenciarEstadoConexao(bool estadoConexao)
        {
            var connection = _context.Database.GetDbConnection();
            //connection.StateChange += (_, __) => ++_count;
            var time = Stopwatch.StartNew();

            if (estadoConexao)
            {
                connection.Open();
            }

            for (int i = 0; i < 200; i++)
            {
                _context.Departamentos.AsNoTracking().Any();
            }

            time.Stop();
            Console.WriteLine($"Tempo : {time.Elapsed}, {estadoConexao}, {_count}");
        }

        static void EnsureCreatedAndDelete()
            => _context.Database.EnsureCreated();

        static void AsSplitQuery()
            => _context.Departamentos
                .Where(x => !x.Ativo)
                .AsSplitQuery();
    }
}