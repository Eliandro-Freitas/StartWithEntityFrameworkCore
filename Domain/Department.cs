using System;
using System.Collections.Generic;

namespace DominandoEfCore.Domain
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        private readonly List<Employee> _funcionarios = new();
        public IReadOnlyList<Employee> Funcionarios => _funcionarios;
    }
}