using Ardalis.Specification;
using DataAccess.Data.Entities;

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
    }
}
