using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace FlatCoaster.Systems.DialogueSystem.Sample
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_dialoguePanelObject;

        [SerializeField]
        private TextMeshProUGUI m_speakerTextObject;
        [SerializeField]
        private TextMeshProUGUI m_subtitleTextObject;

		[SerializeField]
		private GameObject m_optionsUIContainerPanelObject;
		[SerializeField]
		private Transform m_optionsUIContainer;
		[SerializeField]
		private Button m_optionButton;

        [SerializeField]
        private CanvasGroup m_canContinueIndicatorObject;

		[SerializeField]
		private float m_typingDelay = 0.25f;

        [SerializeField]
        private InputAction m_continueInput;

		private bool m_isTyping = false;
		private string m_fullSentence;
		private Coroutine m_typingCoroutine;
		private DialogueSayData m_currentDialogueSayData;

		private void OnEnable()
		{
			DialogueManager.OnDialogueStarted += OnDialogueStarted;
			DialogueManager.OnDialogueEnded += OnDialogueEnded;
			DialogueManager.OnDialogueSay += OnDialogueSay;
			DialogueManager.OnDialogueSayOption += OnDialogueSayOption;

			m_continueInput.performed += TryContinue;
			m_continueInput.Enable();
		}

		private void OnDisable()
		{
			DialogueManager.OnDialogueStarted -= OnDialogueStarted;
			DialogueManager.OnDialogueEnded -= OnDialogueEnded;
			DialogueManager.OnDialogueSay -= OnDialogueSay;
			DialogueManager.OnDialogueSayOption -= OnDialogueSayOption;

			m_continueInput.performed -= TryContinue;
			m_continueInput.Disable();
		}

		private void TryContinue(InputAction.CallbackContext obj)
		{
			if (m_currentDialogueSayData == null) return;
			if (m_isTyping)
			{
				ForceCompleteSentence();
				return;
			}

			m_currentDialogueSayData.Continue();
			m_currentDialogueSayData = null;
		}

		private void OnDialogueSayOption(DialogueSayOptionsData p_dialogueSayData)
		{
			if (m_optionsUIContainer.childCount > 0)
			{
				for (int i = 0; i < m_optionsUIContainer.childCount; i++)
				{
					Destroy(m_optionsUIContainer.GetChild(i).gameObject);
				}
			}

			var l_options = p_dialogueSayData.GetOptions();

			if (l_options != null && l_options.Count > 0)
			{
				m_optionsUIContainerPanelObject.SetActive(true);
				int t_indexFlag = 0;
                foreach (var l_option in l_options)
                {
					var l_optionButtonInstance =
						Instantiate(m_optionButton, m_optionsUIContainer);
					var l_optionIndex = t_indexFlag;
					l_optionButtonInstance.onClick.AddListener(() =>
					{
						p_dialogueSayData.SelectOption(l_optionIndex);
						m_optionsUIContainerPanelObject.SetActive(false);
					});
					
					l_optionButtonInstance.GetComponentInChildren<TextMeshProUGUI>().text = l_option;

					t_indexFlag++;
				}
			}
		}

		private void OnDialogueSay(DialogueSayData p_dialogueSayData)
		{
			m_currentDialogueSayData = p_dialogueSayData;
			m_speakerTextObject.text = p_dialogueSayData.Actor.Name;

			m_subtitleTextObject.text = string.Empty;

			m_typingCoroutine = StartCoroutine(CoTypeWriteSubtitle());
		}

		private void OnDialogueEnded(Unity.Behavior.BehaviorGraph p_graph)
		{
			m_currentDialogueSayData = null;
			m_dialoguePanelObject.SetActive(false);
		}

		private void OnDialogueStarted(Unity.Behavior.BehaviorGraph p_graph)
		{
			m_dialoguePanelObject.SetActive(true);
		}

		private void ForceCompleteSentence()
		{
			m_subtitleTextObject.text = m_fullSentence;
			m_isTyping = false;
			m_canContinueIndicatorObject.alpha = 1.0f;
			StopCoroutine(m_typingCoroutine);
		}

		private IEnumerator CoTypeWriteSubtitle()
		{
			if (m_currentDialogueSayData == null) yield break;
			
			m_canContinueIndicatorObject.alpha = 0f;
			m_fullSentence = m_currentDialogueSayData.Sentence;
			m_isTyping = true;

			int l_index = 0;
			while (m_isTyping)
			{
				m_subtitleTextObject.text += m_fullSentence[l_index];
				m_isTyping = m_subtitleTextObject.text.Length < m_fullSentence.Length;
				l_index++;

				yield return new WaitForSeconds(m_typingDelay);
			}

			m_canContinueIndicatorObject.alpha = 1.0f;
			m_typingCoroutine = null;
		}
	}
}
