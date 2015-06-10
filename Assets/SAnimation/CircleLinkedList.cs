using System;
using UnityEngine;

namespace Assets.SAnimation
{
    [Serializable]
    public class CircleLinkedList
    {        
        [Serializable]
        public class Node
        {
            public Node Next;
            public string FilePath; 
            private Sprite _item;

            public void LoadSprite()
            {
                if(_item!=null)
                    return;
                if (FilePath == "") 
                    throw new InvalidOperationException("You didn't set the file path");
                _item=Resources.Load<Sprite>(FilePath);
            }

            public Sprite GetSprite()
            {
                if(_item==null)
                    LoadSprite();
                return _item;
            }
            public Node() { }

            public Node(string path,Node next)
            {
                Next = next;
                FilePath = path;
            }
        }

        public Node FirstNode;
        private Node _current;

        public CircleLinkedList()
        {

        }

        public CircleLinkedList(string[] items)
        {
            Node last = new Node(items[items.Length-1],null);
            Node tempLast = last;
            for (int i = items.Length - 2; i >= 0; i--)
            {
                Node temp = new Node(items[i],tempLast);
                tempLast = temp;
            }
            FirstNode = tempLast;

        }
        
        public Sprite Next()
        {
            if (_current == null)
            {
                _current = FirstNode;
            }

            if (_current.Next == null)
            {
                if (_current == FirstNode)
                    return _current.GetSprite();
                _current = FirstNode;

            }
            _current = _current.Next;
            if (_current.GetSprite() == null)
            {
                _current.LoadSprite();
            }
            return _current.GetSprite();
        }

        public void Reset()
        {
            _current = FirstNode;
        }

        public void PreLoad()
        {
            Node node = FirstNode;
            node.LoadSprite();
            while (node.Next!=null)
            {
                node = node.Next;
               node.LoadSprite();
                 
            }
        }
        

    }
}