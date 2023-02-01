using System;

namespace RobotArmApp.Source
{
    public class FloatRange
    {
        private float minimum;

        public float Minimum
        {
            get => minimum;
            set
            {
                CheckMinMax(value, maximum);
                minimum = value;
            }
        }

        private float maximum;

        public float Maximum
        {
            get => maximum;
            set
            {
                CheckMinMax(minimum, value);
                maximum = value;
            }
        }

        public float Size => maximum - minimum;

        private static void CheckMinMax(float minimum, float maximum)
        {
            if (minimum > maximum)
            {
                throw new ArgumentException("Min greater than max");
            }
        }

        public FloatRange(FloatRange range)
        {
            minimum = range.minimum;
            maximum = range.maximum;
        }

        public FloatRange(float minimum, float maximum)
        {
            CheckMinMax(minimum, maximum);
            this.minimum = minimum;
            this.maximum = maximum;
        }

        public Func<float, float> CreateMappingFunction(FloatRange range)
        {
            return x => (x - minimum) * (range.Size / Size) + range.minimum;
        }

        public float MapValue(float x, FloatRange range)
        {
            return CreateMappingFunction(range)(x);
        }
    }
}