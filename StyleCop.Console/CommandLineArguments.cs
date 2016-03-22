using System;
using System.Collections.Generic;
using System.Linq;

namespace StyleCop.Console
{
    public abstract class CommandLineArguments
    {
        readonly IDictionary<string, List<string>> _keyValueMap = new Dictionary<string, List<string>>(StringComparer.InvariantCultureIgnoreCase);

        public int Count
        {
            get { return _keyValueMap.Count; }
        }

        protected CommandLineArguments(params string[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                if (IsKey(arg))
                {
                    if (IsNextArgAValue(args, i))
                    {
                        List<string> values;
                        if (!_keyValueMap.TryGetValue(arg, out values))
                        {
                            values = new List<string>();
                            _keyValueMap.Add(arg.Substring(1), values);
                        }

                        values.Add(args[i + 1]);
                        i++;
                    }
                    else
                    {
                        _keyValueMap.Add(arg.Substring(1), new List<string>());
                    }
                }
                else
                {
                    throw new ArgumentException("The argument list must have the form (-key value?)* ");
                }
            }
        }

        bool IsNextArgAValue(string[] args, int i)
        {
            return i + 1 < args.Length && !IsKey(args[i + 1]);
        }

        protected IEnumerable<string> this [string key]
        {
            get
            {
                return !_keyValueMap.ContainsKey(key) ? Enumerable.Empty<string>() : _keyValueMap[key];
            }
        }

        private bool IsKey(string s)
        {
            return s.StartsWith("-");
        }
    }
}
