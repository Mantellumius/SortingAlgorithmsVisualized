using System;
using System.Threading;
using System.Threading.Tasks;
using Sorting_visualizer.MVVM.Model;

namespace Sorting_visualizer.SortEngines;

public class SelectionSortEngine : ISortEngine<int>
{
    public async Task Sort(ValuesModel<int> values, CancellationToken token)
    {
        for (var i = 0; i < values.Length - 1; i++)
        {
            var test = new int[1];
            var minValueIndex = i;
            for (var j = i + 1; j < values.Length; j++)
            {
                if (token.IsCancellationRequested)
                    return;
                minValueIndex = values[j] < values[minValueIndex] ? j : minValueIndex;
                await UtilityFunctions.Inspect(j, minValueIndex);
            }

            await UtilityFunctions.Swap(values, minValueIndex, i);
        }
    }
}