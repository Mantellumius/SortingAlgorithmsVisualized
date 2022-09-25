using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sorting_visualizer.MVVM.Model;

namespace Sorting_visualizer.SortEngines;

public class MergeSortEngine : ISortEngine<int>
{
    public async Task Sort(ValuesModel<int> values, CancellationToken token)
    {
        await MergeSort(values, 0, values.Length - 1, token);
    }

    private static async Task MergeSort(ValuesModel<int> array, int left, int right, CancellationToken token)
    {
        if (left >= right) return;
        var middle = left + (right - left) / 2;
        await MergeSort(array, left, middle, token);
        await MergeSort(array, middle + 1, right, token);
        await MergeArray(array, left, middle, right, token);
    }

    private static async Task MergeArray(ValuesModel<int> values, int left, int middle, int right,
        CancellationToken token)
    {
        var leftTempArray = values.Skip<int>(left).Take(middle - left + 1).ToArray();
        await UtilityFunctions.Inspect(Enumerable.Range(left, middle - left + 1).ToArray());
        var rightTempArray = values.Skip(middle + 1).Take(right - middle).ToArray();
        await UtilityFunctions.Inspect(Enumerable.Range(middle + 1, right - middle).ToArray());
        var i = 0;
        var j = 0;
        var k = left;
        while (i < leftTempArray.Length && j < rightTempArray.Length)
        {
            if (token.IsCancellationRequested)
                return;
            var newValue = leftTempArray[i] <= rightTempArray[j] ? leftTempArray[i++] : rightTempArray[j++];
            await UtilityFunctions.Set(values, ++k - 1, newValue);
        }

        while (i < leftTempArray.Length)
        {
            if (token.IsCancellationRequested)
                return;
            await UtilityFunctions.Set(values, ++k - 1, leftTempArray[i++]);
        }

        while (j < rightTempArray.Length)
        {
            if (token.IsCancellationRequested)
                return;
            await UtilityFunctions.Set(values, ++k - 1, rightTempArray[j++]);
        }
    }
}