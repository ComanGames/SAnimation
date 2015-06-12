using System;
using System.Collections.Generic;
using Assets.SAnimation.Bases;
using UnityEngine;

namespace Assets.SAnimation
{
    [Serializable]
    public class AnimatorState
    {
        public string Folder;
        public string Name;
        public Sprite[] Sprites;
    }

    public class SAnimator : SpriteAnimationBase
    {
        #region Variables

        public string DeffaultAnimation;
        public Dictionary<string, string> AnimationStates;
        public Dictionary<string, CircleLinkedList> Animations;

        #endregion

        public SAnimator()
        {
            AnimationStates = new Dictionary<string, string>();
            Animations = new Dictionary<string, CircleLinkedList>();
        }

        public override void LoadContainers()
        {
            if(AnimationStates != null && (AnimationStates==null&&AnimationStates.Count==0))
                throw new InvalidOperationException();

            if (AnimationStates != null)
            {
                Animations = new Dictionary<string, CircleLinkedList>(AnimationStates.Count);

                foreach (KeyValuePair<string, string> animS in AnimationStates)
                    Animations.Add(animS.Key,LoadAnimationContainer(animS.Value));
            }
            GoToAnim(DeffaultAnimation);

        }

        public override void PreloadAnimation()
        {
            LoadContainers();
            //Preloading each animation in over dictionary
            foreach (KeyValuePair<string, CircleLinkedList> valuePair in Animations)
                valuePair.Value.PreLoad();
            Loaded = true;
        }

        public void GoToAnim(string animationName)
        {
            ResetAnimation();
            if (Animations.ContainsKey(animationName))
            {
                AnimationContainer = Animations[animationName];
                return;
            }
            throw  new InvalidOperationException("No such type of animation");
        }

    }
            
}
