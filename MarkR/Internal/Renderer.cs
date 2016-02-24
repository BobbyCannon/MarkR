#region References

using System;
using System.Text.RegularExpressions;

#endregion

namespace MarkR.Internal
{
	internal class Renderer
	{
		#region Constructors

		public Renderer()
			: this(null)
		{
		}

		public Renderer(Options options)
		{
			Options = options ?? new Options();
		}

		#endregion

		#region Properties

		public Options Options { get; set; }

		#endregion

		#region Methods

		public virtual string Blockquote(string quote)
		{
			return "<blockquote>\n" + quote + "</blockquote>\n";
		}

		public virtual string Br()
		{
			return Options.XHtml ? "<br/>" : "<br>";
		}

		public virtual string Code(string code, string lang, bool escaped)
		{
			var @out = Options.Highlight?.Invoke(code, lang);
			if (@out != null && @out != code)
			{
				escaped = true;
				code = @out;
			}

			if (string.IsNullOrEmpty(lang))
			{
				return "<pre><code>" + (escaped ? code : StringHelper.Escape(code, true)) + "</code></pre>";
			}

			return "<pre><code class=\""
				+ Options.LangPrefix
				+ StringHelper.Escape(lang, true)
				+ "\">"
				+ (escaped ? code : StringHelper.Escape(code, true))
				+ "</code></pre>\n";
		}

		public virtual string Codespan(string text)
		{
			return "<code>" + text + "</code>";
		}

		public virtual string Del(string text)
		{
			return "<del>" + text + "</del>";
		}

		public virtual string Em(string text)
		{
			return "<em>" + text + "</em>";
		}

		public virtual string Heading(string text, int level, string raw)
		{
			var startIndex = raw.IndexOf("{#", StringComparison.Ordinal) + 2;
			var endIndex = raw.IndexOf("}", startIndex, StringComparison.Ordinal);

			if (startIndex < 0 || endIndex <= startIndex)
			{
				return string.Format("<h{0}>{1}</h{0}>\n", level, text);
			}

			var id = Options.HeaderPrefix + raw.Substring(startIndex, endIndex - startIndex);
			text = raw.Remove(startIndex - 2, id.Length + 3);

			return string.Format("<h{0} id=\"{1}\">{2}</h{0}>\n", level, id, text);
		}

		public virtual string Hr()
		{
			return Options.XHtml ? "<hr />\n" : "<hr>\n";
		}

		public virtual string Html(string html)
		{
			return html;
		}

		public virtual string Image(string href, string title, string text)
		{
			var args = new ImageEventArgs(href, href, text, title);
			OnImageParsed(args);

			var @out = "<img src=\"" + args.Src + "\" alt=\"" + args.Alt + "\"";

			if (!string.IsNullOrEmpty(args.Title))
			{
				@out += " title=\"" + args.Title + "\"";
			}

			@out += Options.XHtml ? " />" : ">";
			return @out;
		}

		public virtual string Link(string href, string title, string text)
		{
			if (Options.Sanitize)
			{
				string prot = null;

				try
				{
					prot = Regex.Replace(StringHelper.DecodeURIComponent(StringHelper.Unescape(href)), @"[^\w:]", string.Empty).ToLower();
				}
				catch (Exception)
				{
					return string.Empty;
				}

				if (prot.IndexOf("javascript:") == 0 || prot.IndexOf("vbscript:") == 0)
				{
					return string.Empty;
				}
			}

			var args = new LinkEventArgs(href, href, text, title, "");
			OnLinkParsed(args);

			var response = $"<a href=\"{args.Href}\"";

			if (!string.IsNullOrEmpty(args.Title))
			{
				response += $" title=\"{args.Title}\"";
			}

			if (!string.IsNullOrEmpty(args.CssClass))
			{
				response += $" class=\"{args.CssClass}\"";
			}

			response += ">" + args.Text + "</a>";
			return response;
		}

		public virtual string List(string body, bool ordered)
		{
			var type = ordered ? "ol" : "ul";
			return "<" + type + ">\n" + body + "</" + type + ">\n";
		}

		public virtual string ListItem(string text)
		{
			return $"<li>{text}</li>\n";
		}

		public virtual string Paragraph(string text)
		{
			return $"<p>{text}</p>\n";
		}

		public virtual string Strong(string text)
		{
			return $"<strong>{text}</strong>";
		}

		public virtual string Table(string header, string body)
		{
			return $"<table>\n<thead>\n{header}</thead>\n<tbody>\n{body}</tbody>\n</table>\n";
		}

		internal virtual string TableCell(string content, TableCellFlags flags)
		{
			var type = flags.Header ? "th" : "td";
			var tag = !string.IsNullOrEmpty(flags.Align)
				? "<" + type + " style=\"text-align:" + flags.Align + "\">"
				: "<" + type + ">";

			return tag + content + "</" + type + ">\n";
		}

		public virtual string TableRow(string content)
		{
			return "<tr>\n" + content + "</tr>\n";
		}

		public virtual string Text(string text)
		{
			return text;
		}

		/// <summary>
		/// Raises the <see cref="ImageParsed" /> event.
		/// </summary>
		/// <param name="e"> The event data. </param>
		private void OnImageParsed(ImageEventArgs e)
		{
			ImageParsed?.Invoke(this, e);
		}

		/// <summary>
		/// Raises the <see cref="LinkParsed" /> event.
		/// </summary>
		/// <param name="e"> The event data. </param>
		private void OnLinkParsed(LinkEventArgs e)
		{
			LinkParsed?.Invoke(this, e);
		}

		#endregion

		#region Events

		internal event EventHandler<ImageEventArgs> ImageParsed;
		internal event EventHandler<LinkEventArgs> LinkParsed;

		#endregion
	}
}