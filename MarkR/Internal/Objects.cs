namespace MarkR.Internal
{
	internal class TableCellFlags
	{
		#region Properties

		public string Align { get; set; }
		public bool Header { get; set; }

		#endregion
	}

	internal class LinkObj
	{
		#region Properties

		public string Href { get; set; }
		public string Title { get; set; }

		#endregion
	}
}