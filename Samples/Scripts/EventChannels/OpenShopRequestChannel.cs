using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/OpenShopRequestChannel")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "OpenShopRequestChannel", message: "Player Request to Open Shop", category: "Events", id: "3eb617e8ac59fa48ee1de9716c18e769")]
public sealed partial class OpenShopRequestChannel : EventChannel { }

