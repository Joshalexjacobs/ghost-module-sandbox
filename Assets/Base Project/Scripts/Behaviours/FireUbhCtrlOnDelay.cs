using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class FireUbhCtrlOnDelay : MonoBehaviour {

  public UbhShotCtrl ubhShotCtrl;
  public float delay = 0f;

  public bool destroyWhenDone = false;
  
  private void Awake() {
    StartCoroutine(FireAfterDelay());
  }

  IEnumerator FireAfterDelay() {
    yield return new WaitForSeconds(delay);
    ubhShotCtrl.StartShotRoutine();

    if (destroyWhenDone) {
      yield return new WaitUntil(() => {
        return !ubhShotCtrl.shooting;
      });
      
      Destroy(gameObject);
    }
  }

  void OnDestroy() {
    ubhShotCtrl.StopShotRoutine();
  }
}
