using UnityEngine;

public class FPScapping : MonoBehaviour
{
  [SerializeField] int fps = -1;
  [SerializeField] int vSync = 1;

    // Start is called before the first frame update
    private void Awake() {
      CapFPS();
    }

    private void Update() {
    
    }

    void CapFPS(){
      #if UNITY_EDITOR
      //Debug.Log("Capping FPS");
      QualitySettings.vSyncCount = vSync;
      Application.targetFrameRate = fps;
      #endif
    }
}
