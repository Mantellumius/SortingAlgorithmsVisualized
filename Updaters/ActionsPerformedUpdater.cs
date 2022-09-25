using System.Threading.Tasks;
using Sorting_visualizer.MVVM.Model;

namespace Sorting_visualizer;

public class ActionsPerformedUpdater
{
    public static void Inspect(params int[] arg)
    {
        AlgorithmDataModel.AlgorithmDataModelInstance.AccessesCount += arg.Length;
    }

    public static async Task Swap(int i1, int i2)
    {
        AlgorithmDataModel.AlgorithmDataModelInstance.SwapsCount += 1;
        AlgorithmDataModel.AlgorithmDataModelInstance.AccessesCount += 2;
    }

    public static async Task StraightChange(int arg1, int arg2)
    {
        
    }
}