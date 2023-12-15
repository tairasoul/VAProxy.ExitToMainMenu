using BepInEx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ExitToMainMenuInsteadOfDesktop
{
    [BepInPlugin("exit.mainmenu.insteadofdesktop", "ExitToMainMenu", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        void Awake()
        {
            Logger.LogInfo("exit awake");
            SceneManager.sceneLoaded += (Scene ne, LoadSceneMode mode) =>
            {
                if (ne.name != "Intro" && ne.name != "Menu")
                {
                    while (GameObject.Find("MAINMENU/Canvas/Pages/Setting/Content/GameObject/exit") == null)
                    {
                        Logger.LogInfo("waiting for exit");
                        System.Threading.Thread.Sleep(100);
                    }
                    Logger.LogInfo("replacing listener");
                    Button exit = GameObject.Find("MAINMENU/Canvas/Pages/Setting/Content/GameObject/exit").GetComponent<Button>();
                    exit.onClick.RemoveAllListeners();
                    exit.onClick = new Button.ButtonClickedEvent();
                    exit.onClick.AddListener(() =>
                    {
                        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
                    });
                }
            };
        }
    }
}
