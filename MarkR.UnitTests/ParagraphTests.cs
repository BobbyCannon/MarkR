﻿#region References

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace MarkR.UnitTests
{
	[TestClass]
	public class ParagraphTests
	{
		#region Methods

		[TestMethod]
		public void Paragraphs()
		{
			var input = "Foo\nbar\n\nHello\n\nWorld";
			var expected = "<p>Foo\nbar</p>\n<p>Hello</p>\n<p>World</p>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ParagraphsWithHtmlBreakInMarkdown()
		{
			var input = "Foo\nbar\n\nHello\n\n<br />\nWorld";
			var expected = "<p>Foo\nbar</p>\n<p>Hello</p>\n<p><br />\nWorld</p>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		#endregion
	}
}