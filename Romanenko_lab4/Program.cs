using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Romanenko_lab4
{

    class Towar
    {
        public string name { get; set; }
        public decimal Price { get; set; }
        public string opis { get; set; }
        public string category { get; set; }
    }

    class User
    {
        public string Login { get; set; }
        public string Pass { get; set; }
        public List<Zamovl> history { get; set; }

        public User(string login, string pass)
        {
            Login = login;
            Pass = pass;
            history = new List<Zamovl>();
        }
    }

    class Zamovl
    {
        public Dictionary<Towar, int> towars { get; set; }
        public decimal zagalvart { get; set; }
        public string status { get; set; }

        public Zamovl()
        {
            towars = new Dictionary<Towar, int>();
            zagalvart = 0;
            status = "new";
        }
    }

    
    interface ISearchable
    {
        List<Towar> Search(string kriter, object value);
    }

    
    class Store : ISearchable
    {
        public List<Towar> Towars { get; set; }
        public List<User> Users { get; set; }
        public List<Zamovl> Zamovl { get; set; }

        public Store()
        {
            Towars = new List<Towar>();
            Users = new List<User>();
           Zamovl = new List<Zamovl>();
        }

        public void dodtowar(Towar towar)
        {
            Towars.Add(towar);
        }

        public User createuser(string login, string pass)
        {
            var user = new User(login, pass);
            Users.Add(user);
            return user;
        }

        public Zamovl createzamovl(User user)
        {
            var zamovl = new Zamovl();
            Zamovl.Add(zamovl);
            user.history.Add(zamovl);
            return zamovl;
        }

        public void dodtowardozamovl(Zamovl zamovl, Towar towar, int number)
        {
            if (!zamovl.towars.ContainsKey(towar))
            {
                zamovl.towars[towar] = 0;
            }
            zamovl.towars[towar] += number;
            zamovl.zagalvart += towar.Price * number;
        }

        public void zminstatus(Zamovl zamovl, string status)
        {
            zamovl.status = status;
        }

        public List<Towar> Search(string kriter, object value)
        {
            List<Towar> result = new List<Towar>();
            switch (kriter)
            {
                case "price":
                    decimal price = (decimal)value;
                    result = Towars.FindAll(towar => towar.Price == price);
                    break;
                case "category":
                    string category = (string)value;
                    result = Towars.FindAll(towar => towar.category == category);
                    break;
                    
            }
            return result;
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            var store = new Store();


            var tow1 = new Towar { name = "Товар 1", Price = 100, opis = "Опис товару 1", category = "Категорія 1" };
            var tow2 = new Towar { name = "Товар 2", Price = 150, opis = "Опис товару 2", category = "Категорія 2" };
            store.dodtowar(tow1);
            store.dodtowar(tow2);


            var user = store.createuser("користувач1", "пароль1");


            var zamovl = store.createzamovl(user);


            store.dodtowardozamovl(zamovl, tow1,2);
            store.dodtowardozamovl(zamovl, tow2, 1);


            store.zminstatus(zamovl, "Оплачено");


            var result = store.Search("ціна", 100);
            Console.WriteLine("Результати пошуку за ціною 100:");
            foreach (var towar in result)
            {
                Console.WriteLine($"{towar.name}, Ціна: {towar.Price}");
            }
        }
    }
}
