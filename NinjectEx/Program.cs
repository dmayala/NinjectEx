using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace NinjectEx
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IKernel kernel = new StandardKernel())
            {
                kernel.Bind<IWeapon>().To<Sword>();
                Samurai warrior = kernel.Get<Samurai>();
                warrior.Attack("the evildoers");
            }
        }
    }

    class Samurai
    {
        readonly IWeapon weapon;
        public Samurai(IWeapon weapon)
        {
            this.weapon = weapon;
        }

        public void Attack(string target)
        {
            weapon.Hit(target);
        }
    }

    class Sword : IWeapon {

        public void Hit(string target)
        {
            Console.WriteLine("Sliced {0} in two", target);
        }
    }

    class Shuriken : IWeapon
    {
        public void Hit(string target)
        {
            Console.WriteLine("Pierced {0}'s armor");
        }
    }

    interface IWeapon
    {
        void Hit(string target);
    }
}
