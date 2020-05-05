using UnityEngine;

namespace InternalAssets.Scripts.Player
{
    [ExecuteInEditMode]
    public class SnapGrid : MonoBehaviour {
        public int xStep = 1;
        public int yStep = 1;
        public int zStep = 1;
  
#if UNITY_EDITOR
        void Update () {
            if(!Application.isPlaying){
                SnapPos();
            }
        }
#endif
  
        private void SnapPos(){
            var position = transform.position;
            int clampedX = Mathf.RoundToInt (position.x) / xStep;
            int clampedY = Mathf.RoundToInt (position.y) / yStep;
            int clampedZ = Mathf.RoundToInt (position.z) / zStep;
            position = new Vector3 (clampedX*xStep, clampedY*yStep, clampedZ*zStep);
            transform.position = position;
        }

    }
}