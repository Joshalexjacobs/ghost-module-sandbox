using System.Collections;
using UnityEngine;

public class OneOffSprite : MonoBehaviour {

  public float deathTime = 1f;

  public bool spawnable = false;
  public GameObject spawnableObject;
  public float spawnOdds;

  protected virtual void Awake () {
    if (spawnable) {
      if (Random.Range (0, 100) <= spawnOdds) {
        Instantiate (spawnableObject, transform.position, Quaternion.identity);
      }
    }

    StartCoroutine (Die());
  }

  IEnumerator Die() {
    yield return new WaitForSeconds (deathTime);
    Destroy (gameObject);
  }
}