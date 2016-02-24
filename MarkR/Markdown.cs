#region References

using System;
using System.Reflection;
using MarkR.Internal;

#endregion

namespace MarkR
{
	public class Markdown
	{
		#region Constructors

		public Markdown()
			: this(null)
		{
		}

		public Markdown(Options options)
		{
			Options = options ?? new Options();
			Options.Renderer.ImageParsed += (sender, args) => OnImageParsed(args);
			Options.Renderer.LinkParsed += (sender, args) => OnLinkParsed(args);
		}

		#endregion

		#region Properties

		public Options Options { get; }

		/// <summary>
		/// Version of this markdown parser.
		/// </summary>
		public string Version => Assembly.GetCallingAssembly().GetName().Version.ToString();

		#endregion

		#region Methods

		public virtual string Transform(string markdown)
		{
			if (string.IsNullOrEmpty(markdown))
			{
				return markdown;
			}

			var tokens = Lexer.Lex(markdown, Options);
			var result = Parser.Parse(tokens, Options);
			return result;
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

		public event EventHandler<ImageEventArgs> ImageParsed;
		public event EventHandler<LinkEventArgs> LinkParsed;

		#endregion
	}
}