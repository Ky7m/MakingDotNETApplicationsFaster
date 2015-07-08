using System.Text;
using System.Text.RegularExpressions;
using MakingDotNETApplicationsFaster.Infrastructure;

namespace MakingDotNETApplicationsFaster
{
    sealed class ReplaceOptimizationRunner : IRunner
    {
        public void Run()
        {
            const string text = "<span>Clear  Replace can <span>Dog  be optimized. <span>Cat <span>Draw ";
            new PerformanceTests
            {
                {_ => { ReplaceWithoutContains(text); }, "ReplaceWithoutContains"},
                {_ => { ReplaceWithContains(text); }, "ReplaceWithContains"},
                {_ => { ReplaceUsingRegex(text); }, "ReplaceUsingRegex"},
                {_ => { ReplaceUsingCompiledRegex(text); }, "ReplaceUsingCompiledRegex"},
                {_ => { ReplaceUsingStringBuilder(text); }, "ReplaceUsingStringBuilder"}
            }.Run(1000000);
        }

        static string ReplaceWithoutContains(string text)
        {
            text = text.Replace("<span>Cat ", "<span>Cats ");
            text = text.Replace("<span>Clear ", "<span>Clears ");
            text = text.Replace("<span>Dog ", "<span>Dogs ");
            text = text.Replace("<span>Draw ", "<span>Draws ");
            return text;
        }

        static string ReplaceWithContains(string text)
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

        static string ReplaceUsingRegex(string text)
        {
            text = Regex.Replace(text, "<span>Cat ", "<span>Cats ");
            text = Regex.Replace(text, "<span>Clear ", "<span>Clears ");
            text = Regex.Replace(text, "<span>Dog ", "<span>Dogs ");
            text = Regex.Replace(text, "<span>Draw ", "<span>Draws ");
            return text;
        }

        static string ReplaceUsingCompiledRegex(string text)
        {
            text = Regex.Replace(text, "<span>Cat ", "<span>Cats ", RegexOptions.Compiled);
            text = Regex.Replace(text, "<span>Clear ", "<span>Clears ", RegexOptions.Compiled);
            text = Regex.Replace(text, "<span>Dog ", "<span>Dogs ", RegexOptions.Compiled);
            text = Regex.Replace(text, "<span>Draw ", "<span>Draws ", RegexOptions.Compiled);
            return text;
        }

        static string ReplaceUsingStringBuilder(string text)
        {
            var sb = new StringBuilder(text, text.Length);
            sb = sb.Replace("<span>Cat ", "<span>Cats ");
            sb = sb.Replace("<span>Clear ", "<span>Clears ");
            sb = sb.Replace("<span>Dog ", "<span>Dogs ");
            sb = sb.Replace("<span>Draw ", "<span>Draws ");
            return sb.ToString();
        }
    }
}