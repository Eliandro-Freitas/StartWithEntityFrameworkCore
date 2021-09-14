using System;
using System.Collections.Generic;

namespace DominandoEfCore.Domain
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        void Add(Department departamento);
        void Delete(Department departamento);
        Department GetById(Guid id);
    }
}