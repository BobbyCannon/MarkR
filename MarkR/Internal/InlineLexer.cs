#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

#endregion

namespace MarkR.Internal
{
	/// <summary>
	/// Inline Lexer & Compiler
	/// </summary>
	internal class InlineLexer
	{
		#region Fields

		private readonly Options _options;
		private readonly Random _random = new Random();
		private readonly InlineRules _rules;
		private bool inLink;
		private readonly IDictionary<string, LinkObj> links;

		#endregion

		#region Constructors

		public InlineLexer(IDictionary<string, LinkObj> links, Options options)
		{
			_options = options ?? new Options();

			this.links = links;
			_rules = new NormalInlineRules();

			if (this.links == null)
			{
				throw new Exception("Tokens array requires a `links` property.");
			}

			if (_options.Gfm)
			{
				if (_options.Breaks)
				{
					_rules = new BreaksInlineRules();
				}
				else
				{
					_rules = new GfmInlineRules();
				}
			}
			else if (_options.Pedantic)
			{
				_rules = new PedanticInlineRules();
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Lexing/Compiling
		/// </summary>
		public virtual string Output(string src)
		{
			var @out = string.Empty;
			LinkObj link;
			string text;
			string href;
			IList<string> cap;

			while (!string.IsNullOrEmpty(src))
			{
				// escape
				cap = _rules.Escape.Exec(src);
				if (cap.Any())
				{
					src = src.Substring(cap[0].Length);
					@out += cap[1];
					continue;
				}

				// autolink
				cap = _rules.AutoLink.Exec(src);
				if (cap.Any())
				{
					src = src.Substring(cap[0].Length);
					if (cap[2] == "@")
					{
						text = cap[1][6] == ':'
							? Mangle(cap[1].Substring(7))
							: Mangle(cap[1]);
						href = Mangle("mailto:") + text;
					}
					else
					{
						text = StringHelper.Escape(cap[1]);
						href = text;
					}
					@out += _options.Renderer.Link(href, null, text);
					continue;
				}

				// url (gfm)
				if (!inLink && (cap = _rules.Url.Exec(src)).Any())
				{
					src = src.Substring(cap[0].Length);
					text = StringHelper.Escape(cap[1]);
					href = text;
					@out += _options.Renderer.Link(href, null, text);
					continue;
				}

				// tag
				cap = _rules.Tag.Exec(src);
				if (cap.Any())
				{
					if (!inLink && Regex.IsMatch(cap[0], "^<a ", RegexOptions.IgnoreCase))
					{
						inLink = true;
					}
					else if (inLink && Regex.IsMatch(cap[0], @"^<\/a>", RegexOptions.IgnoreCase))
					{
						inLink = false;
					}
					src = src.Substring(cap[0].Length);
					@out += _options.Sanitize
						? (_options.Sanitizer != null)
							? _options.Sanitizer(cap[0])
							: StringHelper.Escape(cap[0])
						: cap[0];
					continue;
				}

				// link
				cap = _rules.Link.Exec(src);
				if (cap.Any())
				{
					src = src.Substring(cap[0].Length);
					inLink = true;
					@out += OutputLink(cap, new LinkObj
					{
						Href = cap[2],
						Title = cap[3]
					});
					inLink = false;
					continue;
				}

				// reflink, nolink
				if ((cap = _rules.RefLink.Exec(src)).Any() || (cap = _rules.NoLink.Exec(src)).Any())
				{
					src = src.Substring(cap[0].Length);
					var linkStr = (StringHelper.NotEmpty(cap, 2, 1)).ReplaceRegex(@"\s+", " ");

					links.TryGetValue(linkStr.ToLower(), out link);

					if (link == null || string.IsNullOrEmpty(link.Href))
					{
						@out += cap[0][0];
						src = cap[0].Substring(1) + src;
						continue;
					}
					inLink = true;
					@out += OutputLink(cap, link);
					inLink = false;
					continue;
				}

				// strong
				if ((cap = _rules.Strong.Exec(src)).Any())
				{
					src = src.Substring(cap[0].Length);
					@out += _options.Renderer.Strong(Output(StringHelper.NotEmpty(cap, 2, 1)));
					continue;
				}

				// em
				cap = _rules.Em.Exec(src);
				if (cap.Any())
				{
					src = src.Substring(cap[0].Length);
					@out += _options.Renderer.Em(Output(StringHelper.NotEmpty(cap, 2, 1)));
					continue;
				}

				// code
				cap = _rules.Code.Exec(src);
				if (cap.Any())
				{
					src = src.Substring(cap[0].Length);
					@out += _options.Renderer.Codespan(StringHelper.Escape(cap[2], true));
					continue;
				}

				// br
				cap = _rules.Br.Exec(src);
				if (cap.Any())
				{
					src = src.Substring(cap[0].Length);
					@out += _options.Renderer.Br();
					continue;
				}

				// del (gfm)
				cap = _rules.Del.Exec(src);
				if (cap.Any())
				{
					src = src.Substring(cap[0].Length);
					@out += _options.Renderer.Del(Output(cap[1]));
					continue;
				}

				// text
				cap = _rules.Text.Exec(src);
				if (cap.Any())
				{
					src = src.Substring(cap[0].Length);
					@out += _options.Renderer.Text(StringHelper.Escape(Smartypants(cap[0])));
					continue;
				}

				if (!string.IsNullOrEmpty(src))
				{
					throw new Exception("Infinite loop on byte: " + (int) src[0]);
				}
			}

			return @out;
		}

		/// <summary>
		/// Mangle Links
		/// </summary>
		protected virtual string Mangle(string text)
		{
			if (!_options.Mangle)
			{
				return text;
			}
			var @out = string.Empty;

			for (var i = 0; i < text.Length; i++)
			{
				var ch = text[i].ToString();
				if (_random.NextDouble() > 0.5)
				{
					ch = 'x' + Convert.ToString(ch[0], 16);
				}
				@out += "&#" + ch + ";";
			}

			return @out;
		}

		/// <summary>
		/// Compile Link
		/// </summary>
		protected virtual string OutputLink(IList<string> cap, LinkObj link)
		{
			var href = StringHelper.Escape(link.Href);
			var title = !string.IsNullOrEmpty(link.Title) ? StringHelper.Escape(link.Title) : null;

			return cap[0][0] != '!'
				? _options.Renderer.Link(href, title, Output(cap[1]))
				: _options.Renderer.Image(href, title, StringHelper.Escape(cap[1]));
		}

		/// <summary>
		/// Smartypants Transformations
		/// </summary>
		protected virtual string Smartypants(string text)
		{
			if (!_options.Smartypants)
			{
				return text;
			}

			return text
				// em-dashes
				.Replace("---", "\u2014")
				// en-dashes
				.Replace("--", "\u2013")
				// opening singles
				.ReplaceRegex(@"(^|[-\u2014/(\[{""\s])'", "$1\u2018")
				// closing singles & apostrophes
				.Replace("'", "\u2019")
				// opening doubles
				.ReplaceRegex(@"(^|[-\u2014/(\[{\u2018\s])""", "$1\u201c")
				// closing doubles
				.Replace("\"", "\u201d")
				// ellipses
				.Replace("...", "\u2026");
		}

		#endregion
	}
}