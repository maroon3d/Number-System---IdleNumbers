using System;
using System.Linq;
using UnityEngine;

[Serializable]
public struct IdleNumber
{
    #region DATA
    // EXPONENT SYMBOLS
    private static readonly string[] ExpSymbol = { 
        "", "a", "b", "c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
        "A", "B", "C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
    
    private const int TenCubed = 1000; 
    
    // PRIVATE FIELDS
    private double _value; // always < 1000
    private uint   _exp;   // always >= 0
    
    // PROPERTIES
    public  double Value { get => _value; set => _value = NormalizeValue(value); }
    public  uint   Exp   => _exp;

    #endregion
    
    // NORMALIZE IDLE NUMBER
    private double NormalizeValue(double value)
    {
        // ABSOLUTE VALUE
        double result = value >= 0 ? value : -value;
        
        // MOVE POINT FLOAT LEFT
        while (result > TenCubed)
        {
            result /= TenCubed;
            _exp    += 1;
        }
        
        // MOVE POINT FLOAT RIGHT
        while (result < 1f / TenCubed)
        {
            // ZERO LIMIT
            if (_exp > 0)
            {
                result *= TenCubed;
                _exp   -= 1;
            }
            else
            {
                result = 0;
                _exp   = 0;
                break;
            }
        }
        
        
        return result;
    }

    #region CONSTRUCTOR
    /// <summary>
    /// For example value = 12, expSymbol = "c"
    /// </summary>
    /// <param name="value"></param>
    /// <param name="expSymbol"></param>
    public IdleNumber(double value = 0, string expSymbol = "")
    {
        _value = double.MinValue;

        if (!ExpSymbol.Contains(expSymbol))
        {
            Debug.LogWarning("Invalid IdleNumber Exp Symbol");
            expSymbol = "";
        }
        
        _exp  = expSymbol == "" ? 0 : (uint)Array.IndexOf(ExpSymbol, expSymbol);
        Value = value;
    }
    
    /// <summary>
    /// For example value = 7, exp = "3"
    /// </summary>
    /// <param name="value"></param>
    /// <param name="exp"></param>
    public IdleNumber(double value = 0, uint exp = 0)
    {
        _value = double.MinValue;
        _exp   = exp;
        Value  = value;
    }
    #endregion
    
    // PRINT
    public override string ToString()
    {
        string stringValue = Value.ToString(_exp < 1 ? "" : "F2");
        string stringExp = _exp < ExpSymbol.Length ? ExpSymbol[_exp] : "symbolUndefined";
        return $"IdleNumber: {stringValue}{stringExp}";
    }

    #region CREATE FROM NUMBER
    // CREATE FROM INT
    public static IdleNumber FromInt(int _number) { return new IdleNumber(_number, 0); }
    // CREATE FROM DOUBLE
    public static IdleNumber FromDouble(double _number) { return new IdleNumber(_number, 0); }
    #endregion
    
    #region COMPARING

    // GET HASH
    public override int GetHashCode() { return HashCode.Combine(Value, _exp); }

    // EQUALS
    public override bool Equals(object obj) { return Equals(obj); }

    // EQUALITY
    public static bool operator ==(IdleNumber x, IdleNumber y) { return x.Value.CompareTo(y.Value) == 0 && x._exp.CompareTo(y._exp) == 0; }

    // NOT EQUALS
    public static bool operator !=(IdleNumber x, IdleNumber y) { return !(x.Value.CompareTo(y.Value) == 0 && x._exp.CompareTo(y._exp) == 0); }

    // GREATER
    public static bool operator >(IdleNumber x, IdleNumber y) { return x._exp > y._exp || (x._exp == y._exp && x._value > y._value); }

    // GREATER or EQUAL
    public static bool operator >=(IdleNumber x, IdleNumber y) { return x._exp > y._exp || (x._exp == y._exp && x._value >= y._value); }

    // LESS
    public static bool operator <(IdleNumber x, IdleNumber y) { return x._exp < y._exp || (x._exp == y._exp && x._value < y._value); }

    // LESS or EQUAL
    public static bool operator <=(IdleNumber x, IdleNumber y) { return x._exp < y._exp || (x._exp == y._exp && x._value <= y._value); }

    #endregion

    #region OPERATIONS

    // ADD
    public static IdleNumber operator +(IdleNumber x, IdleNumber y)      { return Math_Add(x, y); }
    public static IdleNumber operator +(IdleNumber x, int        number) { return Math_Add(x, FromInt(number)); }
    public static IdleNumber operator +(IdleNumber x, double     number) { return Math_Add(x, FromDouble(number)); }
    public        IdleNumber Add(IdleNumber        idleNumber) { return this += idleNumber; }
    public        IdleNumber Add(int               number)     { return this += FromInt(number); }
    public        IdleNumber Add(double            number)     { return this += FromDouble(number); }

    // SUBTRACT
    public static IdleNumber operator -(IdleNumber x, IdleNumber y)      { return Math_Subtract(x, y); }
    public static IdleNumber operator -(IdleNumber x, int        number) { return Math_Subtract(x, FromInt(number)); }
    public static IdleNumber operator -(IdleNumber x, double     number) { return Math_Subtract(x, FromDouble(number)); }
    public        IdleNumber Subtract(IdleNumber   idleNumber) { return this -= idleNumber; }
    public        IdleNumber Subtract(int          number)     { return this -= FromInt(number); }
    public        IdleNumber Subtract(double       number)     { return this -= FromDouble(number); }
    
    // MULTIPLY
    public static IdleNumber operator *(IdleNumber x, IdleNumber y)      { return Math_Multiply(x, y); }
    public static IdleNumber operator *(IdleNumber x, int        number) { return Math_Multiply(x, FromInt(number)); }
    public static IdleNumber operator *(IdleNumber x, double     number) { return Math_Multiply(x, FromDouble(number)); }
    public        IdleNumber Multiply(IdleNumber   idleNumber) { return this *= idleNumber; }
    public        IdleNumber Multiply(int          number)     { return this *= number; }
    public        IdleNumber Multiply(double       number)     { return this *= number; }
    
    // DIVIDE
    public static IdleNumber operator /(IdleNumber x, IdleNumber y)      { return Math_Divide(x, y); }
    public static IdleNumber operator /(IdleNumber x, int        number) { return Math_Divide(x, FromInt(number)); }
    public static IdleNumber operator /(IdleNumber x, double     number) { return Math_Divide(x, FromDouble(number)); }
    public        IdleNumber Divide(IdleNumber     idleNumber) { return this /= idleNumber; }
    public        IdleNumber Divide(int            number)     { return this /= number; }
    public        IdleNumber Divide(double         number)     { return this /= number; }
    #endregion
    
    #region MATH CALCULATIONS
    // ADD
    private static IdleNumber Math_Add(IdleNumber x, IdleNumber y)
    {
        IdleNumber newIdleNumber = x;
        
        // MODIFY VALUE COMPARING EXP
        uint expDelta = (uint)Mathf.Abs((int)x._exp - (int)y._exp);
        for (int i = 0; i < expDelta; i++)
        {
            if (x._exp > y._exp) y._value /= TenCubed;
            else                 x._value /= TenCubed;
        }

        // ASSIGN NEW VALUES
        newIdleNumber._exp  = x._exp   + expDelta;
        newIdleNumber.Value = x._value + y._value;
        
        return newIdleNumber;
    }
    
    // SUBTRACT
    private static IdleNumber Math_Subtract(IdleNumber x, IdleNumber y)
    {
        IdleNumber newIdleNumber = x;
        
        // MODIFY VALUE COMPARING EXP
        uint expDelta = (uint)Mathf.Abs((int)x._exp - (int)y._exp);
        for (int i = 0; i < expDelta; i++)
        {
            if (x._exp > y._exp) y._value /= TenCubed;
            else                 x._value /= TenCubed;
        }

        // ASSIGN NEW VALUES
        newIdleNumber._exp  = x._exp   + expDelta;
        newIdleNumber.Value = x._value - y._value;
        
        return newIdleNumber;
    }
    
    // MULTIPLY
    private static IdleNumber Math_Multiply(IdleNumber x, IdleNumber y)
    {
        IdleNumber newIdleNumber = x;
        
        // ASSIGN NEW VALUES
        newIdleNumber._exp  = x._exp + y._exp;
        newIdleNumber.Value = x._value * y._value;
        
        return newIdleNumber;
    } 
    
    // DIVIDE
    private static IdleNumber Math_Divide(IdleNumber x, IdleNumber y)
    {
        IdleNumber newIdleNumber = x;
        
        // ZERO LIMIT
        if (y._exp > x._exp || y._value == 0) return new IdleNumber();
        
        // ASSIGN NEW VALUES
        newIdleNumber._exp  = x._exp - y._exp;
        newIdleNumber.Value = x._value / y._value;
        
        return newIdleNumber;
    } 
    #endregion
}
