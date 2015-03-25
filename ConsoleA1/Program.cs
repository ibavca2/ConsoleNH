using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace ConsoleA1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var session = HibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var lada = new Make
                    {
                        Name = "Lada"
                    };
                    var ladaModel = new Model
                    {
                        Name = "2114",
                        Make = lada
                    };
                    var car = new Car
                    {
                        Make = lada,
                        Model = ladaModel,
                        Title = "Моя машина"
                    };
                    session.Save(car);
                    transaction.Commit();
                    Console.WriteLine("Машина создана: " + car.Title);
                }
                Console.ReadLine();
            }
        }
    }
    public class Make
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }

    public class Model
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Make Make { get; set; }
    }
    public class Car
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual Make Make { get; set; }
        public virtual Model Model { get; set; }
    }

    public class MakeMap : ClassMap<Make>
    {
        public MakeMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
    public class ModelMap : ClassMap<Model>
    {
        public ModelMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            References(x => x.Make).Cascade.All();
        }
    }
    public class CarMap : ClassMap<Car>
    {
        public CarMap()
        {
            Id(x => x.Id);
            Map(x => x.Title);
            References(x => x.Make).Cascade.All();
            References(x => x.Model).Cascade.All();
        }
    }

}
