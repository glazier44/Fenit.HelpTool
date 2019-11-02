using System;
using System.Collections.Generic;
using Fenit.Toolbox.Yaml.Test.SampleService;

namespace Fenit.Toolbox.Yaml.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start..");

            var serializationDeserializerSample = new SerializationDeserializerSample();
            serializationDeserializerSample.Test();











            Console.ReadKey();
        }


    }
}
