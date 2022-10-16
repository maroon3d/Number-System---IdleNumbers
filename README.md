# Number system for Idle games

#### Preview Table
| Standard Number | Idle Number |
| :--- | :--- |
| 1 | `1` |
| 21 | `21` |
| 321 | `321` |
| 4321 | `4.32a` |
| 54321 | `54.32a` |
| 654321 | `654.32a` |
| 7654321 | `7.65b` |
| 87654321 | `87.65b` |
| 987654321 | `987.65b` |
| 1987654321 | `1.98c` |
| - - - | `---` |
| 98748812479311788874568789915778890 | `98.75k` |

# Features:

#### Constructors:
- new IdleNumber(`double` value, `string` expSymbol); `// for example 12, "b"`
- new IdleNumber(`double` value, `uint` exp); `// for example 12, 7`

#### Operator Overrides:
- Equals(), GetHashCode()
- Comparers `==, !=, >, >=, <, <=`
- Addition and Subtraction operators `+, -`
- ToString() 

#### Math Functions:
- Add (`IdleNumber`, `int`, `double`)
- Subtract (`IdleNumber`, `int`, `double`)
- Multiply (`IdleNumber`, `int`, `double`)
- Divide (`IdleNumber`, `int`,`double`)

#### Create IdleNumber with standard numbers:
- CreateFromInt (`int` number)
- CreateFromDouble (`double` number)

#
## Overview
- `IdleNumber` is a struct which stores two values. `double` (_mantissa_) and `uint` _exponent_.
- Exponent is the positive integer number, it is power of `1000`
- `IdleNumber` always remains positive, even if you subtract a larger number, the result will be an _absolute value_.
- DO NOT change `IdleNumber` values directly, use math operations. But if you still need it, first change `Exp` and then `Value`. The property setter automatically normalizes and modifies `_exp` if necessary. (to keep `_value` smaller than `1000`)

```csharp
[Serializable]
public struct IdleNumber
{
    // PRIVATE FIELDS
    private double _value; // always < 1000
    private uint   _exp;   // always >= 0
    
    // PROPERTIES
    public  double Value { get => _value; set => _value = NormalizeValue(value); }
    public  uint   Exp   => _exp;
}
```
Exponent symbols are predefined and easy to change:
```csharp
    private static readonly string[] ExpSymbol = 
    { "", "a", "b", "c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","A", "B", "C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
```
#
## Notes
- `IdleNumber` with value below `1/1000` is `0`.
- if exponent is larger than predefined `ExpSymbols` array. Unity will show a `warning`.
## Examples
- **Constructors**
```csharp
    var idleNumber = new IdleNumber(12, "b") // Create new IdleNumber with expSymbol "b" which is 12,000,000
    var idleNumber = new IdleNumber(12, 2)   // Create new IdleNumber with expValue 2 which is 12,000,000
```
- **Create from numbers**
```csharp
    var idleNumber = IdleNumber.FromInt(7500);          // Create IdleNumber from int, which is 7.5a
    var idleNumber = IdleNumber.FromDouble(1987654321); // Create IdleNumber from double, which is 1.98c
```
- **Comparing**
```csharp
    // Create two IdleNumbers
    var idleNumberA = new IdleNumber(64, "a");  // 64,000
    var idleNumberB = new IdleNumber(570, "b"); // 570,000,000
    
    bool isEqual        = idleNumberA == idleNumberB; // false
    bool notEqual       = idleNumberA != idleNumberB; // true
    bool greater        = idleNumberA >  idleNumberB; // false
    bool greaterOrEqual = idleNumberA >= idleNumberB; // false
    bool less           = idleNumberA <  idleNumberB; // true
    bool lessOrEqual    = idleNumberA <= idleNumberB; // true
```
- **Operations**
```csharp
    // Create two IdleNumbers
    var idleNumberA = new IdleNumber(30, "a"); // 30.00a (30,000)
    var idleNumberB = new IdleNumber(1500);   // 1.50a (1500)
    
    // ADD
    idleNumberA + idleNumberB;          // + operator             |31.50a (31,500)
    idleNumberA.Add(idleNumberB);       // Add() method           |31.50a (31,500)
    dleNumberA.Add(7000);               // Add int or double      |37.00a (37,000)
    
    // SUBTRACT
    idleNumberA - idleNumberB;          // - operator             |28.50a (28,500)
    idleNumberA.Subtract(idleNumberB);  // Add() method           |28.50a (28,500)
    dleNumberA.Subtract(7000);          // Subtract int or double |23.00a (23,000)
    
    // Multiply
    idleNumberA * idleNumberB;          // * operator             |45.00b (45,000,000)
    idleNumberA.Multiply(idleNumberB);  // Multiply() method      |45.00b (45,000,000)
    idleNumberA.Multiply(4.5);          // Multiply int or double |135.00a (135,000)
    
    // Divide
    idleNumberA / idleNumberB;          // / operator             |20 (20)
    idleNumberA.Divide(idleNumberB);    // Divide() method        |20 (20)
    idleNumberA.Divide(4.8);            // Divide int or double   |6.25a (6250)
```
- **Display**
```csharp
    var idleNumber = IdleNumber.FromDouble(1987654321);
    Debug.Log($"You need to collect {idleNumber} gold!"); // "You need to collect 1.98c gold!"
```
## License

MIT

**Free to use!**
༼ つ ◕_◕ ༽つ
