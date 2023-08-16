1. Declare an Interface:
* Interface cannot have data fields.
* Interface cannot have constructors.
* Interface don't provide an implementation of member.
* Interface can use read-only property.

2. Property and method (read-only, read-write, write-only property).

3. Invoke interface members
Using try catch when you want to cast into interface type.
Another way is using as keyword to cast. If it is possible, result is null.
You can use the is keyword: myShapes[i] is IPointy ip.

4. Interface cannot have modifier.

5. Explicit implememtation
class Octagon : IDrawToForm, IDrawToMemory, IDrawToPrinter
{
 // Explicitly bind Draw() implementations
 // to a given interface.
 void IDrawToForm.Draw()
 {
 Console.WriteLine("Drawing to form...");
 }
 void IDrawToMemory.Draw()
 {
 Console.WriteLine("Drawing to memory...");
 }
 void IDrawToPrinter.Draw()
 {
 Console.WriteLine("Drawing to a printer...");
 }
}

******* NOTICE *******: those implementations do not need modifier. All implementations are automatically private. To use Draw method, you need to use casting into interface type (because interface do not use modifiers and you can easily access them).

6. Interface hierarchy
In case that multiple inheritance interface has name clash

// Multiple inheritance for interface types is a-okay.
interface IDrawable
{
 void Draw();
}
interface IPrintable
{
 void Print();
 void Draw(); // <-- Note possible name clash here!
}
// Multiple interface inheritance. OK!
interface IShape : IDrawable, IPrintable
{
 int GetNumberOfSides();
}

=> how to solve this clash. 

the first solution

class Rectangle : IShape
{
 public int GetNumberOfSides()
 { return 4; }
 public void Draw()
 { Console.WriteLine("Drawing..."); }
 public void Print()
 { Console.WriteLine("Printing..."); }
}

the second solution

class Square : IShape
{
 // Using explicit implementation to handle member name clash.
 void IPrintable.Draw()
 {
 // Draw to printer ...
 }
 void IDrawable.Draw()
 {
 // Draw to screen ...
 }
 public void Print()
 {
 // Print ...
 }
 public int GetNumberOfSides()
 { return 4; }
}

7. Interesting keyword: yield

public class Garage : IEnumerable
{
 private Car[] carArray = new Car[4];
 ...
 // Iterator method.
 public IEnumerator GetEnumerator()
 {
 foreach (Car c in carArray)
 {
 yield return c;
 }
 }
}

public IEnumerator GetEnumerator()
{
 yield return carArray[0];
 yield return carArray[1];
 yield return carArray[2];
 yield return carArray[3];
}
