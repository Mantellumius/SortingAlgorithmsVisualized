using System.Threading;
using System.Threading.Tasks;
using Sorting_visualizer.MVVM.Model;

namespace Sorting_visualizer.SortEngines;

public interface ISortEngine<T>
{
    public Task Sort(ValuesModel<T> values, CancellationToken token);
}