#region References

using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

#endregion

namespace MarkR
{
	public static class Processor
	{
		#region Static Methods

		/// <summary>
		/// Converts markdown input into HTML output.
		/// </summary>
		/// <param name="markdown">The markdown text to convert.</param>
		/// <returns>The HTML output from the markdown input.</returns>
		public static string Convert(string markdown)
		{
			var linkRegex = new Regex("\\[[a-zA-Z ]*\\]\\([\\w\\d:/.#&-_@]*( \\\"[\\w ]*\\\")*?\\)");
			var lines = markdown.Split(new[] { Environment.NewLine, "\n", "\r" }, StringSplitOptions.None).ToList();
			var startedParagraph = -1;

			for (var i = 0; i < lines.Count; i++)
			{
				if (lines[i].StartsWith("#"))
				{
					lines[i] = ParseHeader(lines[i]);
					continue;
				}

				if (lines[i].StartsWith("```"))
				{
					for (var j = i + 1; j < lines.Count; j++)
					{
						lines[j] = HttpUtility.HtmlEncode(lines[j]);
						if (!lines[j].StartsWith("```"))
						{
							continue;
						}

						// Set start of block
						var className = lines[i].Replace("```", string.Empty);
						lines.RemoveAt(i);
						lines.RemoveAt(j - 1);
						lines[i] = (className.Length > 0 ? "<pre><code class=\"" + className + "\">" : "<pre><code>") + lines[i];
						lines[j - 2] += "</code></pre>";
						i = j - 2;
						break;
					}

					continue;
				}

				if (lines[i].StartsWith("* "))
				{
					var j = i;
					while (++j < lines.Count && lines[j].StartsWith("* "))
					{
						// Do nothing... but loop.
					}

					for (var k = i; k < j; k++)
					{
						lines[k] = lines[k].Replace("* ", "<li>") + "</li>";
					}

					lines.Insert(i, "<ul>");
					lines.Insert(j + 1, "</ul>");
					i = j + 1;
				}

				if (linkRegex.IsMatch(lines[i]))
				{
					var match = linkRegex.Match(lines[i]);
					do
					{
						var link = LinkParser.Parse(match.Value);
						lines[i] = lines[i].Replace(match.Value, link);
					} while ((match = match.NextMatch()).Success);
				}

				if (startedParagraph == -1 && lines[i].Length > 0 && !lines[i].Trim().StartsWith("<"))
				{
					startedParagraph = i;
					continue;
				}
				
				if (startedParagraph >= 0 && string.IsNullOrWhiteSpace(lines[i]))
				{
					lines[startedParagraph] = "<p>" + lines[startedParagraph];
					lines[i-1] += "</p>";
					startedParagraph = -1;
				}
			}

			if (startedParagraph > 0)
			{
				lines[startedParagraph] = "<p>" + lines[startedParagraph];
				lines[lines.Count - 1] += "</p>";
			}

			return string.Join(Environment.NewLine, lines);
		}

		private static string ParseHeader(string line)
		{
			var header = "h1";
			if (line.StartsWith("######"))
			{
				header = "h6";
				line = line.Replace("######", string.Empty).Trim();
			} 
			else if (line.StartsWith("#####"))
			{
				header = "h5";
				line = line.Replace("#####", string.Empty).Trim();
			} 
			else if (line.StartsWith("####"))
			{
				header = "h4";
				line = line.Replace("####", string.Empty).Trim();
			}
			else if (line.StartsWith("###"))
			{
				header = "h3";
				line = line.Replace("###", string.Empty).Trim();
			}
			else if (line.StartsWith("##"))
			{
				header = "h2";
				line = line.Replace("##", string.Empty).Trim();
			}
			else
			{
				line = line.Replace("#", string.Empty).Trim();
			}

			if (line.StartsWith(":"))
			{
				var index = line.IndexOf(" ");
				var classNames = line.Substring(0, index)
					.Replace(":", string.Empty)
					.Replace(",", " ")
					.Trim();

				line = line.Substring(index).Trim();
				return string.Format("<{0} class=\"{1}\">{2}</{0}>", header, classNames, line);
			}
			
			return string.Format("<{0}>{1}</{0}>", header, line);
		}

		#endregion
	}
}