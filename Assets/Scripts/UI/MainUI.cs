using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{

    public class MainUI : PanelBase<MainUI>, ISimuPanel
    {
        public bool isMouseOnUI;
        private void Start()
        {
            Application.targetFrameRate = 60;
        }
        private void Update()
        {
            isMouseOnUI = EventSystem.current.IsPointerOverGameObject();
        }

        private List<ISimuPanel> panels = new List<ISimuPanel>();
        public void CloseLastPanel()
        {
            if (panels.Count > 0)
            {
                ISimuPanel simuPanel = panels[panels.Count - 1];
                simuPanel.SetPanelActive(false);
                panels.Remove(simuPanel);
            }
        }
        public void AddPanel(ISimuPanel simuPanel)
        {
            panels.Add(simuPanel);
        }
        public void RemovePanel(ISimuPanel simuPanel)
        {
            if (panels.Contains(simuPanel)) panels.Remove(simuPanel);
        }
    }

}
