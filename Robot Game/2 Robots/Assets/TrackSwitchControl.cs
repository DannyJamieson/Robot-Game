using UnityEngine;
using System.Collections;

public class TrackSwitchControl : MonoBehaviour {
	
	public KeyCode Forward;
	public KeyCode Backwards;
	public MovingA TankMovement;
	public System.Boolean Using;
	public int Counter;
	public TrackSwitchControl ThisScript;

	void Start () {
	Using=false;
	}
	
	void Update () {
		if(Input.GetKey(Forward)==false && Counter>0 && Counter<200){
			Counter-=2;	
		}
		if(Using==true){
			if(Input.GetKey(Backwards)){
				TankMovement.enabled=true;
				Using=false;
				//Lerp away
			}
			if(Input.GetKey(Forward)&& Counter<200){
				Counter+=1;
			}
		if(Counter>=200){
			Using=false;
			TankMovement.enabled=true;
			ThisScript.enabled=false;
		}
	}
	
	
	
	
	}
		
		void OnTriggerEnter(Collider colInfo){
		if(colInfo.gameObject.tag =="Player" && Counter<200){
			TankMovement.enabled=false;
			Using=true;
			//lerp and slerp
			//wait 1 second
			//Using=true
		}
	}
	
	
}
