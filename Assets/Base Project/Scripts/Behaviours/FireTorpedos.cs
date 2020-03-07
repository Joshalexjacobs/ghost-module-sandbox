using System.Collections;
using CandyCoded;
using UnityEngine;

public class FireTorpedos : MonoBehaviour  {
  
  public GameObject torpedo;
  public int torpedosToSpawn;
  public float torpedoSpawnDelay;

  [SerializeField]
  [RangedSlider(-1, 1)]
  public RangedFloat torpedoRandomX;
  
  [SerializeField]
  [RangedSlider(-1, 1)]
  public RangedFloat torpedoRandomY;
  
  public IEnumerator SpawnTorpedos(Enemy enemy) {
    yield return StartCoroutine(SpawnTorpedos(enemy, transform));
  }

  public IEnumerator SpawnTorpedos(Enemy enemy, Transform transform) {
    for (int i = 0; i < torpedosToSpawn && !enemy.isDead; i++) {
      Instantiate(torpedo, transform.position + new Vector3(torpedoRandomX.Random(), torpedoRandomY.Random(), 0f), Quaternion.identity);
      
      yield return new WaitForSeconds(torpedoSpawnDelay);
    }
  }

  public IEnumerator SpawnMultipleTorpedosAtOnce(Enemy enemy, params Transform[] transforms) {
    for (int i = 0; i < torpedosToSpawn && !enemy.isDead; i++) {
      foreach (Transform transform in transforms) {
        Instantiate(torpedo, transform.position + new Vector3(torpedoRandomX.Random(), torpedoRandomY.Random(), 0f), Quaternion.identity); 
      }

      yield return new WaitForSeconds(torpedoSpawnDelay);
    }
  }
  
}
