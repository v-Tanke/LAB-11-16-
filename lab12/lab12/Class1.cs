using System;
using System.Collections.Generic;//подключение пространства имен обобщенных или типизированных классов коллекций
using System.Text;

namespace lab12
{
    interface IClone//Создаем интерфейс
    {
        void Clone();
    }
    public abstract class Vehicle//Создание обстрактного класса
    {
        public abstract void Clone();
        protected string name;
        //Создаем свойства класса
        public string Name { 
            get { return name; } 
            set { name = value; } }
        protected string owner;
        public string Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }
        protected int price;
        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
        abstract public void IsTrain();
        public abstract void IsCar();
    }
    public class Train : Vehicle, IClone //Класс Train наследуется от класса Vehicle и интерфейса IClone
    {
        public Train(string name, string owner, int price)
        {
            this.owner = owner;
            this.price = price;
            this.name = name;
        }
        public override void IsCar()//Переопределение метода абстрактного класса
        {
            Console.WriteLine("Не является машиной");
        }
        public override void IsTrain()//Переопределение метода абстрактного класса
        {
            Console.WriteLine("Это поезд");
        }
        public override void Clone()//Переопределение метода абстрактного класса
        {
            Console.WriteLine("Метод абстрактного класса");
        }
        void IClone.Clone()//Метод интерфейса
        {
            Console.WriteLine("Это метод интерфейса");
        }
        public override string ToString() // переопределение с выводом доп информации
        {
            Console.WriteLine($"Имя: {Name}, владелец: {Owner}, стоимость: {Price}$");
            return " type " + base.ToString();
        }
    }
    public class Car : Vehicle //Класс Car наследуется от Vehicle
    {
        protected int expenditure;
        public int Expenditure { get { return expenditure; } set { expenditure = value; } }
        protected int speed;
        public int Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }
        public Car(string name, string owner, int price, int speed, int expenditure)
        {
            this.name = name;
            this.owner = owner;
            this.price = price;
            this.speed = speed;
            this.expenditure = expenditure;
        }
        public override void IsCar()
        {
            Console.WriteLine("Это машина");
        }
        public override void IsTrain()
        {
            Console.WriteLine("Не является поездом");
        }
        public override void Clone()
        {
            Console.WriteLine("Метод абстрактного класса");
        }
        public override string ToString() // переопределение с выводом доп информации
        {
            Console.WriteLine($"Имя: {Name}, владелец: {Owner}, стоимость: {Price}$, скорость: {Speed} км/ч, расход топлива на 100 км: {Expenditure} л");
            return " type " + base.GetType();
        }
    }
    public class Express : Train//Класс Express наследуется от Train
    {
        protected int speed;
        public int Speed { get { return speed; } set { speed = value; } }
        public Express(string name, string owner, int price, int speed) : base(name, owner, price)
        {
            this.speed = speed;
        }
        public override string ToString() // переопределение с выводом доп инфы
        {
            Console.WriteLine($"Имя: {Name}, владелец: {Owner}, стоимость: {Price}$, скорость: {Speed} км/ч");
            return " type " + base.GetType();
        }
    }
    sealed public class Engine : Car//Класс Engine наследуется от Car
    {
        private int volume;
        public int Volume
        {
            get
            {
                return volume;
            }
            set
            {
                volume = value;
            }
        }
        public Engine(string name, string owner, int price, int volume, int speed, int expenditure) : base(name, owner, price, speed, expenditure)
        {
            this.volume = volume;
        }
        public override string ToString() // переопределение с выводом доп инфы
        {
            Console.WriteLine($"Имя: {Name}, владелец: {Owner}, стоимость: {Price}$, объём: {volume} л, расход топлива на 100 км: {Expenditure} л");
            return " type " + base.GetType();
        }
    }
    class Van : Train //Класс Van наследуется от Train
    {
        protected int capacity;
        public int Capacity
        {
            get
            {
                return capacity;
            }
            set
            {
                capacity = value;
            }
        }
        public Van(string name, string owner, int price, int capacity) : base(name, owner, price)
        {
            this.capacity = capacity;
        }
        public override string ToString() // переопределение с выводом доп инфы
        {
            Console.WriteLine($"Имя: {Name}, владелец: {Owner}, стоимость: {Price}$, вместимость: {Capacity} человек");
            return " type " + base.GetType();
        }
    }
}
