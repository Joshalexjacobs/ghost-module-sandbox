using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireLaser : MonoBehaviour {
  public LayerMask mask;
  public LayerMask playerMask;

  public Material fireMat;
  public Material preFireMat;

  public bool isFiring = false;

  [Header("Enabled on fire")] public List<ParticleSystem> particleSystems;

  private  bool damagePlayer = false;
  
  private bool isDead = false;
  private LineRenderer _lineRenderer;

  private void Awake() {
    _lineRenderer = GetComponent<LineRenderer>();
    _lineRenderer.material = preFireMat;

    DisableLineRenderer();
  }

  public IEnumerator ChargeAndShootLaser(float preFireWait, float fireWait, float postFireWait) {
    _lineRenderer.enabled = true;
    isFiring = true;

    StartCoroutine(IncreaseLaserSize(preFireWait / 10f));
    yield return new WaitForSeconds(preFireWait);

    EnableParticleSystems();
    damagePlayer = true;
    _lineRenderer.material = fireMat;

    yield return new WaitForSeconds(fireWait);

    damagePlayer = false;
    DisableParticleSystems();

    StartCoroutine(DecreaseLaserSize(postFireWait / 10f));
    yield return new WaitForSeconds(postFireWait);

    isFiring = false;
  }

  private void EnableParticleSystems() {
    if (!isDead) {
      foreach (ParticleSystem system in particleSystems) {
        system.Play();
      }
    }
  }

  private void DisableParticleSystems() {
    foreach (ParticleSystem system in particleSystems) {
      system.Stop();
    }
  }

  private IEnumerator IncreaseLaserSize(float waitTime) {
    for (int i = 0; i <= 10; i++) {
      AnimationCurve animationCurve = new AnimationCurve();
      animationCurve.AddKey(new Keyframe(0f, i * 0.1f));

      _lineRenderer.widthCurve = animationCurve;
      yield return new WaitForSeconds(waitTime);
    }
  }

  private IEnumerator DecreaseLaserSize(float waitTime) {
    for (int i = 10; i >= 0; i--) {
      AnimationCurve animationCurve = new AnimationCurve();
      animationCurve.AddKey(new Keyframe(0f, i * 0.1f));

      _lineRenderer.widthCurve = animationCurve;
      yield return new WaitForSeconds(waitTime);
    }
  }

  private void FixedUpdate() {
    if (isFiring && !isDead) {
      RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, Mathf.Infinity, mask);

      _lineRenderer.SetPosition(0, transform.position);
      _lineRenderer.SetPosition(1, hit.point);

      if (damagePlayer) {
        HandleCollision();
      } 
      
      _lineRenderer.enabled = true;
    }
    else if (isDead) {
      DisableLineRenderer();
    }
  }

  public void DisableLaser() {
    DisableLineRenderer();
    DisableParticleSystems();

    isFiring = false;
    isDead = true;
  }

  public void DisableLineRenderer() {
    _lineRenderer.enabled = false;
  }

  private void HandleCollision() {
    List<RaycastHit2D> playerCollisionList = new List<RaycastHit2D>();

    playerCollisionList.Add(Physics2D.Raycast(transform.position, -transform.up, Mathf.Infinity, playerMask));
    playerCollisionList.Add(Physics2D.Raycast(transform.position + new Vector3(-0.1f, 0f,0f), -transform.up, Mathf.Infinity, playerMask));
    playerCollisionList.Add(Physics2D.Raycast(transform.position + new Vector3(0.1f, 0f,0f), -transform.up, Mathf.Infinity, playerMask));

    Player foundPlayer = playerCollisionList
      .Select(hit => CheckForPlayerCollision(hit)).FirstOrDefault(player => player != null);

    if (foundPlayer != null) {
      foundPlayer.Damage();
    }
  }

  private Player CheckForPlayerCollision(RaycastHit2D hit) {
    if (hit && hit.collider && hit.collider.gameObject.tag == "Player") {
      return hit.collider.gameObject.GetComponent<Player>();
    }

    return null;
  }
  
}