using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Anticrystal : Enemy  {
  
  public UbhShotCtrl ubhShotCtrl;
  public float preFireDelay = 0f;
  public float fireDelay = 0f;

  public int numberOfShots = 0;
  
  public List<Transform> explosion2Points;

  public override void OnBecameVisible() {
    base.OnBecameVisible();
    
    StartCoroutine(FireAfterDelay());
  }

  IEnumerator FireAfterDelay() {
    yield return new WaitForSeconds(preFireDelay);
    
    for (int i = 0; i < numberOfShots && !isDead; i++) {
      base.Animator.SetTrigger("isFiring");
      ubhShotCtrl.StartShotRoutine();
      
      yield return new WaitForSeconds(fireDelay);
    }
  }
  
  public override void Explode() {
    base.Explode();

    StartCoroutine(EnemyExplosionManager.Explode(explosion2, 
      explosion2Points.Select(transform => transform.position).ToList(), 0f));
  }

  public override void OnDestroy() {
    ubhShotCtrl.StopShotRoutineAndPlayingShot();
  }
}
