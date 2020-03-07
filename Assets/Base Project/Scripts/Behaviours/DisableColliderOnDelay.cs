using System.Collections;
using UnityEngine;

public class DisableColliderOnDelay : MonoBehaviour {

  public float disableColliderAfter = 4f;
  
  private CircleCollider2D _circleCollider2D;
  private BoxCollider2D _boxCollider2D;
  
  private void Awake() {
    _circleCollider2D = GetComponent<CircleCollider2D>();
    _boxCollider2D = GetComponent<BoxCollider2D>();

    StartCoroutine(DisableCollider());
  }

  private IEnumerator DisableCollider() {
    yield return new WaitForSeconds(disableColliderAfter);

    if (_circleCollider2D) _circleCollider2D.enabled = false;
    if (_boxCollider2D) _boxCollider2D.enabled = false;
  }
  
}
