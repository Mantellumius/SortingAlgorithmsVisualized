using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Sorting_visualizer.MVVM.Model;

namespace Sorting_visualizer.SortEngines;

public class BucketSortEngine : ISortEngine<int>
{
    public async Task Sort(ValuesModel<int> values, CancellationToken token)
    {
        var sortedList = new List<int>();
        var minValue = values[0];
        var maxValue = values[0];
        if (values.Length <= 1)
            return;

        for (var i = 1; i < values.Length; i++)
        {
            if (token.IsCancellationRequested)
                return;
            await values.Inspect(i);
            if (values[i] > maxValue)
                maxValue = values[i];
            await values.Inspect(i);
            if (values[i] < minValue)
                minValue = values[i];
        }

        var numberOfBuckets = maxValue - minValue + 1;
        var bucket = new List<int>[numberOfBuckets].Select(x => new List<int>()).ToList();

        for (var i = 0; i < values.Length; i++)
        {
            if (token.IsCancellationRequested)
                return;
            await values.Inspect(i);
            var selectedBucket = values[i] / numberOfBuckets;
            await values.Inspect(i);
            bucket[selectedBucket].Add(values[i]);
        }

        for (var i = 0; i < numberOfBuckets; i++)
        {
            if (token.IsCancellationRequested)
                return;
            var temp = BubbleSort(bucket[i].ToArray());
            sortedList.AddRange(temp);
        }

        for (var i = 0; i < values.Length; i++)
        {
            if (token.IsCancellationRequested)
                return;
            await values.Set( i, sortedList[i]);
        }
    }

    private static IEnumerable<int> BubbleSort(int[] listInput)
    {
        for (var i = 0; i < listInput.Length; i++)
        for (var j = 0; j < listInput.Length; j++)
            if (listInput[i] < listInput[j])
                (listInput[i], listInput[j]) = (listInput[j], listInput[i]);

        return listInput;
    }
}