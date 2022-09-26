using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sorting_visualizer.Updaters;

namespace Sorting_visualizer.MVVM.Model;

public class ValuesModel<T> : IEnumerable<int>
{
    public int Length => _values.Length;
    public bool IsCountingEnabled;
    public static event Func<int, int, Task>? OnSwap;
    public static event Func<int[], Task>? OnInspect;
    public static event Func<int, int, Task>? OnSet;
    private readonly int[] _values;

    public int this[int index]
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
                ActionsPerformedUpdater.Set(index, 0);
            _values[index] = value;
        }
    }

    public ValuesModel(int size)
    {
        _values = new int[size];
    }


    public IEnumerator<int> GetEnumerator()
    {
        return _values.Cast<int>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public async Task Swap(int i1, int i2)
    {
        (_values[i1], _values[i2]) = (_values[i2], _values[i1]);
        await OnSwap?.Invoke(i1, i2)!;
    }

    public async Task Set(int i, int value)
    {
        _values[i] = value;
        await OnSet?.Invoke(i, value)!;
    }

    public async Task Inspect(params int[] indexes)
    {
        await OnInspect?.Invoke(indexes)!;
    }
    
    public bool IsSorted()
    {
        for (var i = 0; i < _values.Length - 1; i++)
            if (_values[i] > _values[i + 1])
                return false;
        return true;
    }
}