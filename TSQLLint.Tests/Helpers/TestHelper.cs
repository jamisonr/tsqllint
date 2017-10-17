using System.IO;

namespace TSQLLint.Tests.Helpers
{
    public class TestHelper
    {
        private readonly string WorkingDirectory;

        public TestHelper(string workingDirectory)
        {
            WorkingDirectory = workingDirectory;
        }

        public string GetUnitTestFile(string testFilePath)
        {
            var path = Path.GetFullPath(Path.Combine(WorkingDirectory, $@"..\..\UnitTests\{testFilePath}"));
            return File.ReadAllText(path);
        }
    }
}
