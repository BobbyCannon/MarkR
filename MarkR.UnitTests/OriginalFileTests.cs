#region References

using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace MarkR.UnitTests
{
	[TestClass]
	public class OriginalFileTests
	{
		#region Methods

		[TestMethod]
		public void AmpsAndAngleEncoding()
		{
			RunTestFile("amps-and-angle-encoding");
		}

		[TestMethod]
		public void AutoLinks()
		{
			RunTestFile("auto-links");
		}

		[TestMethod]
		public void BackslashEscapes()
		{
			RunTestFile("backslash-escapes");
		}

		[TestMethod]
		public void BackslashEscapes2()
		{
			RunTestFile("backslash-escapes2");
		}

		[TestMethod]
		public void BlockquotesWithCodeBlocks()
		{
			RunTestFile("blockquotes-with-code-blocks");
		}

		[TestMethod]
		public void CodeBlocks()
		{
			RunTestFile("code-blocks");
		}

		[TestMethod]
		public void CodeBlocksGithub()
		{
			RunTestFile("code-blocks-github");
		}

		[TestMethod]
		public void CodeSpans()
		{
			RunTestFile("code-spans");
		}

		[TestMethod]
		public void Emphasis()
		{
			RunTestFile("emphasis");
		}

		[TestMethod]
		public void FailureToEscapeLessThan()
		{
			RunTestFile("failure-to-escape-less-than");
		}

		[TestMethod]
		public void HardWrappedParagraphsWithListLikeLines()
		{
			RunTestFile("hard-wrapped-paragraphs-with-list-like-lines");
		}

		[TestMethod]
		public void Headers()
		{
			RunTestFile("headers");
		}

		[TestMethod]
		public void HorizontalRules()
		{
			RunTestFile("horizontal-rules");
		}

		[TestMethod]
		public void HorizontalRules2()
		{
			RunTestFile("horizontal-rules2");
		}

		[TestMethod]
		public void HtmlCommentEdgeCase()
		{
			RunTestFile("html-comment-edge-case");
		}

		[TestMethod]
		public void Images()
		{
			RunTestFile("images");
		}

		[TestMethod]
		public void InlineHtmlAdvanced()
		{
			RunTestFile("inline-html-advanced");
		}

		[TestMethod]
		public void InlineHtmlComments()
		{
			RunTestFile("inline-html-comments");
		}

		[TestMethod]
		public void InlineHtmlComments2()
		{
			RunTestFile("inline-html-comments2");
		}

		[TestMethod]
		public void InlineHtmlSimple()
		{
			RunTestFile("inline-html-simple");
		}

		[TestMethod]
		public void InlineHtmlSimple2()
		{
			RunTestFile("inline-html-simple2");
		}

		[TestMethod]
		public void InlineHtmlSpan()
		{
			RunTestFile("inline-html-span");
		}

		[TestMethod]
		public void InsDel()
		{
			RunTestFile("ins-del");
		}

		[TestMethod]
		public void LineEndingsCr()
		{
			RunTestFile("line-endings-cr");
		}

		[TestMethod]
		public void LineEndingsCrlf()
		{
			RunTestFile("line-endings-crlf");
		}

		[TestMethod]
		public void LineEndingsLf()
		{
			RunTestFile("line-endings-lf");
		}

		[TestMethod]
		public void LinksInlineStyle()
		{
			RunTestFile("links-inline-style");
		}

		[TestMethod]
		public void LinksInlineStyle2()
		{
			RunTestFile("links-inline-style2");
		}

		[TestMethod]
		public void LinksReferenceStyle()
		{
			RunTestFile("links-reference-style");
		}

		[TestMethod]
		public void LinksShortcutReferences()
		{
			RunTestFile("links-shortcut-references");
		}

		[TestMethod]
		public void LiteralQuotesInTitles()
		{
			RunTestFile("literal-quotes-in-titles");
		}

		[TestMethod]
		public void MarkdownDocumentationBasics()
		{
			RunTestFile("markdown-documentation-basics");
		}

		[TestMethod]
		public void MarkdownDocumentationSyntax()
		{
			RunTestFile("markdown-documentation-syntax");
		}

		[TestMethod]
		public void MarkdownReadme()
		{
			RunTestFile("markdown-readme");
		}

		[TestMethod]
		public void Md5Hashes()
		{
			RunTestFile("md5-hashes");
		}

		[TestMethod]
		public void NestedBlockquotes()
		{
			RunTestFile("nested-blockquotes");
		}

		[TestMethod]
		public void NestedDivs()
		{
			RunTestFile("nested-divs");
		}

		[TestMethod]
		public void NestedEmphasis()
		{
			RunTestFile("nested-emphasis");
		}

		[TestMethod]
		public void Nesting()
		{
			RunTestFile("nesting");
		}

		[TestMethod]
		public void OrderedAndUnorderedLists()
		{
			RunTestFile("ordered-and-unordered-lists");
		}

		[TestMethod]
		public void ParensInUrl()
		{
			RunTestFile("parens-in-url");
		}

		[TestMethod]
		public void PhpSpecificBugs()
		{
			RunTestFile("php-specific-bugs");
		}

		[TestMethod]
		public void RunTestFile(string filename)
		{
			var parser = new Markdown();
			var relactivePath = "../../TestFiles/";
			var inputFile = relactivePath + filename + ".text";
			var resultFile = relactivePath + filename + ".html";
			var expected = File.ReadAllText(resultFile);
			var markdown = File.ReadAllText(inputFile);
			var actual = parser.Transform(markdown);

			// This is here so we can easily compare the difference in GIT.
			//File.WriteAllText(resultFile, actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Sandbox()
		{
			RunTestFile("!sandbox");
		}

		[TestMethod]
		public void StrongAndEmTogether()
		{
			RunTestFile("strong-and-em-together");
		}

		[TestMethod]
		public void Tabs()
		{
			RunTestFile("tabs");
		}

		[TestMethod]
		public void Tidyness()
		{
			RunTestFile("tidyness");
		}

		[TestMethod]
		public void TightBlocks()
		{
			RunTestFile("tight-blocks");
		}

		[TestMethod]
		public void UnorderedListAndHorizontalRules()
		{
			RunTestFile("unordered-list-and-horizontal-rules");
		}

		[TestMethod]
		public void UnorderedListFollowedByOrderedList()
		{
			RunTestFile("unordered-list-followed-by-ordered-list");
		}

		[TestMethod]
		public void UnpredictableSublists()
		{
			RunTestFile("unpredictable-sublists");
		}

		[TestMethod]
		public void Version()
		{
			var parser = new Markdown();
			Assert.IsTrue(parser.Version.StartsWith("1.15"));
		}

		#endregion
	}
}