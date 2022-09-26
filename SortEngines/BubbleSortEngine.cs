using System.Threading;
using System.Threading.Tasks;
using Sorting_visualizer.MVVM.Model;

namespace Sorting_visualizer.SortEngines;

public class BubbleSortEngine : ISortEngine<int>
{
    public async Task Sort(ValuesModel<int> values, CancellationToken token)
    {
        for (var j = 0; j < values.Length - 1; j++)
        {
            for (var i = 0; i < values.Length - j - 1; i++)
            {
                if (token.IsCancellationRequested)
                    return;
                var nextIndex = i + 1;
                if (values[i] > values[nextIndex])
                    await values.Swap(i, nextIndex);
                else
                    await values.Inspect(i, nextIndex);
            }
        }
    }
}