using System;
using System.Threading.Tasks;
using Sorting_visualizer.MVVM.Model;

namespace Sorting_visualizer;

public static class UtilityFunctions
{
    public static event Func<int, int, Task>? OnSwap;
    public static event Func<int[], Task>? OnInspect;
    public static event Func<int, int, Task> OnStarightChange;

    public static async Task Swap(ValuesModel<int> values, int i1, int i2)
    {
        (values[i1], values[i2]) = (values[i2], values[i1]);
        await OnSwap?.Invoke(i1, i2);
    }

    public static async Task StraightChange(ValuesModel<int> values, int i, int height)
    {
        values[i] = height;
        await OnStarightChange.Invoke(i, height);
    }

    public static async Task Inspect(params int[] indexes)
    {
        await OnInspect(indexes);
    }


    public static bool IsSorted(int[] values)
    {
        for (int i = 0; i < values.Length - 1; i++)
            if (values[i] > values[i + 1])
                return false;
        return true;
    }
}