using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainmenuNav : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject credits;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            LoadLevel(1);
        }
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            credits.SetActive(!credits.activeInHierarchy);
        }
        if(Input.GetKeyDown(KeyCode.Mouse2))   
        {
            Application.Quit();
        }
    }

    void LoadLevel(int i){
        SceneManager.LoadScene(i);
    }
}
