using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sorting_visualizer.MVVM.Model;

namespace Sorting_visualizer.SortEngines;

public class RadixSortEngine : ISortEngine<int>
{
    public async Task Sort(ValuesModel<int> values, CancellationToken token)
    {
        for (var exponent = 1; values.Max() / exponent > 0; exponent *= 10)
            await CountingSort(values, values.Length, exponent, token);
    }

    private static async Task CountingSort(ValuesModel<int> values, int size, int exponent, CancellationToken token)
    {
        var outputArr = new int[size];
        var occurrences = new int[10];
        for (var i = 0; i < 10; i++)
            occurrences[i] = 0;
        for (var i = 0; i < size; i++)
        {
            if (token.IsCancellationRequested)
                return;
            await UtilityFunctions.Inspect(i);
            occurrences[values[i] / exponent % 10]++;
        }

        for (var i = 1; i < 10; i++)
        {
            occurrences[i] += occurrences[i - 1];
        }

        for (var i = size - 1; i >= 0; i--)
        {
            if (token.IsCancellationRequested)
                return;
            await UtilityFunctions.Inspect(i);
            outputArr[occurrences[(values[i] / exponent) % 10] - 1] = values[i];
            occurrences[values[i] / exponent % 10]--;
        }

        for (var i = 0; i < size; i++)
        {
            if (token.IsCancellationRequested)
                return;
            await UtilityFunctions.Set(values, i, outputArr[i]);
        }
    }
}