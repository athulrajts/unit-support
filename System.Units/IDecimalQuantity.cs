namespace System.Units
{
    /// <summary>
    ///     Represents a quantity with a decimal value.
    /// </summary>
    public interface IDecimalQuantity
    {
        /// <summary>
        ///     The decimal value this quantity was constructed with.
        /// </summary>
        decimal Value { get; }
    }
}
