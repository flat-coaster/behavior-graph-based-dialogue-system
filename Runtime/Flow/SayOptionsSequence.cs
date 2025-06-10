using System;
using Unity.Behavior;
using UnityEngine;
using Composite = Unity.Behavior.Composite;
using Unity.Properties;
using FlatCoaster.Systems.DialogueSystem;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SayOptions", story: "Say one of the [options]", category: "Flow", id: "8373454e90e2f826e12007274af2a566")]
public partial class SayOptionsSequence : Composite, IDialogueOptionable
{
    [SerializeReference] public BlackboardVariable<List<string>> options;

	protected int m_selectedIndex = -1;

	protected override Status OnStart()
    {
        if (Children == null || Children.Count == 0)
        {
            return Status.Success;
        }

        //foreach (var child in Children)
        //{
        //    Debug.Log($"Child type: {((Node)child).GetType().Name}");
        //}

		Singleton<DialogueManager>.instance.SetOptions(new(this));
        return Status.Success;
    }

	public void Continue(int p_optionIndex)
    {
        m_selectedIndex = p_optionIndex;

		if (m_selectedIndex >= 0 && m_selectedIndex < Children.Count)
		{
			StartNode(Children[m_selectedIndex]);
			CurrentStatus = Status.Success;
		}
	}

    public List<string> GetOptions()
    {
        if (options == null || options.Value.Count == 0)
        {
            return null;
        }

        return options.Value;
    }
}

