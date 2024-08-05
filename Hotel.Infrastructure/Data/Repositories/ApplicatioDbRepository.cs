
using Hotel.Infrastructure;
using Hotel.Infrastructure.Data.Common;
using Hotel.Infrastructure.Data.Repositories;

namespace Hotel.Infrastructure.Data.Repositories
{
    public class ApplicatioDbRepository : Repository, IApplicatioDbRepository
    {
        public ApplicatioDbRepository(ApplicationDbContext context)
        {
            this.Context = context;
        }
    }
}
