﻿namespace System.Units
{
    public interface IQuantity
    {
        /// <summary>
        /// The <see cref="Dimensions" /> of this quantity.
        /// </summary>
        Dimensions Dimensions { get; }


        /// <summary>
        ///     Information about the quantity type, such as unit values and names.
        /// </summary>
        QuantityInfo QuantityInfo { get; }

        /// <summary>
        ///     The unit this quantity was constructed with -or- BaseUnit if default ctor was used.
        /// </summary>
        Enum Unit { get; }

        /// <summary>
        ///     Gets the value in the given unit.
        /// </summary>
        /// <param name="unit">The unit enum value. The unit must be compatible, so for <see cref="Length"/> you should provide a <see cref="LengthUnit"/> value.</param>
        /// <returns>Value converted to the specified unit.</returns>
        /// <exception cref="InvalidCastException">Wrong unit enum type was given.</exception>
        double As(Enum unit);

        /// <summary>
        ///     Gets the value in the unit determined by the given <see cref="UnitSystem"/>. If multiple units were found for the given <see cref="UnitSystem"/>,
        ///     the first match will be used.
        /// </summary>
        /// <param name="unitSystem">The <see cref="UnitSystem"/> to convert the quantity value to.</param>
        /// <returns>The converted value.</returns>
        double As(UnitSystem unitSystem);

        /// <summary>
        ///     The value this quantity was constructed with. See also <see cref="Unit"/>.
        /// </summary>
        double Value { get; }

        /// <summary>
        ///     Converts to a quantity with the given unit representation, which affects things like <see cref="IQuantity.ToString(System.IFormatProvider)"/>.
        /// </summary>
        /// <param name="unit">The unit enum value. The unit must be compatible, so for <see cref="Length"/> you should provide a <see cref="LengthUnit"/> value.</param>
        /// <returns>A new quantity with the given unit.</returns>
        IQuantity ToUnit(Enum unit);

        /// <summary>
        ///     Converts to a quantity with a unit determined by the given <see cref="UnitSystem"/>, which affects things like <see cref="IQuantity.ToString(System.IFormatProvider)"/>.
        ///     If multiple units were found for the given <see cref="UnitSystem"/>, the first match will be used.
        /// </summary>
        /// <param name="unitSystem">The <see cref="UnitSystem"/> to convert the quantity to.</param>
        /// <returns>A new quantity with the determined unit.</returns>
        IQuantity ToUnit(UnitSystem unitSystem);

        /// <summary>
        ///     Gets the string representation of value and unit. Uses two significant digits after radix.
        /// </summary>
        /// <returns>String representation.</returns>
        /// <param name="provider">Format to use for localization and number formatting. Defaults to <see cref="CultureInfo.CurrentUICulture" /> if null.</param>
        string ToString(IFormatProvider provider);

    }

    /// <summary>
    ///     A stronger typed interface where the unit enum type is known, to avoid passing in the
    ///     wrong unit enum type and not having to cast from <see cref="Enum"/>.
    /// </summary>
    /// <example>
    ///     IQuantity{LengthUnit} length;
    ///     double centimeters = length.As(LengthUnit.Centimeter); // Type safety on enum type
    /// </example>
    public interface IQuantity<TUnitType> : IQuantity
        where TUnitType : Enum
    {
        /// <summary>
        ///     Convert to a unit representation <typeparamref name="TUnitType"/>.
        /// </summary>
        /// <returns>Value converted to the specified unit.</returns>
        double As(TUnitType unit);

        /// <inheritdoc cref="IQuantity.Unit"/>
        new TUnitType Unit { get; }

        /// <inheritdoc cref="IQuantity.QuantityInfo"/>
        new QuantityInfo<TUnitType> QuantityInfo { get; }

        /// <summary>
        ///     Converts to a quantity with the given unit representation, which affects things like <see cref="IQuantity.ToString(System.IFormatProvider)"/>.
        /// </summary>
        /// <param name="unit">The unit enum value.</param>
        /// <returns>A new quantity with the given unit.</returns>
        IQuantity<TUnitType> ToUnit(TUnitType unit);

        /// <summary>
        ///     Converts to a quantity with a unit determined by the given <see cref="UnitSystem"/>, which affects things like <see cref="IQuantity.ToString(System.IFormatProvider)"/>.
        ///     If multiple units were found for the given <see cref="UnitSystem"/>, the first match will be used.
        /// </summary>
        /// <param name="unitSystem">The <see cref="UnitSystem"/> to convert the quantity to.</param>
        /// <returns>A new quantity with the determined unit.</returns>
        new IQuantity<TUnitType> ToUnit(UnitSystem unitSystem);

    }
}
