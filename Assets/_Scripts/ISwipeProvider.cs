using System;

public interface ISwipeProvider
{
    event Action<float> OnHorizontalSwipe;
    event Action<float> OnVerticalSwipe;
}
