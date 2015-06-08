using System.Collections;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : MonoBehaviour
    {

        //Public Variables 
        public float Fps = 10;
        public string FolderName = "Factory";
        private SpriteRenderer currentSprite;
        private CircleLinkedList _spriteAnimation;

        public void Start()
        {
            TextAsset temp = Resources.Load<TextAsset>(FolderName+@"/Baked");
            if (temp == null)
            {
                Debug.Log("O-P");
            }
            XmlSerializer xs = new XmlSerializer(typeof(CircleLinkedList));
            using (TextReader reader = new StringReader(temp.text))
            {
                 _spriteAnimation= (CircleLinkedList)xs.Deserialize(reader);
            }
            
            currentSprite = GetComponent<SpriteRenderer>();
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
            currentSprite.sprite = _spriteAnimation.Next();
        }
    }
}