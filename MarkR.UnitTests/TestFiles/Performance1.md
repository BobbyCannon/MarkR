# Header

* One
* Two
* Three
* Four

## Code

```
#region References

using System;
using System.Linq;

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
			var lines = markdown.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
			for (var i = 0; i < lines.Count; i++)
			{
				if (lines[i].StartsWith("#"))
				{
					lines[i] = ParseHeaders(lines[i]);
					continue;
				}

				if (lines[i].StartsWith("```"))
				{
					for (var j = i + 1; j < lines.Count; j++)
					{
						if (!lines[j].StartsWith("```"))
						{
							continue;
						}

						// Set start of block
						var className = lines[i].Replace("```", string.Empty);
						lines[i] = className.Length > 0 ? "<pre><code class=\"" + className + "\">" : "<pre><code>";
						lines[j] = "</code></pre>";
						i = j;
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
					lines.Insert(j + 1, "</ul>" );
					i = j + 1;
				}
			}

			return string.Join(Environment.NewLine, lines);
		}

		private static string ParseHeaders(string line)
		{
			if (line.StartsWith("######"))
			{
				return line.Replace("###### ", "<h6>").Replace("######", "<h6>") + "</h6>";
			}

			if (line.StartsWith("#####"))
			{
				return line.Replace("##### ", "<h5>").Replace("#####", "<h5>") + "</h5>";
			}

			if (line.StartsWith("####"))
			{
				return line.Replace("#### ", "<h4>").Replace("####", "<h4>") + "</h4>";
			}

			if (line.StartsWith("###"))
			{
				return line.Replace("### ", "<h3>").Replace("###", "<h3>") + "</h3>";
			}

			if (line.StartsWith("##"))
			{
				return line.Replace("## ", "<h2>").Replace("##", "<h2>") + "</h2>";
			}

			return line.Replace("# ", "<h1>").Replace("#", "<h1>") + "</h1>";
		}

		#endregion
	}
}
```

### Header Three
More text here...

<table>
	<tr>
		<td>col</td>
		<td>data</td>
	</tr>
</table>