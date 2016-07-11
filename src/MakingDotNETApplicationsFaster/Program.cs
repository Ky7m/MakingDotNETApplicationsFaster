using System.Reflection;
using BenchmarkDotNet.Running;

namespace MakingDotNETApplicationsFaster
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var switcher = new BenchmarkSwitcher(typeof(Program).GetTypeInfo().Assembly).Run(args);
        }
    }
}
