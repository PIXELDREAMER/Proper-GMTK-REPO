using System;
using UnityEngine;
using UnityEngine.UI;
using Game.Core;

namespace Game.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField]private SquashStretchButton playButton;
        [SerializeField]private SquashStretchButton creditsBtn;
        
        [SerializeField]private SquashStretchButton creditsBackToMainBtn;
        [SerializeField]private SquashStretchButton exitBtn;

        [SerializeField]private Canvas mainMenu;
        [SerializeField]private Canvas creditsMenu;
        [SerializeField]private Canvas loadCanvas;

        [SerializeField]private SceneLoader sceneLoader;
        [SerializeField]private string firstLevelName;

        
        private void LoadFirstLevel()
        {
            mainMenu.gameObject.SetActive(false);
            creditsMenu.gameObject.SetActive(false);
            loadCanvas.gameObject.SetActive(true);
            sceneLoader.LoadScene(firstLevelName);
        }

        private void Exit()
        {
            Application.Quit();
        }

        private void OpenCredits()
        {
            mainMenu.gameObject.SetActive(false);
            creditsMenu.gameObject.SetActive(true);
        }

        private void CloseCredits()
        {
            mainMenu.gameObject.SetActive(true);
            creditsMenu.gameObject.SetActive(false);
        }


        private void Start() 
        {
            mainMenu.gameObject.SetActive(true);
            creditsMenu.gameObject.SetActive(false);
            loadCanvas.gameObject.SetActive(false);
            
            playButton.GetComponent<SquashStretchEffect>().OnEndEffect.AddListener(LoadFirstLevel);
            creditsBtn.GetComponent<SquashStretchEffect>().OnEndEffect.AddListener(OpenCredits);
            creditsBackToMainBtn.GetComponent<SquashStretchEffect>().OnEndEffect.AddListener(CloseCredits);
            exitBtn.GetComponent<SquashStretchEffect>().OnEndEffect.AddListener(Exit);    
        }
    }
}
