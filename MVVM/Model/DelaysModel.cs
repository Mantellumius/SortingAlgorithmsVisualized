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

    private int _setDelay;

    public int SetDelay
    {
        get => _setDelay;
        set
        {
            _setDelay = value;
            OnPropertyChanged();
        }
    }

    public static DelaysModel? DelaysModelInstance;

    public DelaysModel()
    {
        _inspectDelay = 1;
        _swapDelay = 1;
        _setDelay = 1;
        DelaysModelInstance ??= this;
    }
}