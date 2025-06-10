using FlatCoaster.Systems.DialogueSystem;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "SayOption", story: "[Actor] Choose To Say [Sentence]", category: "Conditions", id: "2ab4c2785d17897e505e7cc1368fb2e8")]
public partial class SayOptionCondition : Condition
{
    [SerializeReference] public BlackboardVariable<DialogueActor> Actor;
    [SerializeReference] public BlackboardVariable<string> Sentence;

    public override bool IsTrue()
    {
        return true;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
