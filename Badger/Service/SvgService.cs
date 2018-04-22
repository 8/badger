using Badger.Factory;
using Badger.Model;
using SkiaSharp;

namespace Badger.Service
{
    public interface ISvgService
    {
        void Draw(ParameterModel p);
    }

    public class SvgService : ISvgService
    {
        private readonly IBadgeService _BadgeService;
        private readonly IBadgeFactory _BadgeFactory;

        public SvgService(IBadgeService badgeService,
                          IBadgeFactory badgeFactory)
        {
            this._BadgeService = badgeService;
            this._BadgeFactory = badgeFactory;
        }

        public void Draw(ParameterModel p)
        {
            using (var stream = new SKFileWStream(p.OutputFile))
            using (var writer = new SKXmlStreamWriter(stream))
            {
                var badge = this._BadgeFactory.GetBadge(p);
                var width = this._BadgeService.GetWidth(badge);
                var bounds = new SKRect(0, 0, width, p.Height);
                using (var canvas = SKSvgCanvas.Create(bounds, writer))
                {
                    this._BadgeService.DrawBadge(canvas, badge);
                }
            }
        }
    }
}
