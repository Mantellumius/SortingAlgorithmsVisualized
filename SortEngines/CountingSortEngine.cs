using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sorting_visualizer.MVVM.Model;

namespace Sorting_visualizer.SortEngines;

public class CountingSortEngine : ISortEngine<int>
{
    public async Task Sort(ValuesModel<int> values, CancellationToken token)
    {
        var occurrences = new int[values.Max() + 1];
        for (var i = 0; i < values.Length; i++)
        {
            if (token.IsCancellationRequested)
                return;
            await values.Inspect(i);
            occurrences[values[i]]++;
        }

        for (int i = 0, j = 0; i < occurrences.Length; i++)
            while (occurrences[i]-- > 0)
            {
                if (token.IsCancellationRequested)
                    return;
                await values.Set(j++, i);
            }
    }
}