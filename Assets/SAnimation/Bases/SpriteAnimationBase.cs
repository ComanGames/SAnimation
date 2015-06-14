using System.Collections;
using UnityEngine;

namespace Assets.SAnimation.Bases
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimationBase : MonoBehaviour
    {
        #region Variables

        public const int NormalFps = 25;
        public int Fps = 10;
        public bool UseTimeScale;
        public bool AutoStart;
        public bool Preload;

        protected SpriteRenderer SRenderer;
        protected CircleLinkedList AnimationContainer;
        protected bool Loaded;

        #endregion

        public void Start()
        {
            Preloader.Instance.Preloading += Starter;
        }

        private void Starter()
        {
            SRenderer = GetComponent<SpriteRenderer>();
            if (!Loaded)
            {
                LoadContainers();
                if (Preload)
                {
                    PreloadAnimation();
                }
            }
            if(AutoStart)
                StartAnimation();
        }

        public virtual void LoadContainers()
        {
            
        } 

        public virtual void PreloadAnimation()
        {
            LoadContainers();
            AnimationContainer.PreLoad();
            Loaded = true;
        }

        public void StartAnimation()
        {
            StartCoroutine(UpdeatingSprite());
        }

        public void ResetAnimation()
        {
            if (AnimationContainer != null)
            {
                AnimationContainer.Reset();
                SRenderer.sprite = AnimationContainer.FirstNode.GetSprite();
            }
        }

        public void StopAnimation()
        {
            StopCoroutine(UpdeatingSprite());
        }

        public void GoToNextFrame(int count)
        {
            var tSprite = SRenderer.sprite;
            for (var i = count; i >= 0; i--)
                tSprite = AnimationContainer.Next();

            SRenderer.sprite = tSprite;
        }

        private IEnumerator UpdeatingSprite()
        {
            while (true)
            {
                if (Fps <= 0)
                    yield return new WaitForFixedUpdate();


                float f = Fps;
                if (UseTimeScale)
                    f *= Time.timeScale;

                var countToSkip = (int) (Mathf.Floor(f/NormalFps));
                var fasting = ((f - (NormalFps*countToSkip))/NormalFps) + 1;


                if (f < NormalFps)
                {
                    countToSkip = 1;
                    fasting = f/NormalFps;
                }

                GoToNextFrame(countToSkip);
                yield return new WaitForSeconds(1/(NormalFps*fasting));
            }
            // ReSharper disable once FunctionNeverReturns
       }
    }
}