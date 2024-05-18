
using static UnityEngine.EventSystems.EventTrigger;

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

        public static int Limit(int input, int minInclusive, int maxInclusive)
        {
            if (input < minInclusive)
            {
                return minInclusive;
            }
            else if (input > maxInclusive)
            {
                return maxInclusive;
            }
            return input;
        }
        public static float ConverterPorcentageToDegrees(float value)
        {
            float toConvertValue = 0f;
            switch (value)
            {
                case <= 0:
                    toConvertValue = 0f;
                    break;
                case >= 100:
                    toConvertValue = 100f;
                    break;
                default:
                    toConvertValue = value;
                    break;
            }
            return (toConvertValue * 360f) / 100f;
        }
    }
}
