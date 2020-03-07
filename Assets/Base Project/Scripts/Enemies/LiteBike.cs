using UnityEngine;

public class LiteBike : Enemy {
  
  private SpawnOnDelay _spawnOnDelay;
  
  public override void OnInit() {
    base.OnInit();
    _spawnOnDelay = GetComponent<SpawnOnDelay>();
  }

  public override void Destroy() {
    EnemySpriteRenderer.enabled = false;
    _spawnOnDelay.StopAllCoroutines();
    
    ParticleSystem particleSystem = GetComponent<ParticleSystem>();

    if (particleSystem) {
      particleSystem.Stop();
    }
  }
  
}
