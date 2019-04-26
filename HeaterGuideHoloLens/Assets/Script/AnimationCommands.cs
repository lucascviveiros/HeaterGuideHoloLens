using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnimationCommands : MonoBehaviour
{
	// Animator and animation clip info
	private Animator anim;
	private AnimatorStateInfo stateInfo;

	//3D Objects of the faucets
	public GameObject mObj_tampa;
	public GameObject mObj_tampa_base;
	public GameObject mObj_adaptador;

	// Animation bool expression for control
	private bool active = true;

	// InitialPosition of the object for Reset command
	Vector3 originalPosition;

	void Start()
	{
		originalPosition = this.transform.localPosition;
		mObj_tampa_base.GetComponent<Renderer>().enabled = true;
		mObj_tampa_base.SetActive(true);
		mObj_adaptador.GetComponent<Renderer>().enabled = true;
		mObj_adaptador.SetActive(true);
		anim = this.GetComponent<Animator>();
	}

	void OnNext()
	{

		stateInfo = anim.GetCurrentAnimatorStateInfo(0);

		if (stateInfo.IsName("animation_1_tampa"))
		{
			mObj_tampa.SetActive(false);
			mObj_tampa.GetComponent<Renderer>().enabled = false;
			mObj_adaptador.SetActive(true);
			mObj_adaptador.GetComponent<Renderer>().enabled = true;
			mObj_tampa_base.SetActive(true);
			mObj_tampa_base.GetComponent<Renderer>().enabled = true;
			GetComponent<Animator>().Play("animation_2_tampa");
			
		}
		
		else if (stateInfo.IsName("animation_2_tampa"))
		{
			mObj_tampa.SetActive(false);
			mObj_tampa.GetComponent<Renderer>().enabled = false;
			mObj_tampa_base.SetActive(true);
			mObj_tampa_base.GetComponent<Renderer>().enabled = true;
			GetComponent<Animator>().Play("animation_3_tampa");
			
		}
		
		else if (stateInfo.IsName("animation_2_tampa"))
		{
			mObj_tampa.SetActive(false);
			mObj_tampa.GetComponent<Renderer>().enabled = false;
			GetComponent<Animator>().Play("animation_3_tampa");
			
		}
		
		else if (stateInfo.IsName("animation_3_tampa"))
		{
			mObj_tampa_base.SetActive(false);
			mObj_tampa_base.GetComponent<Renderer>().enabled = false;
			mObj_tampa.SetActive(false);
			mObj_tampa.GetComponent<Renderer>().enabled = false;
			active = false;
			
		}
	}

	void OnBack()
	{
		stateInfo = anim.GetCurrentAnimatorStateInfo(0);

		if (active == false)
		{
			mObj_tampa.SetActive(false);
			mObj_tampa.GetComponent<Renderer>().enabled = false;
			mObj_tampa_base.SetActive(true);
			mObj_tampa_base.GetComponent<Renderer>().enabled = true;
			GetComponent<Animator>().Play("animation_3_tampa");
			active = true;
			
		}
		
		else if (stateInfo.IsName("animation_3_tampa"))
		{
			mObj_tampa.SetActive(false);
			mObj_tampa.GetComponent<Renderer>().enabled = false;
			mObj_tampa_base.SetActive(true);
			mObj_tampa_base.GetComponent<Renderer>().enabled = true;
			GetComponent<Animator>().Play("animation_2_tampa");
			
		}
		
		else if (stateInfo.IsName("animation_2_tampa"))
		{
			mObj_tampa.SetActive(true);
			mObj_tampa.GetComponent<Renderer>().enabled = true;
			mObj_tampa_base.SetActive(true);
			mObj_tampa_base.GetComponent<Renderer>().enabled = true;
			GetComponent<Animator>().Play("animation_1_tampa");
			
		}

	}
	
	// Methods linked to the SpeechManager class 
	void OnReset()
	{
		this.transform.localPosition = originalPosition;
	}

	void OnRotateRight() 
	{
		this.transform.Rotate(0, 30, 0);
	}

	void OnRotateLeft()
	{
		this.transform.Rotate(0, -30, 0);
	}

	void OnPlay()
	{
		Time.timeScale = 1;
	}

	void OnPause()
	{
		Time.timeScale = 0;
	}

	void OnBigger() 
	{
		this.transform.localScale += new Vector3(0.1F, 0.1F, 0.1F);
	}

	void OnSmaller() 
	{
		this.transform.localScale += new Vector3(-0.1F, -0.1F, -0.1F);
	}

	void OnExit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
	}
}

