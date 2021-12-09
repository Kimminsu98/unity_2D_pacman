using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseEvent : MonoBehaviour
{   
   [SerializeField]
   Text startPauseText;
   bool pauseActive = false;

   public void pauseBtn(){
       if( pauseActive ){
           Time.timeScale = 1;
           pauseActive = false;
       }
       else{
           Time.timeScale = 0;
           pauseActive = true;
       }
   }
}
