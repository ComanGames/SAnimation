﻿using Assets.SAnimation.Bakers;
using Assets.SAnimation.Bases;

namespace Assets.SAnimation
{
    public class SAnimation : SpriteAnimationBase
    {
        #region Variables

        //Public Variables 
        public string FolderName = "Factory";
        //private Variables

        #endregion

        public override void LoadContainers()
        {
            AnimationContainer = SerializationUtilits.LoadAnimationContainer(FolderName);
        }
    }
}