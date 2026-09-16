using System;

namespace Session07
{
    // Q1
    public class Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Shape(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double Area()
        {
            return Width * Height;
        }

        public override string ToString()
        {
            return $"(Width = {Width}, Height = {Height})";
        }
    }

    // Q2
    public class Cube : Shape
    {
        public double Depth { get; set; }

        public Cube(double width, double height, double depth) : base(width, height)
        {
            Depth = depth;
        }

        public new double Area()
        {
            return base.Area() * Depth;
        }

        public void Print()
        {
            Console.WriteLine($"Width = {Width}, Height = {Height}, Depth = {Depth}");
        }
    }

    // Q5
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(int id, string name, int age)
        {
            ID = id;
            Name = name;
            Age = age;
        }

        public void Greet()
        {
            Console.WriteLine("I am a Person.");
        }

        public virtual void Display()
        {
            Console.WriteLine($"ID = {ID}, Name = {Name}, Age = {Age}");
        }
    }

    // Q6
    public class Doctor : Person
    {
        public string Specialty { get; set; }

        public Doctor(int id, string name, int age, string specialty) : base(id, name, age)
        {
            Specialty = specialty;
        }

        public new void Greet()
        {
            Console.WriteLine("I am a Doctor.");
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($"Specialty = {Specialty}");
        }
    }

    public class Engineer : Person
    {
        public string Field { get; set; }
        public int YearsOfExperience { get; set; }

        public Engineer(int id, string name, int age, string field, int yearsOfExperience)
            : base(id, name, age)
        {
            Field = field;
            YearsOfExperience = yearsOfExperience;
        }

        public new void Greet()
        {
            Console.WriteLine("I am an Engineer.");
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($"Field = {Field}, YearsOfExperience = {YearsOfExperience}");
        }
    }

    // Q10
    public interface IMoveable
    {
        void MoveForward();
        void MoveBackward();
    }

    public interface IFlyable
    {
        void MoveUp();
        void MoveDown();
    }

    // Q13
    public interface IVehicle : IMoveable, IFlyable
    {
    }

    public class Vehicle : IVehicle
    {
        public virtual void MoveForward() { Console.WriteLine("Vehicle moving forward."); }
        public virtual void MoveBackward() { Console.WriteLine("Vehicle moving backward."); }
        public virtual void MoveUp() { Console.WriteLine("Vehicle moving up."); }
        public virtual void MoveDown() { Console.WriteLine("Vehicle moving down."); }
    }

    // Q11
    public class Car : IMoveable
    {
        public void MoveForward() { Console.WriteLine("Car is moving forward on the ground."); }
        public void MoveBackward() { Console.WriteLine("Car is moving backward on the ground."); }
    }

    public class Ship : IMoveable
    {
        // Q14
        void IMoveable.MoveForward() { Console.WriteLine("Ship is sailing forward on the sea."); }
        public void MoveBackward() { Console.WriteLine("Ship is sailing backward on the sea."); }
    }

    public class Airplane : IMoveable, IFlyable
    {
        public void MoveForward() { Console.WriteLine("Airplane is moving forward in the air."); }
        public void MoveBackward() { Console.WriteLine("Airplane is moving backward in the air."); }
        public void MoveUp() { Console.WriteLine("Airplane is moving up."); }
        public void MoveDown() { Console.WriteLine("Airplane is moving down."); }
    }

    class Program
    {
        // Q7
        static void ProcessPerson(Person person)
        {
            person.Greet();
            person.Display();
        }

        static void Main(string[] args)
        {
            // Q3
            Shape shape = new Shape(2, 3);
            Console.WriteLine(shape.Area()); // 6

            Cube cube = new Cube(2, 3, 4);
            Console.WriteLine(cube.Area()); // 24

            Shape shapeRef = new Cube(2, 3, 4);
            Console.WriteLine(shapeRef.Area());
            // Q3 answer: prints 6, not 24 — Area() is not virtual, so the reference type (Shape) decides which version runs (static binding).

            // Q4
            object obj = new Cube(1, 2, 3);
            Console.WriteLine(obj.ToString());
            // Q4 answer: runs Shape.ToString() (Cube has no override, so it inherits it). ToString() is virtual by default so it's dynamic binding; Area() isn't virtual so it behaved differently.

            // Q7
            Person doctorAsPerson = new Doctor(1, "Mona", 35, "Cardiology");
            ProcessPerson(doctorAsPerson);

            Person engineerAsPerson = new Engineer(2, "Khaled", 30, "Software", 6);
            ProcessPerson(engineerAsPerson);
            // Q7 answer: Greet() resolved at compile time (always Person's), Display() resolved at runtime (derived class's).

            // Q8 answer: removing virtual from Person.Display() causes a compile ERROR in Doctor/Engineer:
            // "cannot override inherited member 'Person.Display()' because it is not marked virtual, abstract, or override."

            // Q9 answer: forcing every vehicle to implement MoveUp()/MoveDown() even if it can't fly (e.g. Car) is wrong design — classes end up with meaningless/empty methods. Interfaces let you split abilities into separate contracts.

            // Q12
            Car car = new Car();
            car.MoveForward();
            Ship ship = new Ship();
            ship.MoveBackward();
            Airplane airplane = new Airplane();
            airplane.MoveUp();

            IMoveable carRef = new Car();
            IMoveable planeRef = new Airplane();
            carRef.MoveForward();
            planeRef.MoveForward();
            // Q12 answer: cannot call planeRef.MoveUp() — planeRef is typed as IMoveable. Need an IFlyable (or Airplane) reference.

            // Q13 answer: grouping multiple interfaces lets a class guarantee it implements all their members together through one reference type.
            Vehicle vehicle = new Vehicle();
            vehicle.MoveForward();

            // Q14
            IMoveable shipAsMoveable = ship;
            shipAsMoveable.MoveForward();
            // Q14 answer: ship.MoveForward() does NOT compile (explicit implementation hides it from Ship's own type). Must call via IMoveable reference: ((IMoveable)ship).MoveForward().
        }
    }
}
