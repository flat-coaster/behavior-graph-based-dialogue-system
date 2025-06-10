using System;
using Unity.Behavior;
using UnityEngine;
using Join = Unity.Behavior.Join;
using Unity.Properties;
using FlatCoaster.Systems.DialogueSystem;
using Unity.VisualScripting;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "EndDialogue", category: "Flow/Parallel Execution", id: "691df139e62e7a008e5963e72f4ca075")]
public partial class EndDialogueSequence : Join
{
    protected override Status OnStart()
    {
		Singleton<DialogueManager>.instance.EndActiveDialogue();
		return Status.Success;
	}
}

