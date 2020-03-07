using UnityEngine;

public class PlaySoundOnAwake : MonoBehaviour {

  public string soundName;
  
  private AudioPool _audioPool;

  private void Awake() {
    _audioPool = FindObjectOfType<AudioPool>();
    
    if (_audioPool) _audioPool.PlaySound(soundName);
  }
}
