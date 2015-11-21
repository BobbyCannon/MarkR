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
		public void CodeBlock()
		{
			var input = "```\n[Header1](#Header2)\n# Header1{#Header2}\n```";
			var expected = "<p><pre><code>[Header1](#Header2)\n# Header1{#Header2}</code></pre></p>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void CodeBlockWithLanguageSpecifier()
		{
			var input = "```C#\n[Header1](#Header2)\n# Header1{#Header2}\n```";
			var expected = "<p><pre><code class=\"language-C#\">[Header1](#Header2)\n# Header1{#Header2}</code></pre></p>\n";

			var parser = new Markdown();
			var actual = parser.Transform(input);

			Assert.AreEqual(expected, actual);
		}

		#endregion
	}
}