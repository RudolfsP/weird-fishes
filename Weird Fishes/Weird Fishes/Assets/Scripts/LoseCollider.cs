using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        //usually avoid using string references in code because they are hardcoded
        SceneManager.LoadScene("Game Over");
    }
}
