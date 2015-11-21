using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarkR.UnitTests
{
	[TestClass]
	public class ListTests
	{
		[TestMethod]
		public void UnorderedList()
		{
			var input = "* Item1\n* Item2\n* Item3";
			var expected = "<p><ul>\n<li>Item1</li>\n<li>Item2</li>\n<li>Item3</li>\n</ul></p>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void OrderedList()
		{
			var input = "1. Item1\n1. Item2\n1. Item3";
			var expected = "<p><ol>\n<li>Item1</li>\n<li>Item2</li>\n<li>Item3</li>\n</ol></p>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}
	}
}
