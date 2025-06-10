using Unity.Behavior;
using UnityEngine;

namespace FlatCoaster.Systems.DialogueSystem
{
	public delegate void DialogueStartedEvent(BehaviorGraph p_graph);
	public delegate void DialogueEndedEvent(BehaviorGraph p_graph);
	public delegate void DialogueSayEvent(DialogueSayData p_dialogueSayData);
	public delegate void DialogueSayOptionEvent(DialogueSayOptionsData p_dialogueSayData);
}
