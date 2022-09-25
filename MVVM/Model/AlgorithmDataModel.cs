using Sorting_visualizer.Core;

namespace Sorting_visualizer.MVVM.Model;

public class AlgorithmDataModel : ObservableObject
{
    private int _arrayLength;

    public int ArrayLength
    {
        get => _arrayLength;
        set
        {
            _arrayLength = value;
            OnPropertyChanged();
        }
    }

    private int _accessesCount;

    public int AccessesCount
    {
        get => _accessesCount;
        set
        {
            _accessesCount = value;
            OnPropertyChanged();
        }
    }

    private int _swapsCount;

    public int SwapsCount
    {
        get => _swapsCount;
        set
        {
            _swapsCount = value;
            OnPropertyChanged();
        }
    }

    private int _setCount;

    public int SetCount
    {
        get => _setCount;
        set
        {
            _setCount = value;
            OnPropertyChanged();
        }
    }

    public static AlgorithmDataModel? AlgorithmDataModelInstance;

    public AlgorithmDataModel()
    {
        _accessesCount = 0;
        _swapsCount = 0;
        _setCount = 0;
        _arrayLength = 1;
        AlgorithmDataModelInstance ??= this;
    }
}