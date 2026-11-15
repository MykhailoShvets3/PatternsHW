// Завдання 4. Шаблон "Декоратор" - ялинка.
using System;
using System.Text;

namespace Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; 

        
            Console.WriteLine("1. Проста ялинка:");
            Tree tree = new ChristmasTree();
            tree.Show();
            tree.Light();

           
            Console.WriteLine("\n2. Ялинка з прикрасами:");
            tree = new OrnamentsDecorator(new ChristmasTree(), "кульки, зірка, цукерки");
            tree.Show();
            tree.Light();

           
            Console.WriteLine("\n3. Ялинка з гірляндою:");
            tree = new GarlandDecorator(new ChristmasTree());
            tree.Show();
            tree.Light();

          
            Console.WriteLine("\n4. Ялинка з прикрасами і гірляндою:");
            tree = new GarlandDecorator(
                new OrnamentsDecorator(new ChristmasTree(), "кульки, зірка, цукерки"));
            tree.Show();
            tree.Light();

            Console.ReadKey();
        }
    }

    // Component - те, що будемо прикрашати
    abstract class Tree
    {
        public abstract void Show();

       
        public virtual void Light()
        {
            Console.WriteLine("Ялинка не світиться.");
        }
    }

    // ConcreteComponent - проста ялинка
    class ChristmasTree : Tree
    {
        public override void Show()
        {
            Console.WriteLine("Ялинка");
        }
    }

    // Decorator - базовий клас для всіх прикрас, містить ялинку, яку прикрашаємо
    abstract class TreeDecorator : Tree
    {
        protected Tree tree;

        public TreeDecorator(Tree tree)
        {
            this.tree = tree;
        }

        public override void Show()
        {
            tree.Show();
        }

        public override void Light()
        {
            tree.Light();
        }
    }

    // ConcreteDecoratorA - ялинкові прикраси
    class OrnamentsDecorator : TreeDecorator
    {
        private string ornaments;

        public OrnamentsDecorator(Tree tree, string ornaments)
            : base(tree)
        {
            this.ornaments = ornaments;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine("Прикраси: " + ornaments);
        }
    }

    // ConcreteDecoratorB - гірлянда (додаємо поведінку: ялинка світиться)
    class GarlandDecorator : TreeDecorator
    {
        public GarlandDecorator(Tree tree)
            : base(tree)
        {
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine("Гірлянда: є");
        }

        public override void Light()
        {
            Console.WriteLine("Гірлянда світиться різними кольорами!");
        }
    }
}