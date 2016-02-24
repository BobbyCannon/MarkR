#region References

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace MarkR.UnitTests
{
	[TestClass]
	public class HtmlTests
	{
		#region Methods

		[TestMethod]
		public void ValidCharactersWithSpaces()
		{
			var input = "# Test\r\n<table>\r\n    <tr>\r\n        <td>Item1</td>\r\n    </tr>\r\n</table>\r\n";
			var expected = "<h1>Test</h1>\n<table>\n    <tr>\n        <td>Item1</td>\n    </tr>\n</table>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		#endregion
	}
}