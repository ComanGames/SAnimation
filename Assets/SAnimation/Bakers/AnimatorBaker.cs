using System;
using UnityEngine;

namespace Assets.SAnimation.Bakers
{
    [Serializable]
    public class AnimatorState
    {
        public string Name;
        public string Folder;
        public Sprite[] Sprites;
    }


    public class AnimatorBaker:MonoBehaviour
    {
        #region Variables
        //Public Variables 
        public bool Bake;
        public string DeffaultAnimation;
        public AnimatorState[] AnimationStates;


        #endregion

        public void OnDrawGizmos()
        {
            if (Bake)
            {
                if (AnimationStates != null && AnimationStates.Length > 0)
                {
                    SAnimator sa = gameObject.AddComponent<SAnimator>();
                    sa.AnimationAddresses = new AnimationAddress[AnimationStates.Length];

                    for (int i = 0; i < AnimationStates.Length; i++)
                    {
                        SerializationUtilits.SerialazingAnimation(AnimationStates[i].Sprites, AnimationStates[i].Folder);
                        sa.AnimationAddresses[i] = new AnimationAddress(AnimationStates[i].Name,AnimationStates[i].Folder);
                    }

                    sa.DeffaultAnimation = DeffaultAnimation == default(string) ? AnimationStates[0].Name : DeffaultAnimation;
                    
                    DestroyImmediate(this);

                }
                else
                {
                    Debug.Log("Soome problems with array");
                    Bake = false;

                }
            }
        }
    }
}