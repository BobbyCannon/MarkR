using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarkR.UnitTests
{
	[TestClass]
	public class HorizantalRules
	{
		[TestMethod]
		public void ValidCharactersWithSpaces()
		{
			var input = "- - -\r\n* * *\r\n_ _ _";
			var expected = "<hr />\n\n<hr />\n\n<hr />\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ValidCharacters()
		{
			var input = "---\n***\n___";
			var expected = "<hr />\n\n<hr />\n\n<hr />\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}
	}
}
