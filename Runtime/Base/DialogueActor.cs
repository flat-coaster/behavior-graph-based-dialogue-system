using UnityEngine;

namespace FlatCoaster.Systems.DialogueSystem
{
	[CreateAssetMenu(fileName = "Dialogue Actor", menuName = "Flat Coaster/Systems/Dialogue System/Actor", order = 0)]
	public class DialogueActor : ScriptableObject, IDialogueActor
	{
		[SerializeField]
		private string m_name;

		public string Name { get => m_name; }
	}
}
