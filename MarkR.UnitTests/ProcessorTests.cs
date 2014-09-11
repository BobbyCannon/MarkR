#region References

using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace MarkR.UnitTests
{
	[TestClass]
	public class ProcessorTests
	{
		#region Methods

		[TestMethod]
		public void ConvertHeaderFive()
		{
			var expected = "<h5>Test</h5>";
			var actual = Processor.Convert("#####Test");
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderFiveWithSpace()
		{
			var expected = "<h5>Test</h5>";
			var actual = Processor.Convert("##### Test");
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderFour()
		{
			var expected = "<h4>Test</h4>";
			var actual = Processor.Convert("####Test");
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderFourWithSpace()
		{
			var expected = "<h4>Test</h4>";
			var actual = Processor.Convert("#### Test");
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderOne()
		{
			var expected = "<h1>Test</h1>";
			var actual = Processor.Convert("#Test");
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderOneWithSpace()
		{
			var expected = "<h1>Test</h1>";
			var actual = Processor.Convert("# Test");
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderSix()
		{
			var expected = "<h6>Test</h6>";
			var actual = Processor.Convert("######Test");
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderSixWithSpace()
		{
			var expected = "<h6>Test</h6>";
			var actual = Processor.Convert("###### Test");
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderThree()
		{
			var expected = "<h3>Test</h3>";
			var actual = Processor.Convert("###Test");
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderThreeWithSpace()
		{
			var expected = "<h3>Test</h3>";
			var actual = Processor.Convert("### Test");
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderTwo()
		{
			var expected = "<h2>Test</h2>";
			var actual = Processor.Convert("##Test");
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderTwoWithSpace()
		{
			var expected = "<h2>Test</h2>";
			var actual = Processor.Convert("## Test");
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ItemListWithSingleItem()
		{
			var expected = string.Format("<ul>{0}<li>Test</li>{0}</ul>", Environment.NewLine);
			var actual = Processor.Convert("* Test");
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ItemListWithThreeItems()
		{
			var markdown = string.Format("* One{0}* Two{0}* Three", Environment.NewLine);
			var expected = string.Format("<ul>{0}<li>One</li>{0}<li>Two</li>{0}<li>Three</li>{0}</ul>", Environment.NewLine);
			var actual = Processor.Convert(markdown);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ItemListWithTwoItems()
		{
			var markdown = string.Format("* One{0}* Two", Environment.NewLine);
			var expected = string.Format("<ul>{0}<li>One</li>{0}<li>Two</li>{0}</ul>", Environment.NewLine);
			var actual = Processor.Convert(markdown);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ParseAllThings()
		{
			var markdown = File.ReadAllText("TestFiles/TestFile1.md");
			var expected = File.ReadAllText("TestFiles/TestFile1.html");
			var actual = Processor.Convert(markdown);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ParseCodeBlock()
		{
			var markdown = File.ReadAllText("TestFiles/TestFile2.md");
			var expected = File.ReadAllText("TestFiles/TestFile2.html");
			var actual = Processor.Convert(markdown);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ParseCodeBlockWithClassName()
		{
			var markdown = File.ReadAllText("TestFiles/TestFile3.md");
			var expected = File.ReadAllText("TestFiles/TestFile3.html");
			var actual = Processor.Convert(markdown);
			Assert.AreEqual(expected, actual);
		}

		#endregion
	}
}