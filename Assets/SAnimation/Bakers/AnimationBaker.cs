using UnityEngine;

namespace Assets.SAnimation.Bakers
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AnimationBaker : MonoBehaviour
    {
        public string Name = "Factory";
        public bool Bake;
        public Sprite[] Sprites;

        public void OnDrawGizmos()
        {
            if (Bake)
            {
                if(Sprites != null &&Sprites.Length>0 && Name != default(string))
                {
                    SerializationUtilits.SerialazingAnimation(Sprites,Name);

                    SAnimation sa = gameObject.AddComponent<SAnimation>();
                    sa.FolderName = Name;
                    gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[0];
                    DestroyImmediate(this);
                    Bake = false;
                }
                else
                {
                    Debug.Log("Add elements to array or set the name");
                    Bake = false;
                }
            }

        }
    }
}