using Badger.Model;

namespace Badger.Service
{
    public interface ISvgService
    {
        void Draw(ParameterModel p);
    }

    public class SvgService : ISvgService
    {
        public void Draw(ParameterModel p)
        {
            throw new System.NotImplementedException();
        }
    }
}
