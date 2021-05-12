using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{

    public class PanelNotice : PanelBase<PanelNotice>, ISimuPanel
    {
        public GameObject ErrorLog;
        public GameObject SuccessLog;
        public GameObject DialogLog;
        public Text text_notice;
        /// <summary>
        /// SetNotice
        /// </summary>
        /// <param name="content"></param>
        /// <param name="log"></param>
        public void SetNotice(string content ,int log= 0)
        {
            text_notice.text ="["+DateTime.Now.ToString()+"]  "+ content;
            SetLogImage(log);
        }

        private void SetLogImage(int log)
        {
            ErrorLog.SetActive(false);
            SuccessLog.SetActive(false);
            DialogLog.SetActive(false);
            switch (log)
            {
                case 0:
                    DialogLog.SetActive(true);
                    break;
                case 1:
                    SuccessLog.SetActive(true);
                    break;
                case 2:
                    ErrorLog.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }
}
