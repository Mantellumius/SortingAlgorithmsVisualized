using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sorting_visualizer.MVVM.Model;

public class ValuesModel<T> : IEnumerable<T>
{
    public int Length => _values.Length;
    public bool IsCountingEnabled;
    private readonly T[] _values;

    public T this[int index]
    {
        get
        {
            if (IsCountingEnabled)
                ActionsPerformedUpdater.Inspect(index);
            return _values[index];
        }
        set
        {
            if (IsCountingEnabled)
                ActionsPerformedUpdater.Inspect(index);
            _values[index] = value;
        }
    }

    public ValuesModel(int size)
    {
        _values = new T[size];
    }


    public IEnumerator<T> GetEnumerator()
    {
        return _values.Cast<T>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}