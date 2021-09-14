using System;

namespace DominandoEfCore.Infra
{
    class Uow : IUow
    {
        private readonly ApplicationContext _context;

        public Uow()
            => _context = new ApplicationContext();

        public void Commit()
            => _context.SaveChanges();

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
