using UnityEngine;

public class FPScapping : MonoBehaviour
{
  [SerializeField] bool toggleCappedFPS = false;
  [SerializeField] int fps = -1;
  [SerializeField] int vSync = 1;

    // Start is called before the first frame update
    private void Awake() {
      if (toggleCappedFPS){
        Invoke("CapFPS",0);
      }
    }

    private void Update() {
      if (toggleCappedFPS){
        Invoke("CapFPS",0);
      }
    }

    void CapFPS(){
      #if UNITY_EDITOR
      Debug.Log("Capping FPS");
      QualitySettings.vSyncCount = vSync;
      Application.targetFrameRate = fps;
      toggleCappedFPS = false;  
      #endif
    }
}
