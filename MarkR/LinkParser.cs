#region References

using System;
using System.Linq;

#endregion

namespace MarkR
{
	public class LinkParser
	{
		#region Properties

		public string Content { get; set; }
		public string Link { get; set; }
		public string Title { get; set; }

		#endregion

		#region Methods

		public void Load(string data)
		{
			var split = data.Split(new[] { "](" }, StringSplitOptions.None);
			Content = split[0].Substring(1);
			var linkData = split[1].Substring(0, split[1].Length - 1);
			var index = linkData.IndexOf(' ');
			Link = linkData;
			Title = string.Empty;

			if (index >= 0)
			{
				Link = linkData.Substring(0, index);
				Title = linkData.Substring(index).Trim().Replace("\"", string.Empty);
			}
		}

		public string ToLink()
		{
			return Title.Any()
				? string.Format("<a href=\"{0}\" title=\"{1}\">{2}</a>", Link, Title, Content)
				: string.Format("<a href=\"{0}\">{1}</a>", Link, Content);
		}

		#endregion

		#region Static Methods

		public static string Parse(string data)
		{
			var parser = new LinkParser();
			parser.Load(data);
			return parser.ToLink();
		}

		#endregion
	}
}