using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

// prostokątna macierz bitów o wymiarach m x n
public partial class BitMatrix : IEquatable<BitMatrix>, IEnumerable<int>, ICloneable
{
    private BitArray data;
    public int NumberOfRows { get; }
    public int NumberOfColumns { get; }
    public bool IsReadOnly => false;

    // tworzy prostokątną macierz bitową wypełnioną `defaultValue`
    public BitMatrix(int numberOfRows, int numberOfColumns, int defaultValue = 0)
    {
        if (numberOfRows < 1 || numberOfColumns < 1)
            throw new ArgumentOutOfRangeException("Incorrect size of matrix");
        data = new BitArray(numberOfRows * numberOfColumns, BitToBool(defaultValue));
        NumberOfRows = numberOfRows;
        NumberOfColumns = numberOfColumns;
    }

    public static int BoolToBit(bool boolValue) => boolValue ? 1 : 0;
    public static bool BitToBool(int bit) => bit != 0;

    public override string ToString()
    {
        string result = string.Empty;
        for (int i = 0; i < NumberOfRows; i++)
        {
            for (int j = 0; j < NumberOfColumns; j++)
            {
                result += BoolToBit(data[(i * NumberOfColumns) + j]);
            }
            result += Environment.NewLine;
        }
        return result;
    }

