using System.Threading;
using System.Threading.Tasks;
using Sorting_visualizer.MVVM.Model;

namespace Sorting_visualizer.SortEngines;

public class QuickSortEngine : ISortEngine<int>
{
    public async Task Sort(ValuesModel<int> values, CancellationToken token)
    {
        await QuickSort(values, 0, values.Length - 1, token);
    }

    private async Task QuickSort(ValuesModel<int> values, int leftIndex, int rightIndex, CancellationToken token)
    {
        var i = leftIndex;
        var j = rightIndex;
        var pivot = values[leftIndex];
        await values.Inspect(leftIndex);
        while (i <= j)
        {
            if (token.IsCancellationRequested)
                return;
            await values.Inspect(i, leftIndex);
            while (values[i] < pivot)
                i++;
            await values.Inspect(j, leftIndex);
            while (values[j] > pivot)
                j--;
            if (i > j) continue;
            await values.Swap(i, j);
            i++;
            j--;
        }

        if (leftIndex < j)
            await QuickSort(values, leftIndex, j, token);
        if (i < rightIndex)
            await QuickSort(values, i, rightIndex, token);
    }
}