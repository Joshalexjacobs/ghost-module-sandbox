using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossExplosion : MonoBehaviour {

  public float delay = 0f;

  public int waves = 1;
  public float waveWaitTime = 0f;
  
  public OneOffSprite explosion1;
  public OneOffSprite explosion2;

  public GameObject backgroundExplosionsParent;

  private ExplosionManager _explosionManager;
  
  private void Awake() {
    _explosionManager = FindObjectOfType<ExplosionManager>();
    StartCoroutine(TriggerBackgroundExplosions());
  }

  public IEnumerator TriggerBackgroundExplosions() {
    yield return new WaitForSeconds(delay);
    
    List<Transform> explosionPoints = backgroundExplosionsParent.GetComponentsInChildren<Transform>().ToList();
    
    explosionPoints.RemoveAt(0);

    for (int i = 0; i < waves; i++) {
      StartCoroutine(_explosionManager.Explode(explosion2, 
        explosionPoints.Select(transform => transform.position).ToList(), 0.005f));
      
      yield return new WaitForSeconds(waveWaitTime);
    }
    
    yield return new WaitForSeconds(2);
    Destroy(gameObject);
  }
  
}
