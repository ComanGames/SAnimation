using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.SAnimation
{
    public class BakerBase
    {
        private AnimationBaker _animationBaker;

        public BakerBase(AnimationBaker animationBaker)
        {
            _animationBaker = animationBaker;
        }

        public void OnDrawGizmos()
        {
            if (_animationBaker.Baked)
            {
                if(_animationBaker.Sprites != null && _animationBaker.Sprites.Length>0 && _animationBaker.Name != default(string))
                {
                    _animationBaker.Sprites = _animationBaker.Sprites.OrderBy(s => s.name).ToArray(); // Sorting over Array
                    string [] spriteNames = SpriteNames();
                    XmlSerialize(spriteNames);

                    SpriteAnimation sa = _animationBaker.gameObject.AddComponent<SpriteAnimation>();
                    sa.FolderName = _animationBaker.Name;
                    _animationBaker.Baked = false;
                    _animationBaker.gameObject.GetComponent<SpriteRenderer>().sprite = _animationBaker.Sprites[0];
                    Object.DestroyImmediate(_animationBaker);

                }
                else
                {
                    Debug.Log("Add elements to array or set the name");
                    _animationBaker.Baked = false;
                }
            }

        }

        private void XmlSerialize(string[] spriteNames)
        {

            CircleLinkedList obj = new CircleLinkedList(spriteNames);
            XmlSerializer xs = new XmlSerializer(typeof (CircleLinkedList));
            FileStream fs = File.Create(Environment.CurrentDirectory + @"\Assets\Resources\" + _animationBaker.Name + @"\Baked.xml");
            xs.Serialize(fs, obj);
            fs.Close();
        }

        private string[] SpriteNames()
        {
            string[] spriteNames = new string[_animationBaker.Sprites.Length];
            for (int i = 0; i < _animationBaker.Sprites.Length; i++)
            {
                spriteNames[i] = _animationBaker.Name + "/" + _animationBaker.Sprites[i].name;
            }
            return spriteNames;
        }
    }
}