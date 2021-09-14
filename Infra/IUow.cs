namespace DominandoEfCore.Infra
{
    public interface IUow
    {
        void Commit();
        void Rollback();
    }
}