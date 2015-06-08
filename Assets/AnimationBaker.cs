using System;
using System.IO;
using UnityEngine;
using System.Linq;
using System.Xml.Serialization;

namespace Assets
{
    public class AnimationBaker : MonoBehaviour
    {
        public string Name = "Factory";
        public bool TurnOn = true;
        public Sprite[] Sprites;


        public void OnDrawGizmos()
        {
            if (TurnOn)
            {
                Debug.Log("Started");
                Sprites = Sprites.OrderBy(s => s.name).ToArray();
                string[] SpriteNames = new string[Sprites.Length];
                for (int i = 0; i < Sprites.Length; i++)
                {
                    SpriteNames[i] = Name + "/" + Sprites[i].name;
                }
                CircleLinkedList obj = new CircleLinkedList(SpriteNames);
                XmlSerializer xs = new XmlSerializer(typeof (CircleLinkedList));
                FileStream fs =
                    File.Create(Environment.CurrentDirectory + @"\Assets\Resources\" + Name + @"\Baked.xml");
                Debug.Log(Environment.CurrentDirectory);
                xs.Serialize(fs, obj);
                fs.Close();
                TurnOn = false;
            }

        }
    }
}