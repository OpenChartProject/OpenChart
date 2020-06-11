using System;

namespace OpenChart.UI
{
    /// <summary>
    /// Represents a RGBA color.
    /// </summary>
    public class Color
    {
        double _red;

        /// <summary>
        /// The red color value. Valid range is from 0 to 1.0
        /// </summary>
        public double Red
        {
            get => _red;
            set
            {
                checkValue(value);
                _red = value;
            }
        }

        double _green;

        /// <summary>
        /// The green color value. Valid range is from 0 to 1.0
        /// </summary>
        public double Green
        {
            get => _green;
            set
            {
                checkValue(value);
                _green = value;
            }
        }

        double _blue;

        /// <summary>
        /// The blue color value. Valid range is from 0 to 1.0
        /// </summary>
        public double Blue
        {
            get => _blue;
            set
            {
                checkValue(value);
                _blue = value;
            }
        }

        double _alpha;

        /// <summary>
        /// The alpha value. Valid range is from 0 (invisible) to 1.0 (opaque)
        /// </summary>
        public double Alpha
        {
            get => _alpha;
            set
            {
                checkValue(value);
                _alpha = value;
            }
        }

        /// <summary>
        /// Creates a new Color instance.
        /// </summary>
        public Color(double red, double green, double blue, double alpha = 1.0f)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        /// <summary>
        /// Returns the color as a <see cref="Cairo.Color" /> instance.
        /// </summary>
        public Cairo.Color AsCairoColor()
        {
            return new Cairo.Color
            {
                R = Red,
                G = Green,
                B = Blue,
                A = Alpha
            };
        }

        /// <summary>
        /// Returns the color as a <see cref="Gdk.RGBA" /> instance.
        /// </summary>
        public Gdk.RGBA AsGdkRGBA()
        {
            return new Gdk.RGBA
            {
                Red = Red,
                Green = Green,
                Blue = Blue,
                Alpha = Alpha
            };
        }

        /// <summary>
        /// Checks the color value and throws an exception if it's out of range.
        /// </summary>
        private void checkValue(double val)
        {
            if (val < 0 || val > 1)
                throw new ArgumentOutOfRangeException("Color value must be between zero and one.");
        }
    }
}
