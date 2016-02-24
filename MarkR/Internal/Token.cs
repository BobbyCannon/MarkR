#region References

using System.Collections.Generic;

#endregion

namespace MarkR.Internal
{
	internal class Token
	{
		#region Properties

		public IList<string> Align { get; set; }
		public IList<IList<string>> Cells { get; set; }

		public int Depth { get; set; }
		public bool Escaped { get; set; }

		public IList<string> Header { get; set; }
		public string Lang { get; set; }
		public bool Ordered { get; set; }

		public bool Pre { get; set; }
		public string Text { get; set; }
		public string Type { get; set; }

		#endregion
	}
}