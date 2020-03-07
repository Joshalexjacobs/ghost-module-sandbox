using CandyCoded;
using UnityEngine;

public class RandomRotationOnAwake : MonoBehaviour  {

  [SerializeField]
  [RangedSlider(-180, 180)]
  private RangedFloat rotationRange;
  
  private void Awake() {
    transform.rotation = Quaternion.Euler(0f, 0f, rotationRange.Random());
  }
  
}
