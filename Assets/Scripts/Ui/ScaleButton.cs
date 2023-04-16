using DG.Tweening;
using UnityEngine;

namespace Ui
{
    public class ScaleButton : MonoBehaviour
    {
        private const float _duration = 0.25f;

        private Tweener scaleOn;
        private Tweener scaleOff;

        public void ScaleOn()
        {
            scaleOn = transform.DOScale(1.05f, _duration);
        }

        public void ScaleOff()
        {
            scaleOff = transform.DOScale(1, _duration);
        }

        public void DefaultScale()
        {
            scaleOn.Kill();
            scaleOff.Kill(); 
            transform.localScale = Vector2.one;
        }

        private void OnDestroy()
        {
            scaleOn.Kill();
            scaleOff.Kill();
        }
    }
}
