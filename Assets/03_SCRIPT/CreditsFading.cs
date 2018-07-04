using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsFading : MonoBehaviour {
    public Canvas firstCanvas;
    public Canvas secondCanvas;
    public Canvas thirdCanvas;

    private float timeStart;

    private float firstCanvasApparitionDelay = 2;
    private float firstCanvasDisparitionDelay = 6;
    private float secondCanvasApparitionDelay = 8;
    private float secondCanvasDisparitionDelay = 12;
    private float thirdCanvasApparitionDelay = 14;
    private float thirdCanvasDisparitionDelay = 18;
    private float nextSceneDelay = 20;
    
    void Start () {
        timeStart = Time.time;
	}
	
	void Update ()
    {
        float firstCanvasOpacity = (Time.time - timeStart - 0) / (firstCanvasApparitionDelay - 0);
        if (firstCanvasOpacity >= 0 && firstCanvasOpacity <= 1)
            firstCanvas.GetComponent<CanvasGroup>().alpha = firstCanvasOpacity;

        float firstCanvasOpacityWhenDisappearing = 1 - (Time.time - timeStart - firstCanvasDisparitionDelay) / (secondCanvasApparitionDelay - firstCanvasDisparitionDelay);
        if (firstCanvasOpacityWhenDisappearing >= 0 && firstCanvasOpacityWhenDisappearing <= 1)
            firstCanvas.GetComponent<CanvasGroup>().alpha = firstCanvasOpacityWhenDisappearing;

        float secondCanvasOpacity = (Time.time - timeStart - firstCanvasDisparitionDelay) / (secondCanvasApparitionDelay - firstCanvasDisparitionDelay);
        if (secondCanvasOpacity >= 0 && secondCanvasOpacity <= 1)
            secondCanvas.GetComponent<CanvasGroup>().alpha = secondCanvasOpacity;

        float secondCanvasOpacityWhenDisappearing = 1 - (Time.time - timeStart - secondCanvasDisparitionDelay) / (thirdCanvasApparitionDelay - secondCanvasDisparitionDelay);
        if (secondCanvasOpacityWhenDisappearing >= 0 && secondCanvasOpacityWhenDisappearing <= 1)
            secondCanvas.GetComponent<CanvasGroup>().alpha = secondCanvasOpacityWhenDisappearing;

        float thirdCanvasOpacity = (Time.time - timeStart - secondCanvasDisparitionDelay) / (thirdCanvasApparitionDelay - secondCanvasDisparitionDelay);
        if (thirdCanvasOpacity >= 0 && thirdCanvasOpacity <= 1)
            thirdCanvas.GetComponent<CanvasGroup>().alpha = thirdCanvasOpacity;

        float thirdCanvasOpacityWhenDisappearing = 1 - (Time.time - timeStart - thirdCanvasDisparitionDelay) / (nextSceneDelay - thirdCanvasDisparitionDelay);
        if (thirdCanvasOpacityWhenDisappearing >= 0 && thirdCanvasOpacityWhenDisappearing <= 1)
            thirdCanvas.GetComponent<CanvasGroup>().alpha = thirdCanvasOpacityWhenDisappearing;

        if (Time.time - timeStart > nextSceneDelay)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
