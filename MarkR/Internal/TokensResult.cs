#region References

using System.Collections.Generic;
using System.Linq;

#endregion

namespace MarkR.Internal
{
	internal class TokensResult
	{
		#region Constructors

		public TokensResult()
		{
			Tokens = new List<Token>();
			Links = new Dictionary<string, LinkObj>();
		}

		#endregion

		#region Properties

		public int Length
		{
			get { return Tokens.Count; }
		}

		public IDictionary<string, LinkObj> Links { get; set; }
		public IList<Token> Tokens { get; set; }

		#endregion

		#region Methods

		public void Add(Token token)
		{
			Tokens.Add(token);
		}

		public IEnumerable<Token> Reverse()
		{
			return Tokens.Reverse();
		}

		#endregion
	}
}