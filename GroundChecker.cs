//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GroundChecker : MonoBehaviour
//{
//    public Transform playerTransform;
//    CharacterController player;
//    Animator playerAnim;

//    void Awake(){
//        player = playerTransform.gameObject.GetComponent<CharacterController>();
//        playerAnim = playerTransform.gameObject.GetComponent<Animator>();
//    }

//    void FixedUpdate(){
//        //by default
//        //player.isGrounded = false; //idk if it should be here or main script
//    }

//    void OnTriggerStay2D(Collider2D other){
//        //if its a platform check
//        //player.isGrounded = true;
//        if (other.transform.gameObject.tag == "Ground")
//        {
//            playerAnim.SetBool("isGrounded", true);
//            player.isGrounded = true;
//        }

//    }

//    void OnTriggerExit2D(Collider2D other){
//        //if platform
//        // player.isGrounded = false; //or maybe on jump idk //yeah it does that on jump too
//        if (other.transform.gameObject.tag == "Ground")
//        {
//            playerAnim.SetBool("isGrounded", false);
//            player.isGrounded = false;
//        }

//    }
//}
