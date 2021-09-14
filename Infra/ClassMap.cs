using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DominandoEfCore.Infra
{
    public abstract class ClassMap<T> : IEntityTypeConfiguration<T> where T : class
    {
        private readonly string _table;

        protected ClassMap(string table) => _table = table;

        public void Configure(EntityTypeBuilder<T> builder)
        {
            MappingProperties(builder);
            MappingKeys(builder);
            builder.ToTable(_table);
        }

        protected abstract void MappingProperties(EntityTypeBuilder<T> builder);
        protected abstract void MappingKeys(EntityTypeBuilder<T> builder);
    }
}