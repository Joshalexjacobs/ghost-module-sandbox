using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class Crystal : Enemy {

  public FireLaser fireLaser;
  public float fireDelay;
  
  public List<Transform> explosion2Points;
  
  public override void OnBecameVisible() {
    base.OnBecameVisible();
    
    if (fireLaser) {
      StartCoroutine(Fire());
    }
  }

  IEnumerator Fire() {
    yield return new WaitForSeconds(fireDelay);
    
    Animator.SetTrigger("isCharging");
    StartCoroutine(fireLaser.ChargeAndShootLaser(1f, 0.2f, 0.5f));
  }

  public override void Explode() {
    base.Explode();

    StartCoroutine(EnemyExplosionManager.Explode(explosion2, 
      explosion2Points.Select(transform => transform.position).ToList(), 0f));
  }

  public override void Destroy() {
    fireLaser.DisableLaser();

    CircleCollider2D.enabled = false;
    EnemySpriteRenderer.enabled = false;
  }

}
