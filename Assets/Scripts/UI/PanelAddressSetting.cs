using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

namespace Assets.Scripts.UI
{
    public class PanelAddressSetting : PanelBase<PanelAddressSetting>, ISimuPanel
    {
        public InputField inputField_get;
        public InputField inputField_post;
        // Start is called before the first frame update
        void Start()
        {
            CVManager.Instance.addressNode.OnAddressConfigChange += SetPanelDown;
            CVManager.Instance.webRequesetServer.OnPostRequest += SetResultText;
            CVManager.Instance.webRequesetServer.OnGetRequest += SetResultText;
            inputField_get.onValueChanged.AddListener((string value) =>
            {
                CVManager.Instance.webRequesetServer.getAddress = value;
            });
            inputField_post.onValueChanged.AddListener((string value) =>
            {
                CVManager.Instance.webRequesetServer.postAddress = value;
            });
        }
        private void SetPanelDown(AddressConfig config)
        {
            inputField_get.text = config.getTrafficAddress.ToString();
            inputField_post.text = config.postTrafficAddress.ToString();
        }
        private void SetResultText(string content)
        {
            PanelNotice.Instance.SetNotice(content);
        }
    }
}
