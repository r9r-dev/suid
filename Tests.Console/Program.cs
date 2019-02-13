using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rlcx.suid;

namespace Tests.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Suid");
            System.Console.WriteLine(Suid.NewSuid());
            System.Console.WriteLine();

            System.Console.WriteLine("Tiny Suid");
            System.Console.WriteLine(Suid.NewTinySuid());
            System.Console.WriteLine();

            System.Console.WriteLine("Url Friendly Suid");
            System.Console.WriteLine(Suid.NewUrlFriendlySuid());
            System.Console.WriteLine();

            System.Console.WriteLine("Filename Suid");
            System.Console.WriteLine(Suid.NewFilenameSuid());
            System.Console.WriteLine();

            System.Console.WriteLine("Letters Only Suid");
            System.Console.WriteLine(Suid.NewLettersOnlySuid());
            System.Console.WriteLine();

            System.Console.WriteLine("Guid to Suid");
            System.Console.WriteLine(Guid.NewGuid().ToSuid());
            System.Console.WriteLine();

            System.Console.WriteLine("Guid to Tiny Suid");
            System.Console.WriteLine(Guid.NewGuid().ToTinySuid());
            System.Console.WriteLine();

            System.Console.ReadKey();
        }
    }
}
