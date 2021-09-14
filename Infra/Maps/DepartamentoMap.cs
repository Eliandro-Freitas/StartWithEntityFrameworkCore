using DominandoEfCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DominandoEfCore.Infra.Maps
{
    public class DepartamentoMap : IEntityTypeConfiguration<Departamento>
    {
        public void Configure(EntityTypeBuilder<Departamento> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Funcionarios).WithOne(x => x.Departamento).HasForeignKey(x => x.DepartamentoId);
        }
    }
}