    public BitMatrix(int numberOfRows, int numberOfColumns, params int[] bits)
    {
        if (bits == null || bits.Length == 0)
        {
            NumberOfColumns = numberOfColumns;
            NumberOfRows = numberOfRows;
            data = new BitArray(numberOfRows * numberOfColumns, BitToBool(0));
        }
        else
        {
            NumberOfColumns = numberOfColumns;
            NumberOfRows = numberOfRows;
            data = new BitArray(numberOfRows * numberOfColumns);

            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    var index = (i * numberOfColumns) + j;
                    if (index < bits.Length)
                        data[index] = BitToBool(bits[index]);
                    else data[index] = BitToBool(0);
                }
            }
        }
    }
    public BitMatrix(int[,] bits)
    {
        if (bits == null)
            throw new NullReferenceException();
        if (bits.Length == 0)
            throw new ArgumentOutOfRangeException();

        NumberOfRows = bits.GetLength(0);
        NumberOfColumns = bits.GetLength(1);
        data = new BitArray(NumberOfRows * NumberOfColumns);

        for (int i = 0; i < NumberOfRows; i++)
        {
            for (int j = 0; j < NumberOfColumns; j++)
            {
                data[(i * NumberOfColumns) + j] = BitToBool(bits[i, j]);
            }
        }
    }
    public BitMatrix(bool[,] bits)
    {
        if (bits == null)
            throw new NullReferenceException();
        if (bits.Length == 0)
            throw new ArgumentOutOfRangeException();

        NumberOfRows = bits.GetLength(0);
        NumberOfColumns = bits.GetLength(1);
        data = new BitArray(NumberOfRows * NumberOfColumns);

        for (int i = 0; i < NumberOfRows; i++)
        {
            for (int j = 0; j < NumberOfColumns; j++)
            {
                data[(i * NumberOfColumns) + j] = bits[i, j];
            }
        }
    }

    public bool Equals(BitMatrix other)
    {
        if (other == null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (NumberOfColumns != other.NumberOfColumns || NumberOfRows != other.NumberOfRows) return false;

        for (int i = 0; i < NumberOfRows; i++)
        {
            for (int j = 0; j < NumberOfColumns; j++)
            {
                var index = (i * NumberOfColumns) + j;
                if (data[index] != other.data[index]) return false;
            }
        }
        return true;
    }
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (obj.GetType() == typeof(BitMatrix)) return Equals(obj);
        return false;
    }
    public static bool Equals(BitMatrix b1, BitMatrix b2)
    {
        if (b1 == null && b2 == null) return true;
        if (b1 == null || b2 == null) return false;
        return b1.Equals(b2);
    }
    public override int GetHashCode() => (NumberOfColumns, NumberOfRows).GetHashCode();
    public static bool operator ==(BitMatrix b1, BitMatrix b2)
    {
        if (((object)b1) == null || ((object)b2) == null)
            return object.Equals(b1, b2);

        return b1.Equals(b2);
    }
    public static bool operator !=(BitMatrix b1, BitMatrix b2)
    {
        if (((object)b1) == null || ((object)b2) == null)
            return !object.Equals(b1, b2);

        return !b1.Equals(b2);
    }

    IEnumerator<int> GetEnumerator()
    {
        foreach (bool bit in data)
        {
            yield return BoolToBit(bit);
        }
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    IEnumerator<int> IEnumerable<int>.GetEnumerator()
    {
        return GetEnumerator();
    }
    public int this[int i, int j]
    {
        get
        {
            if (i >= 0 && j >= 0 && i < NumberOfRows && j < NumberOfColumns)
                return BoolToBit(data[(i * NumberOfColumns) + j]);
            else throw new IndexOutOfRangeException();
        }

        set
        {
            if (i >= 0 && j >= 0 && i < NumberOfRows && j < NumberOfColumns)
                data[(i * NumberOfColumns) + j] = BitToBool(value);
            else throw new IndexOutOfRangeException();
        }
    }

    object ICloneable.Clone()
    {
        return Clone();
    }
    public BitMatrix Clone()
    {
        var newBitMatrix = new BitMatrix(NumberOfRows, NumberOfColumns);
        for (int i = 0; i < NumberOfRows; i++)
        {
            for (int j = 0; j < NumberOfColumns; j++)
            {
                newBitMatrix[i, j] = this[i, j];
            }
        }
        return newBitMatrix;
    }

    public static BitMatrix Parse(string s)
    {
        if (string.IsNullOrEmpty(s)) throw new ArgumentNullException();
        var bitsArray = s.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        var columnNumber = bitsArray[0].Length;
        var rowsNumber = bitsArray.Length;
        foreach (var bitsLine in bitsArray)
        {
            if (bitsLine.Length != columnNumber || bitsLine.Any(x => x != '0' && x != '1'))
                throw new FormatException();
        }
        var newBitMatrix = new BitMatrix(rowsNumber, columnNumber);
        for (int i = 0; i < newBitMatrix.NumberOfRows; i++)
        {
            for (int j = 0; j < newBitMatrix.NumberOfColumns; j++)
            {
                newBitMatrix[i, j] = int.Parse(bitsArray[i][j].ToString());
            }
        }
        return newBitMatrix;
    }
    public static bool TryParse(string s, out BitMatrix result)
    {
        try
        {
            result = Parse(s);
        }
        catch (Exception)
        {
            result = null;
            return false;
        }
        return true;
    }

    public static explicit operator BitMatrix(int[,] array)
    {
        if (array == null) throw new NullReferenceException();
        if (array.Length == 0) throw new ArgumentOutOfRangeException();
        return new BitMatrix(array);
    }

    public static implicit operator int[,](BitMatrix bitMatrix)
    {
        var array = new int[bitMatrix.NumberOfRows, bitMatrix.NumberOfColumns];
        for (int i = 0; i < bitMatrix.NumberOfRows; i++)
        {
            for (int j = 0; j < bitMatrix.NumberOfColumns; j++)
            {
                array[i, j] = bitMatrix[i, j];
            }
        }
        return array;
    }

    public static explicit operator BitMatrix(bool[,] array)
    {
        if (array == null) throw new NullReferenceException();
        if (array.Length == 0) throw new ArgumentOutOfRangeException();
        return new BitMatrix(array);
    }

    public static implicit operator bool[,](BitMatrix bitMatrix)
    {
        var array = new bool[bitMatrix.NumberOfRows, bitMatrix.NumberOfColumns];
        for (int i = 0; i < bitMatrix.NumberOfRows; i++)
        {
            for (int j = 0; j < bitMatrix.NumberOfColumns; j++)
            {
                array[i, j] = bitMatrix[i, j] == 1 ? true : false;
            }
        }
        return array;
    }

    public static explicit operator BitArray(BitMatrix bitMatrix)
    {
        if (bitMatrix == null) throw new NullReferenceException();
        if (bitMatrix.NumberOfColumns <= 0 || bitMatrix.NumberOfRows <= 0) throw new ArgumentOutOfRangeException();
        return new BitArray(bitMatrix.data);
    }
}