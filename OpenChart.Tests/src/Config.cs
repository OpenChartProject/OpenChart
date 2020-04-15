using System.IO;
using System.Reflection;

namespace OpenChart.Tests
{
    /// <summary>
    /// A singleton class that contains test configurations.
    /// </summary>
    public class Config
    {
        static Config _singleton;

        /// <summary>
        /// The absolute path to the root of the project.
        /// </summary>
        public readonly string ProjectPath;

        /// <summary>
        /// The absolute path to the test data directory.
        /// `OpenChart.Tests/test_data/`
        /// </summary>
        public readonly string TestDataPath;

        private Config()
        {
            ProjectPath = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location
            );

            // To get the root directory of the project we need to move up four directories.
            //
            //   OpenChart/OpenChart.Tests/bin/Debug/netcoreapp3.1
            // to
            //   OpenChart/
            ProjectPath = Path.GetFullPath(Path.Join(ProjectPath, "..", "..", "..", ".."));
            TestDataPath = Path.Join(ProjectPath, "OpenChart.Tests", "test_data");
        }

        /// <summary>
        /// Gets the singleton config instance.
        /// </summary>
        public static Config Get()
        {
            if (_singleton == null)
            {
                _singleton = new Config();
            }

            return _singleton;
        }
    }
}
