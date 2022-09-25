using System.Threading.Tasks;
using Sorting_visualizer.MVVM.Model;

namespace Sorting_visualizer.Updaters;

public static class ActionsPerformedUpdater
{
    public static void Inspect(params int[] arg)
    {
        AlgorithmDataModel.AlgorithmDataModelInstance!.AccessesCount += arg.Length;
    }

    public static Task Swap(int i1, int i2)
    {
        AlgorithmDataModel.AlgorithmDataModelInstance!.SwapsCount += 1;
        return Task.CompletedTask;
    }

    public static void Set(int arg1, int arg2)
    {
        AlgorithmDataModel.AlgorithmDataModelInstance!.SetCount += 1;
    }
}