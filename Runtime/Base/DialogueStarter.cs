using Unity.Behavior;
using Unity.VisualScripting;
using UnityEngine;

namespace FlatCoaster.Systems.DialogueSystem
{
    public class DialogueStarter : MonoBehaviour
    {
        public void StartDialogue(BehaviorGraph p_graph)
        {
            Singleton<DialogueManager>.instance.StartDialogue(p_graph);
        }
    }
}
