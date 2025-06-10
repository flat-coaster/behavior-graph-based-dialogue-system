using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;
using Unity.VisualScripting;

namespace FlatCoaster.Systems.DialogueSystem
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "SimpleSay", story: "[Actor] says [Sentence]", category: "Action", id: "7a0db8051dd5b9f860aa57a480fefa08")]
    public partial class SimpleSayAction : SayAction
    {
        [SerializeReference] public BlackboardVariable<DialogueActor> Actor;
        [SerializeReference] public BlackboardVariable<string> Sentence;

		protected DialogueSayData m_dialogueData;
		public override DialogueSayData DialogueData 
		{ 
			get
			{
				if (m_dialogueData == null)
				{
					m_dialogueData = new(this)
					{
						Actor = Actor.Value,
						Sentence = Sentence.Value,
					};
				}
				
				return m_dialogueData;
			}
		}
	}
}
