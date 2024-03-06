using Ardalis.Specification;
using DataAccess.Data.Entities;

namespace BusinessLogic.Specifications
{
    internal static class OrderSpecs
    {
        internal class ByUser : Specification<Order>
        {
            public ByUser(string userId)
            {
                Query
                    .Where(x => x.UserId == userId)
                    .Include(x => x.Products);
            }
        }
    }
}
