using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JavelinFighterBig : Enemy {

  public List<Transform> explosionPoints; 
  public UbhShotCtrl ubhShotCtrl;
  
  public override void Explode() {
    StartCoroutine(EnemyExplosionManager.Explode(explosion, 
      explosionPoints.Select(transform => transform.position).ToList(), 0.1f));
  }
  
  public override void Destroy() {
    EnemySpriteRenderer.enabled = false;
    shadow.SetActive(false);
    ubhShotCtrl.StopShotRoutineAndPlayingShot();

    ParticleSystem particleSystem = GetComponent<ParticleSystem>();

    if (particleSystem) {
      particleSystem.Stop();
    }
  }
  
}