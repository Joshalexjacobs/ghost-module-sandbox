using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerUtil : MonoBehaviour  {

  public IEnumerator LoadSceneAfterXSeconds(float x) {
    yield return new WaitForSeconds(x);
    
    SceneManager.LoadScene("SampleScene");
  }

  public void ReloadScene() {
    SceneManager.LoadScene("SampleScene");
  }
  
}
