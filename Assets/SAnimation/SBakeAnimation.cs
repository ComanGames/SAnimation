﻿using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.SAnimation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SBakeAnimation : MonoBehaviour
    {
        public string Name = "Factory";
        public bool Baked;
        public Sprite[] Sprites;

        public void OnDrawGizmos()
        {
            Baking();
        }

        protected  virtual void Baking()
        {
            if (Baked)
            {
                if (Sprites != null && Sprites.Length > 0 && Name != default(string))
                {
                    Sprites = Sprites.OrderBy(s => s.name).ToArray(); // Sorting over Array

                    string[] spriteNames = SpriteNames();
                    SaveData(spriteNames);

                    SpriteAnimation sa = gameObject.AddComponent<SpriteAnimation>();
                    sa.FolderName = Name;
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
        }

        protected  virtual void SaveData(string[] spriteNames)
        {

            CircleLinkedList obj = new CircleLinkedList(spriteNames);
            XmlSerializer xs = new XmlSerializer(typeof (CircleLinkedList));
            FileStream fs = File.Create(Environment.CurrentDirectory + @"\Assets\Resources\" + Name + @"\Baked.xml");
            xs.Serialize(fs, obj);
            fs.Close();
        }

        protected  virtual  string[] SpriteNames()
        {
            string[] spriteNames = new string[Sprites.Length];
            for (int i = 0; i < Sprites.Length; i++)
            {
                spriteNames[i] = Name + "/" + Sprites[i].name;
            }
            return spriteNames;
        }
    }
}