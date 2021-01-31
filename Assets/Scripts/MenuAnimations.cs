using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAnimations : MonoBehaviour
{
    public float speed = 2.0f;
    public GameObject circle1;
    public GameObject circle2;
    public GameObject circle3;

    public string sceneName;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        circle1.transform.Rotate(new Vector3(0,0,1), speed * Time.deltaTime * 3);
        circle2.transform.Rotate(new Vector3(0, 0, 1), speed * Time.deltaTime * 2 * -1);
        circle3.transform.Rotate(new Vector3(0, 0, 1), speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            gameObject.GetComponent<Animator>().SetBool("fade", true);
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
