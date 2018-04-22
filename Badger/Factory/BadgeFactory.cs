using Badger.Model;

namespace Badger.Factory
{
    public interface IBadgeFactory
    {
        BadgeModel GetBadge(ParameterModel p);
    }

    public class BadgeFactory : IBadgeFactory
    {
        public BadgeModel GetBadge(ParameterModel p)
        {
            return new BadgeModel
            {
                Label = p.Label,
                Result = p.Result,
                Height = p.Height
            };
        }
    }
}
