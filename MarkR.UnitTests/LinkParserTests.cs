#region References

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace MarkR.UnitTests
{
	[TestClass]
	public class LinkParserTests
	{
		#region Methods

		[TestMethod]
		public void ParseLinkOnly()
		{
			var data = "[Test](http://test.com)";
			var parser = new LinkParser();
			parser.Load(data);

			Assert.AreEqual("Test", parser.Content);
			Assert.AreEqual("http://test.com", parser.Link);
			Assert.AreEqual("", parser.Title);
			Assert.AreEqual("<a href=\"http://test.com\">Test</a>", parser.ToLink());
		}
		
		[TestMethod]
		public void ParseLinkSpaceInContent()
		{
			var data = "[My Site](http://test.com)";
			var parser = new LinkParser();
			parser.Load(data);

			Assert.AreEqual("My Site", parser.Content);
			Assert.AreEqual("http://test.com", parser.Link);
			Assert.AreEqual("", parser.Title);
			Assert.AreEqual("<a href=\"http://test.com\">My Site</a>", parser.ToLink());
		}

		[TestMethod]
		public void ParseLinkSpaceWithTitle()
		{
			var data = "[MySite](http://test.com \"MySite\")";
			var parser = new LinkParser();
			parser.Load(data);

			Assert.AreEqual("MySite", parser.Content);
			Assert.AreEqual("http://test.com", parser.Link);
			Assert.AreEqual("MySite", parser.Title);
			Assert.AreEqual("<a href=\"http://test.com\" title=\"MySite\">MySite</a>", parser.ToLink());
		}
		
		[TestMethod]
		public void ParseLinkSpaceWithTitleWithSpace()
		{
			var data = "[My Site](http://test.com \"My Site\")";
			var parser = new LinkParser();
			parser.Load(data);

			Assert.AreEqual("My Site", parser.Content);
			Assert.AreEqual("http://test.com", parser.Link);
			Assert.AreEqual("My Site", parser.Title);
			Assert.AreEqual("<a href=\"http://test.com\" title=\"My Site\">My Site</a>", parser.ToLink());
		}

		#endregion
	}
}