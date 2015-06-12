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
                    for (int i = 0; i < AnimationStates.Length; i++)
                    {
                        SerializationUtilits.SerialazingAnimation(AnimationStates[i].Sprites,AnimationStates[i].Folder);
                        sa.AnimationStates.Add(AnimationStates[i].Name,AnimationStates[i].Folder);
                    }
                    if (DeffaultAnimation == default(string))
                        sa.DeffaultAnimation = AnimationStates[0].Name;
                    else
                        sa.DeffaultAnimation = DeffaultAnimation;
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