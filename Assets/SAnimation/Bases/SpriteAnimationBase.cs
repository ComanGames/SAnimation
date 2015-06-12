using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
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
            SRenderer = GetComponent<SpriteRenderer>();

             Starter();
        }

        private void Starter()
        {
            if (!Loaded)
            {
                LoadContainers();
                if (Preload)
                    PreloadAnimation();
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

        protected CircleLinkedList LoadAnimationContainer(string folderName)
        {
            if(folderName==default(string))
                throw new InvalidOperationException(); 
            CircleLinkedList result;
            TextAsset temp = Resources.Load<TextAsset>(folderName + @"/Bake");

            if(temp==null)
                throw new InvalidOperationException();

            XmlSerializer xs = new XmlSerializer(typeof (CircleLinkedList));
            using (TextReader reader = new StringReader(temp.text))
            {
                result = AnimationContainer = (CircleLinkedList) xs.Deserialize(reader);
            }

            return result;
        }

        public void StartAnimation()
        {
            StartCoroutine(UpdeatingSprite());
        }

        public void ResetAnimation()
        {
            AnimationContainer.Reset();
            SRenderer.sprite = AnimationContainer.FirstNode.GetSprite();
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
        }
    }
}