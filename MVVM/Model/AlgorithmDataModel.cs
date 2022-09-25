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

    public static AlgorithmDataModel AlgorithmDataModelInstance;

    public AlgorithmDataModel(int accessesCount, int swapsCount)
    {
        _accessesCount = accessesCount;
        _swapsCount = swapsCount;
        _arrayLength = 1;
        AlgorithmDataModelInstance = this;
    }

    public AlgorithmDataModel() : this(0, 0)
    {
    }
}