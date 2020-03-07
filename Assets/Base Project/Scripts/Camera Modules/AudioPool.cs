using CandyCoded;
using UnityEngine;

public class AudioPool : MonoBehaviour {

  public AudioPoolReference audioPoolReference;
  
  private void Awake()  {
    audioPoolReference.Populate();
    DontDestroyOnLoad(gameObject);
  }

  public void PlaySound(string soundName) {
    audioPoolReference.Play(soundName);
  }
  
  public void ReleaseAll() {
    StopAllCoroutines();
    audioPoolReference.ReleaseAllObjects();
  }
  
}
