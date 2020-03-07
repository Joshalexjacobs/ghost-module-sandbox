using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour  {

  public IEnumerator Explode(OneOffSprite explosion, Vector3 position) {
    yield return StartCoroutine(Explode(explosion, 1, position, 0f));
  }
  
  public IEnumerator Explode(OneOffSprite explosion, int numberOfExplosions, Vector3 position, float waitTime) {
    for (int i = 0; i < numberOfExplosions; i++) {
      Instantiate(explosion, position, Quaternion.identity);
      yield return new WaitForSeconds(waitTime);
    }
  }

  public IEnumerator Explode(OneOffSprite explosion, List<Vector3> positions, float waitTime) {
    foreach (Vector3 position in positions) {
      yield return StartCoroutine(Explode(explosion, 1, position, waitTime));
    }
  }
  
}
