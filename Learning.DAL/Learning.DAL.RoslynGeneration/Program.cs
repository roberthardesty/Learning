using System;
using System.Collections.Generic;
using System.Text;

namespace Learning.DAL.RoslynGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            new BaseControllerGenerator().GenerateAsync();
        }
    }
}
