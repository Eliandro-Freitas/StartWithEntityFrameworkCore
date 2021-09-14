using System;

namespace DominandoEfCore.Domain
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }

        public Guid DepartamentoId { get; set; }
        public Department Departamento { get; set; }
    }
}