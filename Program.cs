//#define TEST

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Framework;

namespace Ogame
{
    class Program
    {
        static void Main(string[] args)
        {
            Empire MyEmpire = new Empire();
            if (!MyEmpire.CloseDriver())
                Console.WriteLine("Ошибка закрытия браузера");
            Console.ReadKey();
                /*Thread CheckForAction = new Thread(iobj.IsAttackPresent);
                CheckForAction.Start();
                while (CheckForAction.IsAlive)
                {
                    Thread.Sleep(1);
                }*/

        }
    }
}
