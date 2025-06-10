using Unity.Behavior;
using UnityEngine;

namespace FlatCoaster.Systems.DialogueSystem.Sample
{
    public class ShopUI : MonoBehaviour
    {
		[SerializeField]
		private GameObject m_shopPanel;

        [SerializeField]
        private OpenShopRequestChannel m_openShopRequestChannel;

		private void OnEnable()
		{
			m_openShopRequestChannel.AddListener(OpenShop);
		}

		private void OnDisable()
		{
			m_openShopRequestChannel.RemoveListener(OpenShop);
		}

		public void OpenShop()
		{
			m_shopPanel.SetActive(true);
		}
	}

	public delegate void OpenShopRequestChannelDelegate();
}
