#region References

using System.Text.RegularExpressions;

#endregion

namespace MarkR.Internal
{
	/// <summary>
	/// Block-Level Grammar
	/// </summary>
	internal class BlockRules
	{
		#region Fields

		private static readonly Regex blockquote = new Regex(@"^( *>[^\n]+(\n(?! *\[([^\]]+)\]: *<?([^\s>]+)>?(?: +[""(]([^\n]+)["")])? *(?:\n+|$))[^\n]+)*\n*)+");
		private static readonly Regex bullet = new Regex(@"(?:[*+-]|\d+\.)");
		private static readonly Regex code = new Regex(@"^( {4}[^\n]+\n*)+");
		private static readonly Regex def = new Regex(@"^ *\[([^\]]+)\]: *<?([^\s>]+)>?(?: +[""(]([^\n]+)["")])? *(?:\n+|$)");
		private static readonly Regex fences = new Regex(@"(`{3})(\S+)?([\s\S][^`]+)(`{3})");
		private static readonly Regex heading = new Regex(@"^ *(#{1,6}) *([^\n]+?) *#* *(?:\n+|$)");
		private static readonly Regex hr = new Regex(@"^( *[-*_]){3,} *(?:\n+|$)");
		private static readonly Regex html = new Regex(@"^ *(?:<!--[\s\S]*?-->|<((?!(?:a|em|strong|small|s|cite|q|dfn|abbr|data|time|code|var|samp|kbd|sub|sup|i|b|u|mark|ruby|rt|rp|bdi|bdo|span|br|wbr|ins|del|img)\b)\w+(?!:\/|[^\w\s@]*@)\b)[\s\S]+?<\/\1>|<(?!(?:a|em|strong|small|s|cite|q|dfn|abbr|data|time|code|var|samp|kbd|sub|sup|i|b|u|mark|ruby|rt|rp|bdi|bdo|span|br|wbr|ins|del|img)\b)\w+(?!:\/|[^\w\s@]*@)\b(?:""[^""]*""|'[^']*'|[^'"">])*?>) *(?:\n{2,}|\s*$)");
		private static readonly Regex item = new Regex(@"^( *)((?:[*+-]|\d+\.)) [^\n]*(?:\n(?!\1(?:[*+-]|\d+\.) )[^\n]*)*", RegexOptions.Multiline);
		private static readonly Regex lHeading = new Regex(@"^([^\n]+)\n *(=|-){2,} *(?:\n+|$)");
		private static readonly Regex list = new Regex(@"^( *)((?:[*+-]|\d+\.)) [\s\S]+?(?:\n+(?=\1?(?:[-*_] *){3,}(?:\n+|$))|\n+(?= *\[([^\]]+)\]: *<?([^\s>]+)>?(?: +[""(]([^\n]+)["")])? *(?:\n+|$))|\n{2,}(?! )(?!\1(?:[*+-]|\d+\.) )\n*|\s*$)");
		private static readonly Regex newline = new Regex(@"^\n+");
		private static readonly Regex npTable = new Regex("");
		private static readonly Regex paragraph = new Regex(@"^((?:[^\n]+\n?(?!( *[-*_]){3,} *(?:\n+|$)| *(#{1,6}) *([^\n]+?) *#* *(?:\n+|$)|([^\n]+)\n *(=|-){2,} *(?:\n+|$)|( *>[^\n]+(\n(?! *\[([^\]]+)\]: *<?([^\s>]+)>?(?: +[""(]([^\n]+)["")])? *(?:\n+|$))[^\n]+)*\n*)+|<(?!(?:a|em|strong|small|s|cite|q|dfn|abbr|data|time|code|var|samp|kbd|sub|sup|i|b|u|mark|ruby|rt|rp|bdi|bdo|span|br|wbr|ins|del|img)\b)\w+(?!:\/|[^\w\s@]*@)\b| *\[([^\]]+)\]: *<?([^\s>]+)>?(?: +[""(]([^\n]+)["")])? *(?:\n+|$)))+)\n*");
		private static readonly Regex table = new Regex("");
		private static readonly Regex text = new Regex(@"^[^\n]+");

