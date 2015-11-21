#region References

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace MarkR.UnitTests
{
	[TestClass]
	public class HeaderTests
	{
		#region Methods

		[TestMethod]
		public void HeaderAnchor()
		{
			var input = "# Header{#Header}";
			var expected = "<p><h1 id=\"Header\">Header</h1></p>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void HeaderAnchor2()
		{
			var input = "# Header {#Header}";
			var expected = "<p><h1 id=\"Header\">Header </h1></p>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void HeaderSix()
		{
			var input = "###### Header";
			var expected = "<p><h6>Header</h6></p>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void HeaderSixWithAnchor()
		{
			var input = "###### Header{#Header}";
			var expected = "<p><h6 id=\"Header\">Header</h6></p>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		#endregion
	}
}