namespace Booket.BuildingBlocks.Domain;

public abstract class IntTypedIdValueBase : IEquatable<IntTypedIdValueBase>
{
    public int Value { get; }

    protected IntTypedIdValueBase(int value)
    {
        if (value == 0)
        {
            throw new InvalidOperationException("Id value cannot be 0!");
        }

        Value = value;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        return obj is IntTypedIdValueBase other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public bool Equals(IntTypedIdValueBase other)
    {
        return Value == other?.Value;
    }

    public static bool operator ==(IntTypedIdValueBase obj1, IntTypedIdValueBase obj2)
    {
        if (Equals(obj1, null))
        {
            if (Equals(obj2, null))
            {
                return true;
            }

            return false;
        }

        return obj1.Equals(obj2);
    }

    public static bool operator !=(IntTypedIdValueBase x, IntTypedIdValueBase y)
    {
        return !(x == y);
    }
}