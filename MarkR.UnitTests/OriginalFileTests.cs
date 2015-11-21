#region References

using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace MarkR.UnitTests
{
	[TestClass]
	public class OriginalFileTests
	{
		#region Methods

		[TestMethod]
		public void ParseTestFiles()
		{
			var parser = new Markdown();
			var files = Directory.GetFiles("../../TestFiles", "*.text");

			foreach (var file in files)
			{
				var resultFile = file.Replace(".text", ".html");
				var expected = File.ReadAllText(resultFile);
				var markdown = File.ReadAllText(file);
				var actual = parser.Transform(markdown);

				// This is here so we can easily compare the difference in GIT.
				//File.WriteAllText(resultFile, actual);
				Assert.AreEqual(expected, actual);
			}
		}

		[TestMethod]
		public void Version()
		{
			var parser = new Markdown();
			Assert.IsTrue(parser.Version.StartsWith("1.14"));
		}

		#endregion
	}
}