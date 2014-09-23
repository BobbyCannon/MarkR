#region References

using System.Management.Automation;

#endregion

namespace MarkR.PowerShell
{
	[Cmdlet(VerbsData.Convert, "Markdown")]
	public class ConvertMarkdownCmdlet : Cmdlet
	{
		#region Properties

		[Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
		public string Input { get; set; }

		#endregion

		#region Methods

		protected override void ProcessRecord()
		{
			WriteObject(Processor.Convert(Input));
		}

		#endregion
	}
}