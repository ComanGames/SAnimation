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
            [NonSerialized]
            public Sprite Item;
            public string FilePath;

            public Node() { }

            public Node(string path,Node next)
            {
                Next = next;
                FilePath = path;
            }
        }

        public Node _current;
        public Node _firstNode;

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
            _firstNode = tempLast;
            _current = last;

        }

        public Sprite Next()
        {
            if (_current == null)
            {
                throw new InvalidOperationException();
            }
            else
            {
                Debug.Log(_current.FilePath);
            }
            if (_current.Next == null)
            {
                if (_current == _firstNode)
                    return _current.Item;
                _current = _firstNode;

            }
            _current = _current.Next;
            if (_current.Item == null)
            {
                _current = LoadSprite(_current);
            }
            return _current.Item;
        }

        private Node LoadSprite(Node node)
        {
            if (node == null)
                return null;
            if(node.FilePath==default(string))
                throw  new InvalidOperationException("you haven't set the path");
            node.Item = Resources.Load<Sprite>(node.FilePath);
            if (node.Item == null)
            {
                throw new ArgumentException();
            }
            return node;

        }
    }
}