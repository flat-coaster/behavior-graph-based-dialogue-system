using System;
using System.Collections.Generic;
using SystemAction = System.Action;
using Unity.Behavior;
using System.Diagnostics;

namespace FlatCoaster.Systems.DialogueSystem
{
    public static partial class GraphEventChannelExtensions
    {
        private static readonly Dictionary<EventChannel, Dictionary<SystemAction, Delegate>> m_registeredActions = new();

		#region Helpers
		private static void RegisterListener(EventChannel p_eventChannel, SystemAction p_action, BlackboardVariable[] p_vars)
        {
			Delegate l_actionToRegister = p_eventChannel.CreateEventHandler(p_vars, p_action);
			p_eventChannel.RegisterListener(l_actionToRegister);

			TryRecordListener(p_eventChannel, p_action, l_actionToRegister);
		}

        private static void UnRegisterListener(EventChannel p_eventChannel, SystemAction p_action)
		{
			p_eventChannel.UnregisterListener(m_registeredActions[p_eventChannel][p_action]);

			TryUnRecordListener(p_eventChannel, p_action);
		}

        private static bool HasEventAndActionRegistered(EventChannel p_eventChannel, SystemAction p_action)
		{
            return m_registeredActions.ContainsKey(p_eventChannel) &&
                m_registeredActions[p_eventChannel].ContainsKey(p_action);
		}

        private static bool HasEventAndActionNotRegistered(EventChannel p_eventChannel,SystemAction p_action)
		{
            return !m_registeredActions.ContainsKey(p_eventChannel) ||
                !m_registeredActions[p_eventChannel].ContainsKey(p_action);

		}

        private static void TryRecordListener(EventChannel p_eventChannel, SystemAction p_action, Delegate p_actionDelegate)
        {
			if (!m_registeredActions.ContainsKey(p_eventChannel))
			{
				m_registeredActions.Add(p_eventChannel, new());
			}

            if (m_registeredActions[p_eventChannel].ContainsKey(p_action))
            {
                m_registeredActions[p_eventChannel][p_action] = p_actionDelegate;
                return;
            }

			m_registeredActions[p_eventChannel].Add(p_action, p_actionDelegate);
		}

        private static void TryUnRecordListener(EventChannel p_eventChannel, SystemAction p_action)
        {
            if (HasEventAndActionNotRegistered(p_eventChannel, p_action)) return;

			m_registeredActions[p_eventChannel].Remove(p_action);

			if (m_registeredActions[p_eventChannel].Count == 0)
			{
				m_registeredActions.Remove(p_eventChannel);
			}
		}
		#endregion

		public static void AddListener(this EventChannel l_eventChannel, SystemAction p_action, BlackboardVariable[] p_vars = null)
        {
            if (HasEventAndActionRegistered(l_eventChannel, p_action)) return;

            RegisterListener(l_eventChannel, p_action, p_vars);
            // UnityEngine.Debug.Log($"Event Registered\nRegistered Event Count: {m_registeredActions[l_eventChannel].Count}");
        }

        public static void RemoveListener(this EventChannel l_eventChannel, SystemAction p_action)
        {
            if (HasEventAndActionNotRegistered(l_eventChannel, p_action)) return;

            UnRegisterListener(l_eventChannel, p_action);
			// UnityEngine.Debug.Log($"Event Un-Registered\nRegistered Event Count: {m_registeredActions.Count}\nRegistered Actions: {(m_registeredActions.Count > 0 ? m_registeredActions[l_eventChannel].Count : 0)}");
		}
	}
}
