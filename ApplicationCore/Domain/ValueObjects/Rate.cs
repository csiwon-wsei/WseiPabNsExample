using ApplicationCore.Domain.Exceptions;

namespace ApplicationCore.Domain.ValueObjects;

public class Rate
{
    private uint _value;
    public uint Value => _value;
    private Rate(uint value)
    {
        _value = value;
    }

    public static Rate Of(uint value)
    {
        if (value > 10)
        {
            throw new InvalidRateValueException($"Invalid rate value: {value}!");
        }
        return new Rate(value);
    }
}