#region References

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace MarkR.Internal
{
	internal class Parser
	{
		#region Fields

		private readonly Options _options;
		private InlineLexer inline;
		private Token token;
		private Stack<Token> tokens;

		#endregion

		#region Constructors

		public Parser(Options options)
		{
			tokens = new Stack<Token>();
			token = null;
			_options = options ?? new Options();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Static Parse Method
		/// </summary>
		public static string Parse(TokensResult src, Options options)
		{
			var parser = new Parser(options);
			return parser.Parse(src);
		}

		/// <summary>
		/// Parse Loop
		/// </summary>
		public virtual string Parse(TokensResult src)
		{
			inline = new InlineLexer(src.Links, _options);
			tokens = new Stack<Token>(src.Reverse());

			var @out = string.Empty;
			while (Next() != null)
			{
				@out += Tok();
			}

			return @out;
		}

		/// <summary>
		/// Next Token
		/// </summary>
		protected virtual Token Next()
		{
			return token = (tokens.Any()) ? tokens.Pop() : null;
		}

		/// <summary>
		/// Parse Text Tokens
		/// </summary>
		protected virtual string ParseText()
		{
			var body = token.Text;

			while (Peek().Type == "text")
			{
				body += '\n' + Next().Text;
			}

			return inline.Output(body);
		}

		/// <summary>
		/// Preview Next Token
		/// </summary>
		protected virtual Token Peek()
		{
			return tokens.Peek() ?? new Token();
		}

		/// <summary>
		/// Parse Current Token
		/// </summary>
		protected virtual string Tok()
		{
			switch (token.Type)
			{
				case "space":
				{
					return string.Empty;
				}
				case "hr":
				{
					return _options.Renderer.Hr();
				}
				case "heading":
				{
					return _options.Renderer.Heading(inline.Output(token.Text), token.Depth, token.Text);
				}
				case "code":
				{
					return _options.Renderer.Code(token.Text, token.Lang, token.Escaped);
				}
				case "table":
				{
					string header = string.Empty,
						body = string.Empty;

					// header
					var cell = string.Empty;
					for (var i = 0; i < token.Header.Count; i++)
					{
						cell += _options.Renderer.TableCell(
							inline.Output(token.Header[i]),
							new TableCellFlags { Header = true, Align = i < token.Align.Count ? token.Align[i] : null }
							);
					}
					header += _options.Renderer.TableRow(cell);

					for (var i = 0; i < token.Cells.Count; i++)
					{
						var row = token.Cells[i];

						cell = string.Empty;
						for (var j = 0; j < row.Count; j++)
						{
							cell += _options.Renderer.TableCell(
								inline.Output(row[j]),
								new TableCellFlags { Header = false, Align = j < token.Align.Count ? token.Align[j] : null }
								);
						}

						body += _options.Renderer.TableRow(cell);
					}
					return _options.Renderer.Table(header, body);
				}
				case "blockquote_start":
				{
					var body = string.Empty;

					while (Next().Type != "blockquote_end")
					{
						body += Tok();
					}

					return _options.Renderer.Blockquote(body);
				}
				case "list_start":
				{
					var body = string.Empty;
					var ordered = token.Ordered;

					while (Next().Type != "list_end")
					{
						body += Tok();
					}

					return _options.Renderer.List(body, ordered);
				}
				case "list_item_start":
				{
					var body = string.Empty;

					while (Next().Type != "list_item_end")
					{
						body += token.Type == "text"
							? ParseText()
							: Tok();
					}

					return _options.Renderer.ListItem(body);
				}
				case "loose_item_start":
				{
					var body = string.Empty;

					while (Next().Type != "list_item_end")
					{
						body += Tok();
					}

					return _options.Renderer.ListItem(body);
				}
				case "html":
				{
					var html = !token.Pre && !_options.Pedantic
						? inline.Output(token.Text)
						: token.Text;
					return _options.Renderer.Html(html);
				}
				case "paragraph":
				{
					return _options.Renderer.Paragraph(inline.Output(token.Text));
				}
				case "text":
				{
					return _options.Renderer.Paragraph(ParseText());
				}
			}

			throw new Exception();
		}

		#endregion
	}
}