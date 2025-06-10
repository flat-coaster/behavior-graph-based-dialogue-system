using System.Collections.Generic;
using Unity.Behavior;

namespace FlatCoaster.Systems.DialogueSystem
{
	public partial class DialogueSayData
	{
		public IDialogueActor Actor { get; set; }
		public string Sentence { get; set; }

		private readonly IDialogueAction m_origin;

		public DialogueSayData(IDialogueAction p_origin)
		{
			m_origin = p_origin;
		}

		public void Continue()
		{
			m_origin.Continue();
		}
	}

	public partial class DialogueSayOptionsData
	{
		private readonly IDialogueOptionable m_origin;

		public DialogueSayOptionsData(IDialogueOptionable p_origin)
		{
			m_origin = p_origin;
		}

		public List<string> GetOptions()
		{
			return m_origin.GetOptions();
		}

		public void SelectOption(int p_optionIndex)
		{
			m_origin.Continue(p_optionIndex);
		}
	}
}
