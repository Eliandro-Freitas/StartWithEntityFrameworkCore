using System;
using System.Collections.Generic;

namespace DominandoEfCore.Domain
{
    public class Departamento
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        private readonly List<Funcionario> _funcionarios = new();
        public IReadOnlyList<Funcionario> Funcionarios => _funcionarios;
    }
}