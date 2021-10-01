using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Text.RegularExpressions;

namespace HowExpensiveIsRegexMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<BenchmarkClass>();
            Console.ReadLine();
        }

    }
    [MemoryDiagnoser]
    public class BenchmarkClass
    {
        private static readonly Regex regexCompiledWithTimeout =
            new Regex(regexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));
        private static readonly Regex regexCompiled =
            new Regex(regexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex regex =
            new Regex(regexPattern, RegexOptions.IgnoreCase);
        private string sampleStringMatch = "fhjdskfjds@dfshfdjksfndksj.dsadasjk";
        private string sampleStringNoMatch = "kfds8989fdsf8d9s";

        private const string regexPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        [Benchmark]
        public void UseRegexCompiledWithTimeout()
        {
            regexCompiledWithTimeout.Match(sampleStringMatch);
            regexCompiledWithTimeout.Match(sampleStringNoMatch);
        }
        [Benchmark]
        public void UseRegexCompiled()
        {
            regexCompiled.Match(sampleStringMatch);
            regexCompiled.Match(sampleStringNoMatch);
        }
        [Benchmark]
        public void UseRegex()
        {
            regex.Match(sampleStringMatch);
            regex.Match(sampleStringNoMatch);
        }

        [Benchmark]
        public void RegexInstanceCompiledWithTimeout()
        {
            Regex x = new Regex(regexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));
        }

        [Benchmark]
        public void RegexInstanceCompiled()
        {
            Regex x = new Regex(regexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        [Benchmark]
        public void RegexInstance()
        {
            Regex x = new Regex(regexPattern, RegexOptions.IgnoreCase);
        }
    }
}
