using MakingDotNETApplicationsFaster.Infrastructure;

namespace MakingDotNETApplicationsFaster
{
    internal sealed class ReplaceOptimizationRunner : IRunner
    {
        public void Run()
        {
            const string text = "<span>Clear  Replace can <span>Dog  be optimized. <span>Cat <span>Draw ";
            new PerformanceTests
            {
                {_ => { ReplaceWithoutContains(text); }, "ReplaceWithoutContains"},
                {_ => { ReplaceWithContains(text); }, "ReplaceWithContains"}
            }.Run(10000000);
        }

        private static string ReplaceWithoutContains(string text)
        {
            text = text.Replace("<span>Cat ", "<span>Cats ");
            text = text.Replace("<span>Clear ", "<span>Clears ");
            text = text.Replace("<span>Dog ", "<span>Dogs ");
            text = text.Replace("<span>Draw ", "<span>Draws ");
            return text;
        }

        private static string ReplaceWithContains(string text)
        {
            if (text.Contains("<span>C"))
            {
                text = text.Replace("<span>Cat ", "<span>Cats ");
                text = text.Replace("<span>Clear ", "<span>Clears ");
            }
            if (text.Contains("<span>D"))
            {
                text = text.Replace("<span>Dog ", "<span>Dogs ");
                text = text.Replace("<span>Draw ", "<span>Draws ");
            }
            return text;
        }
    }
}