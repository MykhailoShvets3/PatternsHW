// Завдання 6. Два шаблони: Фабричний метод та Адаптер
// Factory Method: фабрика створює транспорт (авто, велосипед, літак) за номером типу
// Adapter: старий термометр (Фаренгейт) підключаємо до нового інтерфейсу (Цельсій)
// Обидва шаблони в одному проєкті, Main запускає їх по черзі.
using System;
using System.Text;

namespace FactoryMethodAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine(" Factory Method ");
            FactoryMethodExample.FactoryMethodDemo.Run();

            Console.WriteLine();
            Console.WriteLine(" Adapter ");
            AdapterExample.AdapterDemo.Run();

            Console.ReadKey();
        }
    }
}

// Шаблон Factory Method
namespace FactoryMethodExample
{
    // Product - абстрактний продукт
    public abstract class Transport
    {
        public abstract void Move();
    }

    // Конкретні продукти
    public class Car : Transport
    {
        public override void Move()
        {
            Console.WriteLine("Автомобіль їде по дорозі");
        }
    }

    public class Bike : Transport
    {
        public override void Move()
        {
            Console.WriteLine("Велосипед їде по доріжці");
        }
    }

    public class Plane : Transport
    {
        public override void Move()
        {
            Console.WriteLine("Літак летить у небі");
        }
    }

    public abstract class Creator
    {
        public abstract Transport FactoryMethod(int type);
    }

    // ConcreteCreator - сам вирішує, який об'єкт створити
    public class ConcreteCreator : Creator
    {
        public override Transport FactoryMethod(int type)
        {
            switch (type)
            {
                case 1: return new Car();
                case 2: return new Bike();
                case 3: return new Plane();
                default: throw new ArgumentException("Invalid type.", "type");
            }
        }
    }

    public static class FactoryMethodDemo
    {
        public static void Run()
        {
            Creator creator = new ConcreteCreator();

            for (int i = 1; i <= 3; i++)
            {
                Transport transport = creator.FactoryMethod(i);
                Console.Write("Тип {0}: створено {1}. ", i, transport.GetType().Name);
                transport.Move();
            }

            // перевірка помилки: такого типу немає
            try
            {
                creator.FactoryMethod(4);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Тип 4: такого транспорту немає");
            }
        }
    }
}

//  Шаблон Adapter 
namespace AdapterExample
{
    // Adaptee - старий термометр, який показує градуси Фаренгейта
    class OldThermometer
    {
        public double GetFahrenheit()
        {
            return 68;
        }
    }

    interface INewThermometer
    {
        double GetCelsius();
    }

    // Новий термометр, який вже працює з Цельсієм
    class NewThermometer : INewThermometer
    {
        public double GetCelsius()
        {
            return 25;
        }
    }

    // Adapter - зовні виглядає як новий термометр, всередині користується старим
    class ThermometerAdapter : INewThermometer
    {
        private readonly OldThermometer _adaptee;

        public ThermometerAdapter(OldThermometer adaptee)
        {
            _adaptee = adaptee;
        }

        // тут "перекладаємо" з Фаренгейтів у Цельсії
        public double GetCelsius()
        {
            return (_adaptee.GetFahrenheit() - 32) * 5 / 9;
        }
    }

    class WeatherStation
    {
        public static void ShowTemperature(INewThermometer thermometer)
        {
            Console.WriteLine("Температура: {0} °C", thermometer.GetCelsius());
        }
    }

    public static class AdapterDemo
    {
        public static void Run()
        {
            
            WeatherStation.ShowTemperature(new NewThermometer());

           
            WeatherStation.ShowTemperature(new ThermometerAdapter(new OldThermometer()));
        }
    }
}