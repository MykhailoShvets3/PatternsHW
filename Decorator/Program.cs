// Завдання 4. Шаблон "Декоратор" (Decorator) - ялинка.
using System;
using System.Text;

namespace Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Random rnd = new Random();
            string[] ornaments = { "кульки", "зірка", "цукерки", "сніжинки", "дощик" };

            // робимо 5 ялинок, кожна прикрашається випадково
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine("Ялинка " + i + ":");
                Tree tree = new ChristmasTree();

                // випадково: або є прикраси, або нема 
                if (rnd.Next(2) == 1)
                {
                    string ornament = ornaments[rnd.Next(ornaments.Length)];
                    tree = new OrnamentsDecorator(tree, ornament);
                }

                // випадково: або є гірлянда, або нема
                if (rnd.Next(2) == 1)
                {
                    tree = new GarlandDecorator(tree);
                }

                tree.Show();
                tree.Light();
                Console.WriteLine();
            }

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

    // ConcreteDecoratorB - гірлянда 
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