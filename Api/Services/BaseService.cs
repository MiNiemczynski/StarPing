using StarPingData.Models.Context;

namespace Api.Services
{
    public class BaseService
    {
        protected StarPingDbContext _context;
        public BaseService(StarPingDbContext context)
        {
            _context = context;
        }
    }
}
