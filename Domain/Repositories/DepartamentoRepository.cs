using DominandoEfCore.Infra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DominandoEfCore.Domain.Repositories
{
    public class DepartamentoRepository : IDepartmentRepository
    {
        public readonly ApplicationContext _context;

        public DepartamentoRepository()
            => _context = new ApplicationContext();

        public void Add(Department departamento)
            => _context.Add(departamento);

        public void Delete(Department departamento) 
        {
            _context.Remove(departamento);
            _context.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
           => _context.Departamentos
                .OrderBy(x => x.Descricao)
                .ToList();

        public Department GetById(Guid id)
            => _context.Departamentos
                .Where(x => id.Equals(x.Id))
                .FirstOrDefault();
    }
}