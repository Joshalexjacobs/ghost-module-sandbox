
public class LandingCharger : Enemy {

  public UbhShotCtrl ubhShotCtrl;

  public override void OnBecameVisible() {
    ubhShotCtrl.enabled = true;
  }
  
  public override void OnBecameInvisible() {
    ubhShotCtrl.StopShotRoutineAndPlayingShot();
  }
}
