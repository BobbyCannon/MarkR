#region References

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace MarkR.UnitTests
{
	[TestClass]
	public class CodeBlockTests
	{
		#region Methods

		[TestMethod]
		public void CodeBlockFenceWithLanguageSpecifier()
		{
			var input = "```C#\n[Header1](#Header2)\n# Header1{#Header2}\n```";
			var expected = "<pre><code class=\"language-C#\">[Header1](#Header2)\n# Header1{#Header2}</code></pre>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void CodeBlockFense()
		{
			var input = "```\n[Header1](#Header2)\n# Header1{#Header2}\n```";
			var expected = "<pre><code>[Header1](#Header2)\n# Header1{#Header2}</code></pre>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void CodeBlockSpan()
		{
			var input = "The quick brown `fox` jumped over the lazy dog.";
			var expected = "<p>The quick brown <code>fox</code> jumped over the lazy dog.</p>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void CodeBlockSpanWithEscapedContent()
		{
			var input = @"The quick brown `` \`fox\` `` jumped over the lazy dog.";
			var expected = "<p>The quick brown <code>\\`fox\\`</code> jumped over the lazy dog.</p>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void EscapedCodeBlockSpan()
		{
			var input = @"The quick brown \`fox\` jumped over the lazy dog.";
			var expected = "<p>The quick brown `fox` jumped over the lazy dog.</p>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		#endregion
	}
}