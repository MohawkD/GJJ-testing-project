 using UnityEngine;
 using UnityEngine.UI;
 using System.Collections;

public class DisableObject : MonoBehaviour
{  
   // public bool ScriptOn;
    void Start ()
    {
        this.gameObject.GetComponent<Image>().enabled = false;
    }
	
	void Update () 
	{
		if (this.enabled == true ) 
		{
          
	      this.enabled = false;
		  this.gameObject.GetComponent<Image>().enabled = true;
		}
		
		else
	    {
			this.gameObject.GetComponent<Image>().enabled = false;
		}
		
	}
}