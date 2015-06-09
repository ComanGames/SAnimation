using System.Collections;
using System.IO;
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
        private SpriteRenderer _spriteRenderer;
        private CircleLinkedList _spriteAnimation;

        public void Start()
        {
            LoadAnimation();
            //_spriteAnimation.Preload();
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