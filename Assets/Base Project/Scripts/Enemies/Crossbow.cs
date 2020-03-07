using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crossbow : Enemy  {
  
  public List<Transform> explosionPoints;
  public UbhShotCtrl ubhShotCtrl;

  public override void Explode() {
    StartCoroutine(EnemyExplosionManager.Explode(explosion, 
      explosionPoints.Select(transform => transform.position).ToList(), 0.15f));
    
    StartCoroutine(EnemyExplosionManager.Explode(explosion2, 
      explosionPoints.Select(transform => transform.position).ToList(), 0.05f));
  }

  public override void Destroy() {
    StartCoroutine(Flicker(15, 0.025f));
    
    shadow.SetActive(false);
    ubhShotCtrl.StopShotRoutineAndPlayingShot();

    ParticleSystem particleSystem = GetComponent<ParticleSystem>();

    if (particleSystem) {
      particleSystem.Stop();
    }
  }
  
}
