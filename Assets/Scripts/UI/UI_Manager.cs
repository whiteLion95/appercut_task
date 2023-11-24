using PajamaNinja.Common;
using System.Collections.Generic;
using UnityEngine;

namespace PajamaNinja.UI
{
    public class UI_Manager : Singleton<UI_Manager>
    {
        [SerializeField] private List<UI_Screen> screens;
        [SerializeField] private UI_Screen defaultScreen;

        void Start()
        {
            InitScreens();
        }

        private void InitScreens()
        {
            foreach (var screen in screens)
            {
                if (!screen.Equals(defaultScreen))
                {
                    screen.Hide(true);
                }
                else
                {
                    screen.Show(true);
                }
            }
        }

        public void ShowScreen(UI_Screen.Name screenName)
        {
            UI_Screen screen = screens.Find((s) => s.ScreenName == screenName);

            if (screen != null)
            {
                screen.Show();
            }
        }
    }
}