using Signa.Domain.Signs.Static;

namespace Signa.Data.Repository
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly string repositoryDataFilePath;

        private static IRepository<Sign> staticSignRepository; 

        public RepositoryFactory(string repositoryDataFilePath)
        {
            this.repositoryDataFilePath = repositoryDataFilePath;
        }

        public IRepository<Sign> CreateAndLoadStaticSignRepository()
        {
            if (staticSignRepository == null)
            {
                staticSignRepository = new StaticSignRepository(repositoryDataFilePath);
                staticSignRepository.Load();    
            }
            
            return staticSignRepository;
        }
    }
}