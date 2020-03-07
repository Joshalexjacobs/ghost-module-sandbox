using UnityEngine;

public class JavelinFighterSmall : Enemy {
  
  public GameObject exhaust;
  public UbhShotCtrl ubhShotCtrl;
  
  public override void Destroy() {
    EnemySpriteRenderer.enabled = false;
    shadow.SetActive(false);
    exhaust.SetActive(false);

    if (ubhShotCtrl) ubhShotCtrl.StopShotRoutineAndPlayingShot();

      ParticleSystem particleSystem = GetComponent<ParticleSystem>();

    if (particleSystem) {
      particleSystem.Stop();
    }
  }
  
}
