using UnityEngine;
using UnityEngine.UI;

/*
 * How to use -
 *      Attach to a text or text mesh pro object in the canvas.
 */

namespace Utilities.FPS
{
    public class FpsCounter : MonoBehaviour
    {
        private Text _fpsText;

        private int _lastFrameIndex;
        private float[] _frameDeltaTimeArray;

        private void Awake()
        {
            if (TryGetComponent(out Text text))
                _fpsText = text;

            _frameDeltaTimeArray = new float[50];
        }

        private void Update()
        {
            _frameDeltaTimeArray[_lastFrameIndex] = Time.unscaledDeltaTime;
            _lastFrameIndex = (_lastFrameIndex + 1) % _frameDeltaTimeArray.Length;

            if (_fpsText != null)
                _fpsText.text = Mathf.RoundToInt(CalculateFPS()).ToString();

        }

        private float CalculateFPS()
        {
            float total = 0f;
            foreach (float deltaTime in _frameDeltaTimeArray)
            {
                total += deltaTime;
            }
            return _frameDeltaTimeArray.Length / total;
        }
    }
}
