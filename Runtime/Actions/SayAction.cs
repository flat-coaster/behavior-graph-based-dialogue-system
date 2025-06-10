using Unity.VisualScripting;
using Action = Unity.Behavior.Action;

namespace FlatCoaster.Systems.DialogueSystem
{
	public abstract class SayAction : Action, IDialogueAction
    {
        protected bool m_continue = false;

		public abstract DialogueSayData DialogueData { get; }

		protected virtual void Say()
		{
			Singleton<DialogueManager>.instance.Say(DialogueData);
		}

		protected override Status OnStart()
        {
            Say();
			m_continue = false;
            return Status.Running;
        }

		protected override Status OnUpdate()
		{
			if (m_continue)
			{
				return Status.Success;
			}

			return Status.Running;
		}

		protected override void OnEnd()
		{
			m_continue = false;
		}

		public void Continue()
        {
            m_continue = true;
        }
	}
}
