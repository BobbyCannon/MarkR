#region References

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace MarkR.UnitTests
{
	[TestClass]
	public class LinkTests
	{
		#region Methods

		[TestMethod]
		public void ImageLink()
		{
			var input = "![blah](/Testing.png)";
			var expected = "<p><img src=\"/Testing.png\" alt=\"blah\" /></p>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ImageLinkWithTitle()
		{
			var input = "![blah](/Testing.png \"blah\")";
			var expected = "<p><img src=\"/Testing.png\" alt=\"blah\" title=\"blah\" /></p>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void LinkToAnchor()
		{
			var input = "[Header1](#Header2)\r\n# Header1{#Header2}";
			var expected = "<p><a href=\"#Header2\">Header1</a><br />\n<h1 id=\"Header2\">Header1</h1></p>\n";

            var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void LinkToAnchorWithTitle()
		{
			var input = "[Header1](#Header2 \"Header3\")\r\n# Header1 {#Header2}";
			var expected = "<p><a href=\"#Header2\" title=\"Header3\">Header1</a><br />\n<h1 id=\"Header2\">Header1 </h1></p>\n";

            var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		#endregion
	}
}