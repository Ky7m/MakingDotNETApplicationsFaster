using System.Collections.Generic;
using MakingDotNETApplicationsFaster.Infrastructure;

namespace MakingDotNETApplicationsFaster.Runners.DictionaryPerformance
{
    sealed class DictionaryPerformanceRunner : IRunner
    {
        public void Run()
        {
            const int size = 1000000;

            var dictionary = new Dictionary<int, string>();

            for (var i = 0; i < size; i++)
            {
                dictionary.Add(i, i.ToString());
            }

            new PerformanceTests
            {
                {_ => { UsingContainsKey(dictionary,size); }, "UsingContainsKey"},
                {_ => { UsingTryGetValue(dictionary,size); }, "UsingTryGetValue"}
            }.Run(100);
        }

        static string UsingContainsKey(Dictionary<int, string> dictionary, int size)
        {
            var result = string.Empty;
            for (var i = 0; i < size; i++)
            {
                if (dictionary.ContainsKey(i))
                {
                    result = dictionary[i];
                }
            }
            return result;
        }

        static string UsingTryGetValue(Dictionary<int, string> dictionary, int size)
        {
            var result = string.Empty;
            for (var i = 0; i < size; i++)
            {
                dictionary.TryGetValue(i, out result);
            }
            return result;
        }
    }
}
