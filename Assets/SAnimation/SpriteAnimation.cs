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
        private SpriteRenderer _currentSprite;
        private CircleLinkedList _spriteAnimation;

        public void Start()
        {
            TextAsset temp = Resources.Load<TextAsset>(FolderName+@"/Baked");
            XmlSerializer xs = new XmlSerializer(typeof(CircleLinkedList));
            Debug.Log(temp.text);
            using (TextReader reader = new StringReader(temp.text))
            {
                _spriteAnimation = (CircleLinkedList)xs.Deserialize(reader);
            }
            //           _spriteAnimation.Preload();
            _currentSprite = GetComponent<SpriteRenderer>();
            _currentSprite.sprite = _spriteAnimation._current.Item;
            StartCoroutine(UpdeatingSprite());
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
            _currentSprite.sprite = _spriteAnimation.Next();
        }
    }
}