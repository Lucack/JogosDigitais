using UnityEngine;

public class Desativar : MonoBehaviour
{

    bool bouncing = false;

    void OnCollisionEnter(Collision player){
        if(player.gameObject.tag == "Player" && !bouncing){
            Invoke("SetInative", 7.0f);
            bouncing = true;
        }
    }

    void SetInative(){
        this.gameObject.SetActive(false);
        bouncing = false;
    }

}
