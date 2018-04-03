using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

	// Use this for initialization

	private Camera cam;
	public GameObject gameManager;

	void Start () {
		
		cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();

	}

	void OnMouseDown () {
		GameObject.Destroy (this.gameObject);

	}

	// Update is called once per frame
	void Update () {
		Vector3 viewPos = cam.WorldToViewportPoint (this.transform.position);
		if (viewPos.x < 0f || viewPos.y < 0f || viewPos.x > 1f || viewPos.y > 1f) {
			GameObject.Destroy (this.gameObject);
			gameManager.GetComponent <GameManager> ().atualizarVida (-1);

		}

	}
}
