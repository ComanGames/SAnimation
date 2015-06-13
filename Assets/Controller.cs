using UnityEngine;
using System.Collections;
using Assets.SAnimation;

namespace Assets
{


    public class Controller : MonoBehaviour
    {
        public SAnimator MyAnimator;

        public void Update()
        {
            if (Input.GetAxisRaw("Vertical") == 0)
            {
                
            }

            if(Input.GetAxisRaw("Vertical")==1)
            {
                
                MyAnimator.GoToAnim("Jump");
                MyAnimator.AddNextAnimation(MyAnimator.DeffaultAnimation);
            }
            if (Input.GetAxisRaw("Vertical") == -1)
            {
                MyAnimator.GoToAnim("Slide");

                MyAnimator.AddNextAnimation("SlideDown");
                MyAnimator.AddNextAnimation("SlideUp");
                MyAnimator.AddNextAnimation("Run");
            }
        }

    }
}