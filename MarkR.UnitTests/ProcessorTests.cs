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
		public void TestNewLinesOnly()
		{
			var input = "### test\n\n\n* test\n* again\n\nTesting\n\nAnother Paragraph";
			var expected = "<h3>test</h3>\r\n\r\n\r\n<ul>\r\n<li>test</li>\r\n<li>again</li>\r\n</ul>\r\n\r\n<p>Testing</p>\r\n\r\n<p>Another Paragraph</p>";
			var actual = Processor.Convert(input);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestReturnLinesOnly()
		{
			var input = "### test\r\r\r* test\r* again\r\rTesting\r\rAnother Paragraph";
			var expected = "<h3>test</h3>\r\n\r\n\r\n<ul>\r\n<li>test</li>\r\n<li>again</li>\r\n</ul>\r\n\r\n<p>Testing</p>\r\n\r\n<p>Another Paragraph</p>";
			var actual = Processor.Convert(input);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderFive()
		{
			var expected = "<h5>Test</h5>";
			var actual = Processor.Convert("#####Test");
			Console.WriteLine(actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderWithClass()
		{
			var expected = "<h5 class=\"blue\">Test</h5>";
			var actual = Processor.Convert("#####:blue Test");
			Console.WriteLine(actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderWithMulitpleClasses()
		{
			var expected = "<h5 class=\"blue light\">Test</h5>";
			var actual = Processor.Convert("#####:blue,light Test");
			Console.WriteLine(actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderFiveWithSpace()
		{
			var expected = "<h5>Test</h5>";
			var actual = Processor.Convert("##### Test");
			Console.WriteLine(actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderFour()
		{
			var expected = "<h4>Test</h4>";
			var actual = Processor.Convert("####Test");
			Console.WriteLine(actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderFourWithSpace()
		{
			var expected = "<h4>Test</h4>";
			var actual = Processor.Convert("#### Test");
			Console.WriteLine(actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderOne()
		{
			var expected = "<h1>Test</h1>";
			var actual = Processor.Convert("#Test");
			Console.WriteLine(actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderOneWithSpace()
		{
			var expected = "<h1>Test</h1>";
			var actual = Processor.Convert("# Test");
			Console.WriteLine(actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderSix()
		{
			var expected = "<h6>Test</h6>";
			var actual = Processor.Convert("######Test");
			Console.WriteLine(actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderSixWithSpace()
		{
			var expected = "<h6>Test</h6>";
			var actual = Processor.Convert("###### Test");
			Console.WriteLine(actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderThree()
		{
			var expected = "<h3>Test</h3>";
			var actual = Processor.Convert("###Test");
			Console.WriteLine(actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderThreeWithSpace()
		{
			var expected = "<h3>Test</h3>";
			var actual = Processor.Convert("### Test");
			Console.WriteLine(actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderTwo()
		{
			var expected = "<h2>Test</h2>";
			var actual = Processor.Convert("##Test");
			Console.WriteLine(actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConvertHeaderTwoWithSpace()
		{
			var expected = "<h2>Test</h2>";
			var actual = Processor.Convert("## Test");
			Console.WriteLine(actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ItemListWithSingleItem()
		{
			var expected = string.Format("<ul>{0}<li>Test</li>{0}</ul>", Environment.NewLine);
			var actual = Processor.Convert("* Test");
			Console.WriteLine(actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ItemListWithThreeItems()
		{
			var markdown = string.Format("* One{0}* Two{0}* Three", Environment.NewLine);
			var expected = string.Format("<ul>{0}<li>One</li>{0}<li>Two</li>{0}<li>Three</li>{0}</ul>", Environment.NewLine);
			var actual = Processor.Convert(markdown);
			Console.WriteLine(actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ItemListWithTwoItems()
		{
			var markdown = string.Format("* One{0}* Two", Environment.NewLine);
			var expected = string.Format("<ul>{0}<li>One</li>{0}<li>Two</li>{0}</ul>", Environment.NewLine);
			var actual = Processor.Convert(markdown);
			Console.WriteLine(actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ParseCodePerformance()
		{
			var markdown = File.ReadAllText("TestFiles/Performance1.md");
			var expected = File.ReadAllText("TestFiles/Performance1.html");
			var actual = Processor.Convert(markdown);
			Console.WriteLine(actual);
			//File.WriteAllText("../../TestFiles/Performance1.html", actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ParseTest1()
		{
			var markdown = File.ReadAllText("TestFiles/TestFile1.md");
			var expected = File.ReadAllText("TestFiles/TestFile1.html");
			var actual = Processor.Convert(markdown);
			Console.WriteLine(actual);
			//File.WriteAllText("../../TestFiles/TestFile1.html", actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ParseTest2()
		{
			var markdown = File.ReadAllText("TestFiles/TestFile2.md");
			var expected = File.ReadAllText("TestFiles/TestFile2.html");
			var actual = Processor.Convert(markdown);
			Console.WriteLine(actual);
			//File.WriteAllText("../../TestFiles/TestFile2.html", actual);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ParseTest3()
		{
			var markdown = File.ReadAllText("TestFiles/TestFile3.md");
			var expected = File.ReadAllText("TestFiles/TestFile3.html");
			var actual = Processor.Convert(markdown);
			Console.WriteLine(actual);
			Assert.AreEqual(expected, actual);
		}

		#endregion
	}
}