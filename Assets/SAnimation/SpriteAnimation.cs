using System.Collections;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.SAnimation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : MonoBehaviour
    {

        #region Variables

        public const int NormalFps = 25;

        //Public Variables 
        public int Fps = 10;
        public bool UseTimeScale;
        public bool AutoStart;
        public bool Preload;
        public string FolderName = "Factory";

        //private Variables
        private SpriteRenderer _spriteRenderer;
        private CircleLinkedList _spriteAnimation;

        #endregion

        public void Start()
        {
            LoadAnimation();
            
            if (Preload)
                PreloadAnimation();

            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (AutoStart)
                Start();
        }

        public void ResetAnimation()
        {
            _spriteAnimation.Reset();
            _spriteRenderer.sprite = _spriteAnimation.FirstNode.GetSprite();
        }

        public void StopAnimation()
        {
            StopCoroutine(UpdeatingSprite());
        }

        public void StartAnimation()
        {
            StartCoroutine(UpdeatingSprite());
        }

        public void PreloadAnimation()
        {
            _spriteAnimation.PreLoad();
        }

        private void LoadAnimation()
        {
            TextAsset temp = Resources.Load<TextAsset>(FolderName + @"/Baked");
            XmlSerializer xs = new XmlSerializer(typeof (CircleLinkedList));
            using (TextReader reader = new StringReader(temp.text))
            {
                _spriteAnimation = (CircleLinkedList) xs.Deserialize(reader);
            }
        }

        IEnumerator UpdeatingSprite()
        {
            while (true)
            {
                if (Fps <= 0)
                    yield return new WaitForFixedUpdate();


                float f = Fps;
                if (UseTimeScale)
                    f *= Time.timeScale;

                int countToSkip = (int)(Mathf.Floor(f/NormalFps));
                float fasting = ((f-(NormalFps*countToSkip))/NormalFps)+1;


                if (f < NormalFps)
                {
                    countToSkip = 1;
                    fasting = f/NormalFps;
                }
           
               GoToNextFrame(countToSkip);
                yield return new WaitForSeconds(1 / (NormalFps*fasting));
            }
        }

        private void GoToNextFrame(int count )
        {
            Sprite tSprite = _spriteRenderer.sprite;
            for (int i = count; i>=0;i--)
                tSprite = _spriteAnimation.Next();

            _spriteRenderer.sprite = tSprite;

        }

    }
}