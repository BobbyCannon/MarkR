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
			var input = "![blah](/Testing.png)\n![blah](Testing.png)";
			var expected = "<p><img src=\"/Testing.png\" alt=\"blah\" /><br />\n<img src=\"Testing.png\" alt=\"blah\" /></p>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ImageLinkToCustomReference()
		{
			var input = "![blah] (File with spaces.png)";
			var expected = "<p><img src=\"/Files?name=File%20with%20spaces.png\" alt=\"blah\" /></p>\n";

			var parser = new Markdown();
			parser.ImageParsed += (sender, args) => args.Src = "/Files?name=File%20with%20spaces.png";
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

		[TestMethod]
		public void LinkToEmailAddress()
		{
			var input = "Please contact [John Doe](mailto:john.doe@domain.com) to get more information.";
			var expected = "<p>Please contact <a href=\"mailto:john.doe@domain.com\">John Doe</a> to get more information.</p>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void LinkToCustomReference()
		{
			var input = "[Test](blah with stuff)";
			var expected = "<p><a href=\"/Link?id=3\">Test</a></p>\n";

			var parser = new Markdown();
			parser.LinkParsed += (sender, args) => args.Href = "/Link?id=3";
			var actual = parser.Transform(input);
			
			Assert.AreEqual(expected, actual);
		}

		#endregion
	}
}