using UnityEngine;

public class HomingLaser : Laser {
    private Vector3 targetPosition;
    private float speed = 1f;

	void Start () {
        targetPosition = ChooseTargetPosition();
	}
	
    Vector2 ChooseTargetPosition(){
        return new Vector3(1f, 1f, 10f);
    }

	void Update () {
        transform.LookAt(targetPosition, Vector3.up);
        Vector3 dirGlobal = transform.TransformDirection(Vector3.up);
        transform.Translate(dirGlobal * speed * Time.deltaTime);
	}
}
