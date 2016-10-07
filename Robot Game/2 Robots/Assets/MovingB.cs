using UnityEngine;
using System.Collections;

public class MovingB : MonoBehaviour {
	
	
	public float Speed = 4;
	public KeyCode Curser;
	public Cameratest Camera;
	public float horizontalSpeed = 2.0F;
	public float MyY=0;
	public float x;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		x += horizontalSpeed * Input.GetAxis("Mouse X");


        Quaternion target = Quaternion.Euler(0, x, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, 1f);
		if(Input.GetKey(Curser)){
			transform.Translate(Mathf.Sin(transform.rotation.x)*Time.deltaTime * Speed, 0, Mathf.Cos(transform.rotation.x)*Time.deltaTime * Speed);
		}
	}
}
