using Badger.Factory;
using Badger.Model;
using Badger.Service;
using NUnit.Framework;

namespace Badger.Test
{
    public class SvgServiceTest
    {
        private ISvgService GetService()
        {
            return new SvgService(new BadgeService(), new BadgeFactory());
        }

        [Test]
        public void SvgServiceTest_Ctor()
        {
            GetService();
        }

        [Test]
        public void SvgServiceTest_Draw()
        {
            /* arrange */
            var service = GetService();
            var p = new ParameterModel { Action = ActionType.CreateImage, Label = "build", Result = "successful", ResultColor = "#00ff00" };

            /* act */
            service.Draw(p);
        }
    }
}
