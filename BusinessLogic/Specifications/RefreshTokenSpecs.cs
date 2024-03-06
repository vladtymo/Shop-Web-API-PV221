using Ardalis.Specification;
using DataAccess.Data.Entities;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic.Specifications
{
    internal static class RefreshTokenSpecs
    {
        internal class ByToken : Specification<RefreshToken>
        {
            public ByToken(string value)
            {
                Query.Where(x => x.Token == value);
            }
        }
        internal class CreatedBy : Specification<RefreshToken>
        {
            public CreatedBy(DateTime date)
            {

                Query.Where(x => x.CreationDate < date);
            }
        }
    }
}
