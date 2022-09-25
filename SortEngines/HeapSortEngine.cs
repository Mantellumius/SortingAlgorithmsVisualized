using System;
using System.Threading;
using System.Threading.Tasks;
using Sorting_visualizer.MVVM.Model;

namespace Sorting_visualizer.SortEngines;

public class HeapSortEngine : ISortEngine<int>
{
    public async Task Sort(ValuesModel<int> values, CancellationToken token)
    {
        for (var i = values.Length / 2 - 1; i >= 0; i--)
        {
            await Heapify(values, values.Length, i, token);
        }
        for (var i = values.Length - 1; i > 0; i--)
        {
            await UtilityFunctions.Swap(values, 0, i);
            await Heapify(values, i, 0, token);
        }
    }

    private static async Task Heapify(ValuesModel<int> values, int n, int i, CancellationToken token)
    {
        while (true)
        {
            if (token.IsCancellationRequested)
                return;
            var largest = i;
            var left = 2 * largest + 1;
            var right = 2 * largest + 2;
            if (left < n)
            {
                if (token.IsCancellationRequested)
                    return;
                await UtilityFunctions.Inspect(left, largest);
                if (values[left] > values[largest])
                    largest = left;
            }

            if (right < n)
            {
                if (token.IsCancellationRequested)
                    return;
                await UtilityFunctions.Inspect(right, largest);
                if (values[right] > values[largest])
                    largest = right;
            }

            if (largest == i) return;
            await UtilityFunctions.Swap(values, i, largest);
            i = largest;
        }
    }
}