using System.Collections;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.SAnimation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : MonoBehaviour
    {

        //Public Variables 
        public float Fps = 10;
        public string FolderName = "Factory";
        public bool Preload;
        private SpriteRenderer _spriteRenderer;
        private CircleLinkedList _spriteAnimation;
        public const int NormalFps =30;
        

        public void Start()
        {
            LoadAnimation();
            if (Preload)
            {
                _spriteAnimation.PreLoad();
            }
            _spriteRenderer = GetComponent<SpriteRenderer>();
            StartCoroutine(UpdeatingSprite());
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
                Debug.Log("Wati for:" + Fps*Time.timeScale);
                Debug.Log("Next Frame is:");
            
               GoToNextFrame();
                yield return new WaitForSeconds(1 / (Fps * Time.timeScale));
            }
        }

        private void GoToNextFrame()
        {
            _spriteRenderer.sprite = _spriteAnimation.Next();
        }
    }
}