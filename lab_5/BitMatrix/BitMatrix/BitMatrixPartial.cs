using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public partial class BitMatrix
{
    public BitMatrix And(BitMatrix other)
    {
        CheckOtherBitMatrix(other);

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = data[i] & other.data[i];
        }

        return this;
    }
    public BitMatrix Or(BitMatrix other)
    {
        CheckOtherBitMatrix(other);

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = data[i] | other.data[i];
        }

        return this;
    }
    public BitMatrix Xor(BitMatrix other)
    {
        CheckOtherBitMatrix(other);

        for (int i = 0; i < data.Length; i++)
        {
            data[i] ^= other.data[i];
        }

        return this;
    }
    public BitMatrix Not()
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = !data[i];
        }

        return this;
    }
    private void CheckOtherBitMatrix(BitMatrix other)
    {
        if (other == null) throw new ArgumentNullException();
        if (NumberOfColumns != other.NumberOfColumns || NumberOfRows != other.NumberOfRows) throw new ArgumentException();
    }
    public static BitMatrix operator &(BitMatrix b1, BitMatrix b2)
    {
        if (b1 == null || b2 == null) throw new ArgumentNullException();
        if (b1.NumberOfColumns != b2.NumberOfColumns || b1.NumberOfRows != b2.NumberOfRows) throw new ArgumentException();
        BitMatrix newBitMatrix = (BitMatrix)b1.Clone();
        return newBitMatrix.And(b2);
    }
    public static BitMatrix operator |(BitMatrix b1, BitMatrix b2)
    {
        if (b1 == null || b2 == null) throw new ArgumentNullException();
        if (b1.NumberOfColumns != b2.NumberOfColumns || b1.NumberOfRows != b2.NumberOfRows) throw new ArgumentException();
        BitMatrix newBitMatrix = (BitMatrix)b1.Clone();
        return newBitMatrix.Or(b2);
    }
    public static BitMatrix operator ^(BitMatrix b1, BitMatrix b2)
    {
        if (b1 == null || b2 == null) throw new ArgumentNullException();
        if (b1.NumberOfColumns != b2.NumberOfColumns || b1.NumberOfRows != b2.NumberOfRows) throw new ArgumentException();
        BitMatrix newBitMatrix = (BitMatrix)b1.Clone();
        return newBitMatrix.Xor(b2);
    }
    public static BitMatrix operator !(BitMatrix b1)
    {
        if (b1 == null) throw new ArgumentNullException();
        BitMatrix newBitMatrix = (BitMatrix)b1.Clone();
        return newBitMatrix.Not();
    }
}