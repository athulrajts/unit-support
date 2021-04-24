namespace System.Units
{

    /// <summary>
    ///     The base type for UnitsNet exceptions.
    /// </summary>
    public class UnitsNetException : Exception
    {
        /// <inheritdoc />
        public UnitsNetException()
        {
            HResult = 1;
        }

        /// <inheritdoc />
        public UnitsNetException(string message) : base(message)
        {
            HResult = 1;
        }

        /// <inheritdoc />
        public UnitsNetException(string message, Exception innerException) : base(message, innerException)
        {
            HResult = 1;
        }
    }

    /// <summary>
    ///     Unit was not found. This is typically thrown for dynamic conversions,
    ///     such as <see cref="UnitConverter.ConvertByName" />.
    /// </summary>
    public class UnitNotFoundException : UnitsNetException
    {
        /// <inheritdoc />
        public UnitNotFoundException()
        {
        }

        /// <inheritdoc />
        public UnitNotFoundException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public UnitNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
