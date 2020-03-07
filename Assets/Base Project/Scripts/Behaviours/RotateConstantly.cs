using UnityEngine;

public class RotateConstantly : MonoBehaviour {

    public enum RotationDirections {
        RIGHT = -1,
        LEFT =  1
    }

    public RotationDirections rotationDirection = RotationDirections.RIGHT;
    public float rotateSpeed = 0f;

    public bool isRotating = true;
    public bool randomizeRotationSpeed = true;
    public bool randomizeRotationDirection = true;

    private void Start() {
        if (randomizeRotationSpeed) {
            rotateSpeed = Random.Range(0, 180f);
        }

        if (randomizeRotationDirection) {
            rotationDirection = Random.Range(0, 2) == 0 ? RotationDirections.LEFT : RotationDirections.RIGHT;
        }
    }

    void Update() {
        if (isRotating) {
            transform.Rotate(0, 0, (int)rotationDirection * rotateSpeed * Time.deltaTime);
        }
    }
}
