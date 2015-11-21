using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarkR.UnitTests
{
	[TestClass]
	public class ParagraphTests
	{
		[TestMethod]
		public void Paragraphs()
		{
			var input = "Foo\nbar\n\nHello\n\nWorld";
			var expected = "<p>Foo<br />\nbar</p>\n\n<p>Hello</p>\n\n<p>World</p>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ParagraphsWithHtmlBreakInMarkdown()
		{
			var input = "Foo\nbar\n\nHello\n\n<br />\nWorld";
			var expected = "<p>Foo<br />\nbar</p>\n\n<p>Hello</p>\n\n<p><br />\nWorld</p>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}
	}
}
