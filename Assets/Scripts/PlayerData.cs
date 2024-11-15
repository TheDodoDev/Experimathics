using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // Start is called before the first frame update
    private int highScore_AimematchicsI, highScore_AimematchicsII;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHighScore(int scene, int score)
    {
        if(scene == 2)
        {
            if(highScore_AimematchicsI < score)
            {
                highScore_AimematchicsI = score;
            }
        }
        if (scene == 3)
        {
            if (highScore_AimematchicsII < score)
            {
                highScore_AimematchicsII = score;
            }
        }
        Debug.Log("Aimemathics I High Score: " + highScore_AimematchicsI);
        Debug.Log("Aimemathics II High Score: " + highScore_AimematchicsII);
    }

    
}
