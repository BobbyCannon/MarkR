#region References

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace MarkR.UnitTests
{
	[TestClass]
	public class ListTests
	{
		#region Methods

		[TestMethod]
		public void OrderedList()
		{
			var input = "1. Item1\n1. Item2\n1. Item3";
			var expected = "<ol>\n<li>Item1</li>\n<li>Item2</li>\n<li>Item3</li>\n</ol>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void UnorderedList()
		{
			var input = "* Item1\n* Item2\n* Item3";
			var expected = "<ul>\n<li>Item1</li>\n<li>Item2</li>\n<li>Item3</li>\n</ul>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		#endregion
	}
}