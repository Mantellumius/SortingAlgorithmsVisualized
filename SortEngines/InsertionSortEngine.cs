using System.Threading;
using System.Threading.Tasks;
using Sorting_visualizer.MVVM.Model;

namespace Sorting_visualizer.SortEngines;

public class InsertionSortEngine : ISortEngine<int>
{
    public async Task Sort(ValuesModel<int> values, CancellationToken token)
    {
        for (var i = 1; i < values.Length; i++)
        {
            var value = values[i];
            for (var j = i - 1; j >= 0;)
            {
                if (token.IsCancellationRequested)
                    return;
                if (value < values[j])
                {
                    await values.Swap(j, j + 1);
                    j--;
                }
                else
                {
                    await values.Inspect(i, j);
                    break;
                }
            }
        }
    }
}