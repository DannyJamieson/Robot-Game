using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwitchControl : MonoBehaviour {
	
	//public var called target.
	public GameObject CurrentRobot;
	//private var target A is game object
	public GameObject targetA;
	//private var target B is game object
	public GameObject targetB;
	public System.Boolean Moving;
	private float MovingStartTime;
	private Vector3 CurrentPosition;
	private Vector3 TargetPosition;
	private Vector3 Difference;
	public int Timer;
	public KeyCode SwitchBots;
	private Vector3 DifferentVector;
	public float Angle;
	public KeyCode Curser;
	private CursorLockMode wantedMode;
	public MovingA BotAControl;
	public MovingB BotBControl;
	public Quaternion MovingCamRotate;
	public float y;
	public Quaternion Rotate;
	
	void Start () {
	Target(targetA);
	Moving = false;//if camer is moving
	wantedMode = CursorLockMode.Locked;
	BotBControl.enabled = false;	
	BotAControl.enabled = false;
	Moving=true;
	Timer=61;
	
    //transform.rotation = Quaternion.Slerp(transform.rotation, MovingCamRotate, 1f);
	}

	void Update () {
		Cursor.lockState = wantedMode;
		Cursor.visible = (CursorLockMode.Locked != wantedMode);
		
		if(Input.GetKey(Curser)){
				if(wantedMode == CursorLockMode.Locked){wantedMode = CursorLockMode.None; Debug.Log("NOT LOCKED");}
				else if(wantedMode == CursorLockMode.None){wantedMode = CursorLockMode.Locked;Debug.Log("LOCKED");}
		}

		if (Moving==false){
			float verticalSpeed = 2.0F;
			y += verticalSpeed * Input.GetAxis("Mouse Y");
			if(y>90){y=90;}
			if(y<-90){y=-90;}
			if(CurrentRobot==targetA){
				Rotate = Quaternion.Euler(-y,BotAControl.x,0);
			}
			else if(CurrentRobot==targetB){
				Rotate = Quaternion.Euler(-y,BotBControl.x,0);
			}
			transform.rotation = Quaternion.Slerp(transform.rotation, Rotate, 1f);
			transform.position= new Vector3(CurrentRobot.transform.position.x, transform.position.y, CurrentRobot.transform.position.z);
		}
		
		if(Input.GetKey(SwitchBots)&&Moving==false){
			
			Moving=true;
			if(CurrentRobot==targetA){
				
				MovingCamRotate= new Quaternion(targetB.transform.rotation.x,BotBControl.MyY,0,targetB.transform.rotation.w);
				transform.rotation = Quaternion.FromToRotation(transform.position, targetB.transform.position);
				BotAControl.MyY=y;
				BotAControl.enabled = false;
				Target(targetB);
			}
			else if(CurrentRobot==targetB){
				BotBControl.MyY=y;
				MovingCamRotate= new Quaternion(targetA.transform.rotation.x,BotAControl.MyY,0,targetB.transform.rotation.w);
				
				//transform.rotation = Quaternion.FromToRotation(transform.rotation, MovingCamRotate);
				BotBControl.enabled = false;
				BotBControl.MyY=y;
				Target(targetA);
			}
			Timer=61;
		}
		
		if(Timer==1){
			
			
			if(CurrentRobot==targetA){
				BotAControl.enabled = true;
				y=BotAControl.MyY;
			}
			else if(CurrentRobot==targetB){
				BotBControl.enabled = true;
				y=BotBControl.MyY;
			}
			Moving=false;
			//transform.parent = CurrentRobot.transform;
		}

		if(Timer>0){
			Timer-=1;
		}
		
		if(Moving==true){
			DifferentVector = CurrentPosition + (Difference*(Time.time-MovingStartTime));
			transform.position = DifferentVector;
			//transform.rotation = Quaternion.Slerp(transform.rotation, MovingCamRotate, 0);
			
		}
	}

	void Target(GameObject target){
		if(CurrentRobot!=null){transform.parent=null;}
		MovingStartTime=Time.time;
		CurrentPosition=transform.position;
		TargetPosition=target.transform.position;		
		Difference = TargetPosition-CurrentPosition;
		CurrentRobot=target;		
	}

}
