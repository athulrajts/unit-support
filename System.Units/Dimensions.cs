using System.Linq;
using System.Text;

namespace System.Units
{
    public sealed class Dimensions
    {
        /// <summary>
        /// Gets the length dimensions (L).
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Gets the mass dimensions (M).
        /// </summary>
        public int Mass { get; }

        /// <summary>
        /// Gets the time dimensions (T).
        /// </summary>
        public int Time { get; }

        /// <summary>
        /// Gets the electric current dimensions (I).
        /// </summary>
        public int Current { get; }

        /// <summary>
        /// Gets the temperature dimensions (Θ).
        /// </summary>
        public int Temperature { get; }

        /// <summary>
        /// Gets the amount of substance dimensions (N).
        /// </summary>
        public int Amount { get; }

        /// <summary>
        /// Gets the luminous intensity dimensions (J).
        /// </summary>
        public int LuminousIntensity { get; }


        public Dimensions(int length, int mass, int time, int current, int temperature, int amount, int luminousIntensity)
        {
            Length = length;
            Mass = mass;
            Time = time;
            Current = current;
            Temperature = temperature;
            Amount = amount;
            LuminousIntensity = luminousIntensity;
        }

        /// <summary>
        /// Checks if the dimensions represent a base quantity.
        /// </summary>
        /// <returns>True if the dimensions represent a base quantity, otherwise false.</returns>
        public bool IsBaseQuantity()
        {
            var dimensionsArray = new int[] { Length, Mass, Time, Current, Temperature, Amount, LuminousIntensity };
            bool onlyOneEqualsOne = dimensionsArray.Where(dimension => dimension == 1).Count() == 1 && dimensionsArray.Where(dimension => dimension == 0).Count() == 6;
            return onlyOneEqualsOne;
        }

        /// <summary>
        /// Checks if the dimensions represent a derived quantity.
        /// </summary>
        /// <returns>True if the dimensions represent a derived quantity, otherwise false.</returns>
        public bool IsDerivedQuantity()
        {
            return !IsBaseQuantity() && !IsDimensionless();
        }

        /// <summary>
        /// Checks if this base dimensions object represents a dimensionless quantity.
        /// </summary>
        /// <returns>True if this object represents a dimensionless quantity, otherwise false.</returns>
        public bool IsDimensionless()
        {
            return this == Dimensionless;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is Dimensions))
            {
                return false;
            }

            var other = (Dimensions)obj;

            return Length == other.Length &&
                Mass == other.Mass &&
                Time == other.Time &&
                Current == other.Current &&
                Temperature == other.Temperature &&
                Amount == other.Amount &&
                LuminousIntensity == other.LuminousIntensity;
        }


        /// <inheritdoc />
        public override int GetHashCode()
        {
            return new { Length, Mass, Time, Current, Temperature, Amount, LuminousIntensity }.GetHashCode();
        }

        /// <summary>
        /// Get resulting dimensions after multiplying two dimensions, by performing addition of each dimension.
        /// </summary>
        /// <param name="right">Other dimensions.</param>
        /// <returns>Resulting dimensions.</returns>
        public Dimensions Multiply(Dimensions right)
        {
            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return new Dimensions(
                Length + right.Length,
                Mass + right.Mass,
                Time + right.Time,
                Current + right.Current,
                Temperature + right.Temperature,
                Amount + right.Amount,
                LuminousIntensity + right.LuminousIntensity);
        }

        /// <summary>
        /// Get resulting dimensions after dividing two dimensions, by performing subtraction of each dimension.
        /// </summary>
        /// <param name="right">Other dimensions.</param>
        /// <returns>Resulting dimensions.</returns>
        public Dimensions Divide(Dimensions right)
        {
            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return new Dimensions(
                Length - right.Length,
                Mass - right.Mass,
                Time - right.Time,
                Current - right.Current,
                Temperature - right.Temperature,
                Amount - right.Amount,
                LuminousIntensity - right.LuminousIntensity);
        }

        /// <summary>
        /// Check if two dimensions are equal.
        /// </summary>
        /// <param name="left">Left.</param>
        /// <param name="right">Right.</param>
        /// <returns>True if equal.</returns>
        public static bool operator ==(Dimensions left, Dimensions right)
        {
            return left is null ? right is null : left.Equals(right);
        }

        /// <summary>
        /// Check if two dimensions are unequal.
        /// </summary>
        /// <param name="left">Left.</param>
        /// <param name="right">Right.</param>
        /// <returns>True if not equal.</returns>
        public static bool operator !=(Dimensions left, Dimensions right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Multiply two dimensions.
        /// </summary>
        /// <param name="left">Left.</param>
        /// <param name="right">Right.</param>
        /// <returns>Resulting dimensions.</returns>
        public static Dimensions operator *(Dimensions left, Dimensions right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));
            else if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left.Multiply(right);
        }

        /// <summary>
        /// Divide two dimensions.
        /// </summary>
        /// <param name="left">Left.</param>
        /// <param name="right">Right.</param>
        /// <returns>Resulting dimensions.</returns>
        public static Dimensions operator /(Dimensions left, Dimensions right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));
            else if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left.Divide(right);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder();

            AppendDimensionString(sb, "Length", Length);
            AppendDimensionString(sb, "Mass", Mass);
            AppendDimensionString(sb, "Time", Time);
            AppendDimensionString(sb, "Current", Current);
            AppendDimensionString(sb, "Temperature", Temperature);
            AppendDimensionString(sb, "Amount", Amount);
            AppendDimensionString(sb, "LuminousIntensity", LuminousIntensity);

            return sb.ToString();
        }

        private static void AppendDimensionString(StringBuilder sb, string name, int value)
        {
            var absoluteValue = Math.Abs(value);

            if (absoluteValue > 0)
            {
                sb.AppendFormat("[{0}]", name);

                if (absoluteValue > 1)
                    sb.AppendFormat("^{0}", value);
            }
        }


        /// <summary>
        /// Represents a dimensionless (unitless) quantity.
        /// </summary>
        public static Dimensions Dimensionless { get; } = new Dimensions(0, 0, 0, 0, 0, 0, 0);
    }
}
