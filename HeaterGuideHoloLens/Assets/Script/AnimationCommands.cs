using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnimationCommands : MonoBehaviour
{
	//Animation
	private Animator anim;
	private AnimatorStateInfo stateInfo;
	//Prefabs
	public GameObject mObj_tampa;
	public GameObject mObj_tampa_base;
	public GameObject mObj_adaptador;

	//Animation Index Control
	bool active = true;

	//InitialPosition for "Reset"
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


	void OnSelect()
	{
		// If the sphere has no Rigidbody component, add one to enable physics.
		if (!this.GetComponent<Rigidbody>())
		{
			var rigidbody = this.gameObject.AddComponent<Rigidbody>();
			rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
		}
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
	
	void OnReset()
	{

		this.transform.localPosition = originalPosition;
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

