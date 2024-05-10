
namespace CtrlAltJam3
{
    public class MathUtils
    {
        public static int Wrap(int input, int minInclusive, int maxExclusive)
        {
            if (input < minInclusive)
            {
                return maxExclusive - (minInclusive - input) % (maxExclusive - minInclusive);
            }
            else
            {
                return minInclusive + (input - minInclusive) % (maxExclusive - minInclusive);
            }
        }
    }
}
