using System;
using System.Collections;
using System.Collections.Generic;
using CandyCoded;
using UnityEngine;

public class SpawnOnDelay : MonoBehaviour {

  public float delay;
  
  public float waitTimeInbetweenSpawn;

  public bool randomizePosition = false;
  
  [SerializeField]
  [RangedSlider(-1, 1)]
  private RangedFloat randomX;
  
  [Header("Single Object")]
  public GameObject objectToSpawn;

  public int numberToSpawn = 1;

  [Header("Multiple Different Objects")]
  public List<GameObject> objectsToSpawn;

  private void Awake() {
    if (objectsToSpawn.Count > 0) {
      StartCoroutine(SpawnObjects());
    } else {
      StartCoroutine(SpawnObject());
    }
  }

  private Vector3 GetPosition() {
    return randomizePosition
      ? transform.position + new Vector3(randomX.Random(), 0f, 0f)
      : transform.position;
  }
  
  private IEnumerator SpawnObjects() {
    yield return new WaitForSeconds(delay);
    
    foreach (GameObject obj in objectsToSpawn) {
      Instantiate(obj, GetPosition(), Quaternion.identity);
      yield return new WaitForSeconds(waitTimeInbetweenSpawn);
    }
  }

  private IEnumerator SpawnObject() {
    yield return new WaitForSeconds(delay);

    for (int i = 0; i < numberToSpawn; i++) {
      Instantiate(objectToSpawn, GetPosition(), Quaternion.identity);
      yield return new WaitForSeconds(waitTimeInbetweenSpawn);
    }
  }
}
