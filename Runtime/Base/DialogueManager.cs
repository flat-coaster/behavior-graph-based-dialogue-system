using System;
using Unity.Behavior;
using Unity.VisualScripting;
using UnityEngine;

namespace FlatCoaster.Systems.DialogueSystem
{
	[Singleton]
	[RequireComponent(typeof(BehaviorGraphAgent))]
	public class DialogueManager : MonoBehaviour, ISingleton
    {
		private BehaviorGraphAgent m_graphAgent;

		public static event DialogueStartedEvent OnDialogueStarted;
		public static event DialogueEndedEvent OnDialogueEnded;
		public static event DialogueSayEvent OnDialogueSay;
		public static event DialogueSayOptionEvent OnDialogueSayOption;

		private void Awake()
		{
			if (Singleton<DialogueManager>.instantiated)
			{
				Destroy(gameObject);
				return;
			}

			Singleton<DialogueManager>.Instantiate();
		}

		private void Start()
		{
			if (m_graphAgent == null)
			{
				m_graphAgent = gameObject.GetOrAddComponent<BehaviorGraphAgent>();
			}
		}

		public void StartDialogue(BehaviorGraph p_dialogueGraph)
		{
			m_graphAgent.Graph = p_dialogueGraph;

			OnDialogueStarted?.Invoke(m_graphAgent.Graph);
		}

		public void EndActiveDialogue()
		{
			if (m_graphAgent == null)
			{
				return;
			}

			OnDialogueEnded?.Invoke(m_graphAgent.Graph);
			m_graphAgent.Graph = null;
		}

		public void SetOptions(DialogueSayOptionsData p_optionData)
		{
			OnDialogueSayOption?.Invoke(p_optionData);
		}

		public void Say(DialogueSayData p_sayData)
		{
			OnDialogueSay?.Invoke(p_sayData);
		}
	}
}
