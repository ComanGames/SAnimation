using System;
using UnityEngine;

namespace Assets
{
    [System.Serializable]
    public class CircleLinkedList 
    {
        [System.Serializable]
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
        public Node FirstNode;

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
            _current = last;

        }

        public Sprite Next()
        {

            if (_current.Next == null)
            {
                _current = FirstNode;

            }
            _current = _current.Next;
            if (_current.Item == null)
            {
                _current.Item = Resources.Load<Sprite>(_current.FilePath);
                if (_current.Item == null)
                {
                    Debug.Log(_current.FilePath);

                    throw new ArgumentException();
                }
            }
            return _current.Item;
        }

    }
}