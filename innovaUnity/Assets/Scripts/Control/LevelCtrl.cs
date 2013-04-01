using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelCtrl : MonoBehaviour {
	
	private static LevelCtrl levelCtrl;
	public static LevelCtrl Instance{
		get{
			if(levelCtrl.Equals(null)){
				levelCtrl = new LevelCtrl();
			}
			return levelCtrl;
		}
	}
		
	public float sceneryLength;
	public float sceneryHeight;
	
	public float skyLength;
	
	public float gameSpeed;
	public float maxGameSpeed;
	public GameObject objectGenerator;
	
	public AudioClip introLoopPrincipal;
	public AudioClip loopPrincipal;
	public AudioClip audioGanar;
	public AudioClip introLoopSegway;
	public AudioClip LoopSegway;
	public AudioClip audioFail;

    public GameObject loseScreen;
    public bool fadeLose = false;
    public bool won = false;
    public float fadeDuration = 5.0f;
    public float elapsedFadeTime = 0.0f;
	private float timeElapsed;
	
	void Awake(){
		levelCtrl = this;
	}
	
	void Start(){
		objectGenerator = Instantiate(objectGenerator, new Vector3(transform.position.x + sceneryLength, objectGenerator.transform.position.y, 
			objectGenerator.transform.position.z), transform.rotation) as GameObject;
		
		PlayLoopPrincipal();
	}
	
	void Update(){		
		
		if (!audio.isPlaying){
			if (audio.clip==introLoopPrincipal){
				audio.clip=loopPrincipal;
				audio.Play();
				audio.loop=true;
			}
			if (audio.clip==introLoopSegway){
				audio.clip=LoopSegway;
				audio.Play();
				audio.loop=true;
			}
		}
        if(fadeLose){
            loseScreen.renderer.material.color = new Color(loseScreen.renderer.material.color.r, loseScreen.renderer.material.color.g, loseScreen.renderer.material.color.b, Mathf.Clamp(Mathf.Lerp(256, 0, elapsedFadeTime /  fadeDuration),0,256));
            elapsedFadeTime+=Time.deltaTime;
        }
	}

    public void WinGame()
    {
        PlayWin();
		Destroy(this.objectGenerator);
		setSpeedToZero();
		getScore();
    }
	
    public void LoseGame() {
        PlayFail();
		setSpeedToZero();
        if(loseScreen != null){
            loseScreen = (GameObject)GameObject.Instantiate(loseScreen, camera.transform.position + Vector3.forward * 10, Quaternion.identity);
        }       
    }
	
//	void OnGUI () {
//		
//	}
	
	public void PlayLoopPrincipal (){
		audio.loop=false;
		audio.Stop();
		audio.clip = introLoopPrincipal;
		audio.Play();
	}
	
	public void PlaySegway(){
		audio.loop=false;
		audio.Stop();
		audio.clip = introLoopSegway;
		audio.Play();
	}
	
	
//	public IEnumerator PlayWin(){
//		if (audio.clip != audioGanar) {
//			audio.loop=false;
//			audio.Stop();
//			audio.clip = audioGanar;
//			audio.Play();
//		}
//        yield return new WaitForSeconds(audio.clip.length);
//	}
//	
//	public IEnumerator PlayFail() {
//		audio.loop=false;
//		audio.clip = audioFail;
//		audio.Play();
//        yield return new WaitForSeconds(audio.clip.length);
//	}
	
	public void PlayWin(){
		if (audio.clip != audioGanar) {
			audio.loop=false;
			audio.Stop();
			audio.clip = audioGanar;
			audio.Play();
		}
	}
	public void PlayFail() {
		audio.loop=false;
		audio.clip = audioFail;
		audio.Play();
	}
	
	public void setSpeedToZero(){
		GameObject.FindGameObjectWithTag("Auditorium").GetComponent<Scenery>().speed = 0f;
		GameObject[] skies = GameObject.FindGameObjectsWithTag("Sky");
		for(int i = 0; i < skies.Length; i++){
			skies[i].GetComponent<SkyMovement>().speed = 0;
		}
	}
	
	private void getScore(){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		float playerScore = player.GetComponent<Player>().getScore();

		Debug.Log(playerScore);
	}
}