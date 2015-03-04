using Signa.Domain.Signs.Static;

namespace Signa.Data.Repository
{
    public interface IRepositoryFactory
    {
        IRepository<Sign> CreateAndLoadStaticSignRepository();
    }
}