using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpacePixelPerfectCanvasScaler : MonoBehaviour
{
    // Ready made in -> CanvasWorldScaler

    void AdjustCanvas()
    {
          //if (!_pixelPerfectCamera.isInitialized || _pixelPerfectCamera.cameraSize.x == 0)
          //  return;
          
        //_cameraSize = _pixelPerfectCamera.cameraSize;
        //_assetsPixelsPerUnit = _pixelPerfectCamera.assetsPixelsPerUnit;
        //GetComponent<RectTransform>().sizeDelta = 2.0f * _assetsPixelsPerUnit * _cameraSize;

        //Vector3 localScale = GetComponent<RectTransform>().localScale;
        //localScale.x = 1 / _assetsPixelsPerUnit;
        //localScale.y = 1 / _assetsPixelsPerUnit;
        //GetComponent<RectTransform>().localScale = localScale;
    }
}
