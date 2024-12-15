using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    // Método para carregar a cena de forma normal
      public void LoadSceneSingle(string Scene)
        {
            SceneManager.LoadScene(Scene, LoadSceneMode.Single);
        }
    
 
        // Método para carregar a cena de forma aditiva
        public void LoadAdditionalScene(string Scene)
        {
            SceneManager.LoadScene(Scene, LoadSceneMode.Additive);
        }

        // Método para descarregar a cena adicional
        public void UnloadAdditionalScene(string Scene)
        {
            SceneManager.UnloadSceneAsync(Scene);
        }
   

}
