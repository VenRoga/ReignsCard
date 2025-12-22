using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Shapes
{
    
    public abstract class Shape
    {
        // Shapes can be assigned a friendly pet name.
        public string petName { get; set; }
        public string color { get; set; }
        public int points { get; set; }
        public byte number { get; set; }

        // Constructors.
        public Shape() 
        { 
            petName = "NoName";
            color = "white";
            points = 0;
            number = 0;
        }
        public Shape(string s, string c, int n, byte k) 
        { 
            petName = s;
            color = c;
            points = n;
            number = k;
        }
        // Draw() is now completely abstract (note semicolon).
        public abstract void Draw();
        public override string ToString()
        {
            return string .Format ("{0} {1} {2} {3}", petName , color ,points ,number);
        }
    }
    
    public class Circle : Shape
    {
        public Circle() { }
        public Circle(string name, string c, byte r) : base(name, c, 0, r) { }

        // Now Circle must decide how to render itself.
        public override void Draw()
        {
            Console.WriteLine("Drawing {0} the Circle", petName);
        }
    }
    public class Hexagon : Shape
    {
        public Hexagon() 
        {
            points = 6;
        }
        public Hexagon(string name, string c, byte d) : base(name, c, 6, d) { }
        public override void Draw()
        {
            Console.WriteLine("Drawing {0} the Hexagon", petName);
        }
    }
   
}
