using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Text gamemodeText;

    private MonoBehaviour[] scripts;
    private List<MonoBehaviour> builderScripts = new List<MonoBehaviour>();
    private List<MonoBehaviour> playerScripts = new List<MonoBehaviour>();
    private BlockPlacer blockPlacerScript;
    private bool builderEnabled;

    private GameObject egg;


    // Seperate player and builder scripts; Make sure builder is fully enabled
    void Start () {
        scripts = player.GetComponents<MonoBehaviour>();
        for (int i=0; i<3; i++)
        {
            builderScripts.Add(scripts[i]);
            scripts[i].enabled = true;
            playerScripts.Add(scripts[i + 3]);
            scripts[i + 3].enabled = false;
        }
        blockPlacerScript = (BlockPlacer)builderScripts[2];

        player.GetComponent<Rigidbody>().isKinematic = true;
        builderEnabled = true;
    }
	
	void Update () {
		
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleGamemode();
        }
        if (egg != null)
        {
            CheckForEggHit();
        }
    }

    // Toggle gamemode (if builderEnabled = true, turn builder scripts off, turn playerScripts on)
    private void ToggleGamemode() 
    {
        for (int i = 0; i < 3; i++) //Trickery to only need one loop here (even though I still do the if statement below for changing text)
        {  
            builderScripts[i].enabled = !builderEnabled;
            playerScripts[i].enabled = builderEnabled;
            player.GetComponent<Rigidbody>().isKinematic = !builderEnabled;
        }
        if (builderEnabled)
        {
            gamemodeText.text = "Player";
            blockPlacerScript.RemoveBlockHighlight();
            if (blockPlacerScript.GetEgg() != null)
            {
                egg = blockPlacerScript.GetEgg();
            }
        }
        else
        {
            gamemodeText.text = "Builder";

        }

        builderEnabled = !builderEnabled;

    }

    private void CheckForEggHit()
    {
        if (egg.GetComponent<Rigidbody>().velocity.magnitude != 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        gamemodeText.text = "YOU WIN";
    }

}
