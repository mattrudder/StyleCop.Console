using System.Collections.Generic;
using System.Linq;

namespace StyleCop.Console
{
    class RunnerArguments : CommandLineArguments
    {
        public RunnerArguments(string[] args)
            : base(args)
        {
        }

        public string ProjectPath
        {
            get { return (this["project-path"].Concat(this["p"])).LastOrDefault(); }
        }

        public bool NotRecursive
        {
			get { return (this["not-recursively"].Concat(this["n"])).Any(); }
        }

        public bool Help
        {
			get { return (this["help"].Concat(this["?"])).Any(); }
        }

        public string SettingsLocation
        {
            get { return (this["settings-location"].Concat(this["s"])).LastOrDefault(); }
        }

		public IEnumerable<string> IgnoredPaths
		{
			get { return this["ignored-paths"].Concat(this["i"]); }
		}
    }
}
