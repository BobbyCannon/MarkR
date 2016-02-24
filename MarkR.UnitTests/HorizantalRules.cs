#region References

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace MarkR.UnitTests
{
	[TestClass]
	public class HorizantalRules
	{
		#region Methods

		[TestMethod]
		public void ValidCharacters()
		{
			var input = "---\n***\n___";
			var expected = "<hr />\n<hr />\n<hr />\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ValidCharactersWithSpaces()
		{
			var input = "- - -\r\n* * *\r\n_ _ _";
			var expected = "<hr />\n<hr />\n<hr />\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		#endregion
	}
}