using System;
using System.Collections.Generic;
using Assets.SAnimation.Bakers;
using Assets.SAnimation.Bases;

namespace Assets.SAnimation
{
    [Serializable]
    public class AnimationAddress
    {
        public string Name;
        public string Folder;

        public AnimationAddress(string name, string folder)
        {
            Name = name;
            Folder = folder;
        }
    }

    public class SAnimator : SpriteAnimationBase
    {
       #region Variables

        public string DeffaultAnimation;
        public Dictionary<string, CircleLinkedList> Animations;
        public AnimationAddress[] AnimationAddresses;
        private Queue<string> _animationQueue;
        private bool _autoGoToNext;
        #endregion

        public override void LoadContainers()
        {
            if (AnimationAddresses == null || AnimationAddresses.Length == 0)
                throw new InvalidOperationException("We have no animation to preload");

            Animations = new Dictionary<string, CircleLinkedList>(AnimationAddresses.Length);

            foreach (AnimationAddress t in AnimationAddresses)
                Animations.Add(t.Name, SerializationUtilits.LoadAnimationContainer(t.Folder));

            _animationQueue = new Queue<string>();
            if (DeffaultAnimation == default(string))
                DeffaultAnimation = AnimationAddresses[0].Name;
            GoToAnim(DeffaultAnimation);
        }

        public override void PreloadAnimation()
        {
            LoadContainers();
            //PreLoading each animation in over dictionary
            foreach (KeyValuePair<string, CircleLinkedList> valuePair in Animations)
                valuePair.Value.PreLoad();
            Loaded = true;
        }

        public void GoToAnim(string animationName)
        {
            ChangeAnim(animationName);
            _animationQueue = new Queue<string>();
            GoToNextFrame(1);
        }

        private void ChangeAnim(string animationName)
        {
            if (!Animations.ContainsKey(animationName))
                throw new InvalidOperationException("The animation" + animationName + " isn't in over list");
            //Chenging Animation
            ResetAnimation();
            AnimationContainer = Animations[animationName];
            if (_autoGoToNext)
            {
                AnimationContainer.ClearEndEvnt();
                AnimationContainer.OnEnd += NextInQueue;
            }
        }

        private void NextInQueue()
        {
            if (_animationQueue.Count > 0)
            {
                string nextAnimation = _animationQueue.Dequeue();
                ChangeAnim(nextAnimation);
            }
            else
                _autoGoToNext = false;
        }

        public void AddNextAnimation(string animName)
        {
            if (!_autoGoToNext)
            {
                _autoGoToNext = true;
                AnimationContainer.ClearEndEvnt();
                AnimationContainer.OnEnd += NextInQueue;

            }
            _animationQueue.Enqueue(animName);
        }

    }
            
}
