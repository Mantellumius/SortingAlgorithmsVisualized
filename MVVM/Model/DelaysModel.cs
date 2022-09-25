using Sorting_visualizer.Core;

namespace Sorting_visualizer.MVVM.Model;

public class DelaysModel : ObservableObject
{
    private int _inspectDelay;

    public int InspectDelay
    {
        get => _inspectDelay;
        set
        {
            _inspectDelay = value;
            OnPropertyChanged();
        }
    }

    private int _swapDelay;

    public int SwapDelay
    {
        get => _swapDelay;
        set
        {
            _swapDelay = value;
            OnPropertyChanged();
        }
    }

    private int _straightChange;

    public int StraightChange
    {
        get => _straightChange;
        set
        {
            _straightChange = value;
            OnPropertyChanged();
        }
    }

    public static DelaysModel? DelaysModelInstance;

    public DelaysModel()
    {
        _inspectDelay = 1;
        _swapDelay = 1;
        _straightChange = 1;
        DelaysModelInstance ??= this;
    }
}