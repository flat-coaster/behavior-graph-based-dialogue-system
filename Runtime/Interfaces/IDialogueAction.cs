using System.Collections.Generic;
using Unity.Behavior;

namespace FlatCoaster.Systems.DialogueSystem
{
	public interface IDialogueAction
    {
        DialogueSayData DialogueData { get; }
        void Continue();
    }

    public interface IDialogueOptionable
    {
        List<string> GetOptions();
        void Continue(int p_optionIndex);
    }
}