		#endregion

		#region Properties

		public virtual Regex Blockquote => blockquote;

		public virtual Regex Bullet => bullet;

		public virtual Regex Def => def;

		public virtual Regex Fences => fences;

		public virtual Regex Heading => heading;

		public virtual Regex Hr => hr;

		public virtual Regex Html => html;

		public virtual Regex Item => item;

		public virtual Regex LHeading => lHeading;

		public virtual Regex List => list;

		public virtual Regex Newline => newline;

		public virtual Regex NpTable => npTable;

		public virtual Regex Paragraph => paragraph;

		public virtual Regex Table => table;

		public virtual Regex Text => text;

		public virtual Regex Сode => code;

		#endregion
	}

	/// <summary>
	/// Normal Block Grammar
	/// </summary>
	internal class NormalBlockRules : BlockRules
	{
	}

	/// <summary>
	/// GFM Block Grammar
	/// </summary>
	internal class GfmBlockRules : BlockRules
	{
		#region Fields

		private static readonly Regex fences = new Regex(@"^ *(`{3,}|~{3,}) *(\S+)? *\n([\s\S]+?)\s*\1 *(?:\n+|$)");
		private static readonly Regex heading = new Regex(@"^ *(#{1,6}) *([^\n]+?) *#* *(?:\n+|$)");
		private static readonly Regex paragraph = new Regex(@"^((?:[^\n]+\n?(?! *(`{3,}|~{3,}) *(\S+)? *\n([\s\S]+?)\s*\2 *(?:\n+|$)|( *)((?:[*+-]|\d+\.)) [\s\S]+?(?:\n+(?=\3?(?:[-*_] *){3,}(?:\n+|$))|\n+(?= *\[([^\]]+)\]: *<?([^\s>]+)>?(?: +[""(]([^\n]+)["")])? *(?:\n+|$))|\n{2,}(?! )(?!\1(?:[*+-]|\d+\.) )\n*|\s*$)|( *[-*_]){3,} *(?:\n+|$)| *(#{1,6}) *([^\n]+?) *#* *(?:\n+|$)|([^\n]+)\n *(=|-){2,} *(?:\n+|$)|( *>[^\n]+(\n(?! *\[([^\]]+)\]: *<?([^\s>]+)>?(?: +[""(]([^\n]+)["")])? *(?:\n+|$))[^\n]+)*\n*)+|<(?!(?:a|em|strong|small|s|cite|q|dfn|abbr|data|time|code|var|samp|kbd|sub|sup|i|b|u|mark|ruby|rt|rp|bdi|bdo|span|br|wbr|ins|del|img)\b)\w+(?!:\/|[^\w\s@]*@)\b| *\[([^\]]+)\]: *<?([^\s>]+)>?(?: +[""(]([^\n]+)["")])? *(?:\n+|$)))+)\n*");

		#endregion

		#region Properties

		public override Regex Fences
		{
			get { return fences; }
		}

		public override Regex Heading
		{
			get { return heading; }
		}

		public override Regex Paragraph
		{
			get { return paragraph; }
		}

		#endregion
	}

	/// <summary>
	/// GFM + Tables Block Grammar
	/// </summary>
	internal class TablesBlockRules : GfmBlockRules
	{
		#region Fields

		private static readonly Regex npTable = new Regex(@"^ *(\S.*\|.*)\n *([-:]+ *\|[-| :]*)\n((?:.*\|.*(?:\n|$))*)\n*");
		private static readonly Regex table = new Regex(@"^ *\|(.+)\n *\|( *[-:]+[-| :]*)\n((?: *\|.*(?:\n|$))*)\n*");

		#endregion

		#region Properties

		public override Regex NpTable => npTable;

		public override Regex Table => table;

		#endregion
	}
}