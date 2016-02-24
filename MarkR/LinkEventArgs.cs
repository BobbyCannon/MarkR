#region References

using System;

#endregion

namespace MarkR
{
	/// <summary>
	/// Holds information when a hyperlink is processed, giving the caller the ability to translate the outputted HTML.
	/// </summary>
	public class LinkEventArgs : EventArgs
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="LinkEventArgs" /> class.
		/// </summary>
		public LinkEventArgs(string originalHref, string href, string text, string title, string target)
		{
			OriginalHref = originalHref;
			Href = href;
			Target = target;
			Text = text;
			Title = title;
			CssClass = "";
			IsInternalLink = false;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The css class attribute.
		/// </summary>
		public string CssClass { get; set; }

		/// <summary>
		/// The href to use for the HTML.
		/// </summary>
		public string Href { get; set; }

		/// <summary>
		/// True if the link points to another page in the wiki, including Special: urls, and attachments.
		/// </summary>
		public bool IsInternalLink { get; set; }

		/// <summary>
		/// The original href.
		/// </summary>
		public string OriginalHref { get; set; }

		/// <summary>
		/// The target attribute, e.g. _blank.
		/// </summary>
		public string Target { get; set; }

		/// <summary>
		/// The title tag for the link.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// The link text.
		/// </summary>
		public string Text { get; set; }

		#endregion
	}
}