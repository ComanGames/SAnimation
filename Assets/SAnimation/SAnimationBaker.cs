using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.SAnimation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SAnimationBaker : MonoBehaviour
    {
        public string FolderName = "Factory";
        public bool Baked;
        public Sprite[] Sprites;

        public void OnDrawGizmos()
        {
            if (Baked)
                Bake();

        }

        private void Bake()
        {
            if (Sprites != null && Sprites.Length > 0 && FolderName != default(string))
            {
                Sprites = Sprites.OrderBy(s => s.name).ToArray(); // Sorting over Array
                string[] spriteNames = SpriteNames();
                SaveData(spriteNames);

                SpriteAnimation sa = gameObject.AddComponent<SpriteAnimation>();
                sa.FolderName = FolderName;
                Baked = false;
                gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[0];
                DestroyImmediate(this);
                } 
            else
            {
                Debug.Log("Add elements to array or set the name");
                Baked = false;
            }
        }

        private void SaveData(string[] spriteNames)
        {

            CircleLinkedList obj = new CircleLinkedList(spriteNames);
            XmlSerializer xs = new XmlSerializer(typeof(CircleLinkedList));
            FileStream fs = File.Create(Environment.CurrentDirectory + @"\Assets\Resources\" + FolderName + @"\Baked.xml");
            xs.Serialize(fs, obj);
            fs.Close();
        }

        private string[] SpriteNames()
        {
            string[] spriteNames = new string[Sprites.Length];
            for (int i = 0; i < Sprites.Length; i++)
            {
                spriteNames[i] = FolderName + "/" + Sprites[i].name;
            }
            return spriteNames;
        }
    }
}