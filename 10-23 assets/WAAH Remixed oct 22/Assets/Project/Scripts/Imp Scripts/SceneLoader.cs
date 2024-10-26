using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Core
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]private Image loadPercent;
        private Coroutine sceneCoroutine;
    
        public void LoadScene(string sceneName)
        {
            if(sceneCoroutine == null)
            {
                sceneCoroutine = StartCoroutine(LoadLevelAsync(sceneName));
            }
        }
        
        private IEnumerator LoadLevelAsync(string levelName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName);
    
            while(!asyncLoad.isDone)
            {
                loadPercent.fillAmount = asyncLoad.progress;
                yield return null;
            }
    
            sceneCoroutine = null;
        }
    }    
}

