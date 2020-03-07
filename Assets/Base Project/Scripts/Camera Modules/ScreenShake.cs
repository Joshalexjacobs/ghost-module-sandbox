using UnityEngine;

public class ScreenShake : MonoBehaviour  {

  public bool isShaking = false;
  public float shakeMagnitude = 0.7f;
  public float dampingSpeed = 1.0f;

  private float shakeDuration = 0f;
  private Vector3 initialPosition;

  void OnEnable() {
    initialPosition = transform.localPosition;
  }

  void Update() {
    if (isShaking) {
      if (shakeDuration > 0) {
        transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

        shakeDuration -= Time.deltaTime * dampingSpeed;
      } else {
        shakeDuration = 0f;
        transform.localPosition = initialPosition;
        isShaking = false;
      }
    }
  }

  private void InitializeShake(float duration, float magnitude) {
    initialPosition = transform.localPosition;
    shakeDuration = duration;

    shakeMagnitude = magnitude;

    isShaking = true;
  }

  public void TriggerShake(float duration) {
    TriggerShake(duration, shakeMagnitude);
  }

  public void TriggerShake(float duration, float magnitude) {
    if (!CheckIfBiggerShakeOccuring(magnitude)) {
      InitializeShake(duration, magnitude);
    }
  }

  public void TriggerPriorityShake(float duration, float magnitude) {
    InitializeShake(duration, magnitude);
  }

  private bool CheckIfBiggerShakeOccuring(float magnitude) {
    return isShaking && shakeMagnitude > magnitude;
  }
}