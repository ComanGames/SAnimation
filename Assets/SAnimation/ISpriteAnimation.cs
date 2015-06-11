namespace Assets.SAnimation
{
    public interface ISpriteAnimation
    {
        void LoadAnimation();
        void ResetAnimation();
        void StopAnimation();
        void StartAnimation();
        void PreloadAnimation();
        void GoToNextFrame(int count);
    }
}