using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningFlash : MonoBehaviour
{
    public float timeColorChangeComplete = 3.0f;
    float redValue = 1.0f;
    float blueValue = 1.0f;
    float greenValue = 1.0f;
    float transValue = 0.75f;
    SpriteRenderer spriteRender;
    public float time = 0.0f;
    
    enum ColorPhase{Red,ActualColor};
    ColorPhase colorPhase;

    // Start is called before the first frame update
    void Start()
    {
        colorPhase = ColorPhase.Red;
        spriteRender = this.gameObject.GetComponent<SpriteRenderer>();
        spriteRender.color = new Color(redValue,blueValue,greenValue,transValue);
        StartCoroutine(WarningFlashRenderChange());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WarningFlashRenderChange()
    {
        while(time < timeColorChangeComplete){
            if (colorPhase == ColorPhase.Red){
                blueValue -= 0.01f;
                greenValue -= 0.01f;
                spriteRender.color = new Color(redValue,blueValue,greenValue,transValue);

                if(blueValue <= 0.4f || greenValue <= 0.4f)
                    colorPhase = ColorPhase.ActualColor;
            }
            if (colorPhase == ColorPhase.ActualColor){
                blueValue += 0.01f;
                greenValue += 0.01f;
                spriteRender.color = new Color(redValue,blueValue,greenValue,transValue);

                if(blueValue >= 1.0f || greenValue >= 1.0f)
                    colorPhase = ColorPhase.Red;
            }



            time += 0.01f;
            yield return null;
        }
        
        Debug.Log("End of Color Change");
        spriteRender.color = new Color(1.0f,1.0f,1.0f,1.0f);
        //TODO Add that collider is back active
        //TODO Need to optimize to work for all gameobjects that are being loaded
        yield return null;
    }
}
