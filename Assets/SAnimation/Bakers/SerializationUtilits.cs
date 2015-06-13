using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Assets.SAnimation.Bases;
using UnityEngine;

namespace Assets.SAnimation.Bakers
{
    public class SerializationUtilits
    {
        private static readonly string _bakeFileName = @"/Bake";

        public static void SerialazingAnimation(Sprite[] spritesArr,string folderName)
        {
            Sprite[] sprites = spritesArr;
            sprites = sprites.OrderBy(s => s.name).ToArray();
            CicrcleLinkedToXml(SpriteNames(sprites,folderName), folderName);
        }

        private static void CicrcleLinkedToXml(string[] spriteNames, string folderName)
        {
            CircleLinkedList obj = new CircleLinkedList(spriteNames);
            XmlSerializer xs = new XmlSerializer(typeof (CircleLinkedList));
            FileStream fs = File.Create(Environment.CurrentDirectory + @"\Assets\Resources\" + folderName + @"\Bake.xml");
            xs.Serialize(fs, obj);
            fs.Close();
        }

        public static CircleLinkedList LoadAnimationContainer(string folderName)
        {
            if(folderName==default(string))
                throw new InvalidOperationException(); 
            CircleLinkedList result;
            TextAsset temp = Resources.Load<TextAsset>(folderName + _bakeFileName);

            if(temp==null)
                throw new InvalidOperationException();

            XmlSerializer xs = new XmlSerializer(typeof (CircleLinkedList));
            using (TextReader reader = new StringReader(temp.text))
            {
                result = (CircleLinkedList) xs.Deserialize(reader);
            }

            return result;
        }

        private static string[] SpriteNames(Sprite[] sprites,string folderName)
        {
            string[] spriteNames = new string[sprites.Length];
            for (int i = 0; i < sprites.Length; i++)
            {
                spriteNames[i] = folderName + "/" + sprites[i].name;
            }
            return spriteNames;
        }
    }
}