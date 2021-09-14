using System;
using System.Collections.Generic;

namespace DominandoEfCore.Domain
{
    public interface IDepartamentoRepository
    {
        IEnumerable<Departamento> GetAll();
        void Add(Departamento departamento);
        void Delete(Departamento departamento);
        Departamento GetById(Guid id);
    }
}