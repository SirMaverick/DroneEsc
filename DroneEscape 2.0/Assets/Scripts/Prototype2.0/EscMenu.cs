using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscMenu : MonoBehaviour {

    [SerializeField] Canvas escScreen;
    public GameObject currentPlayer;



    private static EscMenu _instance;

    public static EscMenu Instance { get { return _instance; } }


    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame();
        }
	}

    public void PauseGame() {
        escScreen.enabled = true;
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void UnpauseGame() {
        escScreen.enabled = false;
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
    } 

    public void QuitGame() {

    }
}
