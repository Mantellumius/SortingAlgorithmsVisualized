using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using Sorting_visualizer.MVVM.Model;

namespace Sorting_visualizer.Updaters;

public static class ArrayViewUpdater
{
    public static readonly Brush BaseColor = Brushes.Cornsilk;
    public static readonly Brush InspectColor = Brushes.Chocolate;
    public static readonly Brush SwapColor = Brushes.Blue;
    public static readonly Brush ChangePerformedColor = Brushes.Crimson;


    public static async Task Swap(int i1, int i2)
    {
        ChangeBarColor(SwapColor, i1, i2);
        if (DelaysModel.DelaysModelInstance!.SwapDelay > 0)
            await Task.Delay(DelaysModel.DelaysModelInstance.SwapDelay);
        var bar1 = (Rectangle)MainWindow.MainWindowInstance.ArrayCanvas?.Children[i1]!;
        var bar2 = (Rectangle)MainWindow.MainWindowInstance.ArrayCanvas?.Children[i2]!;
        (bar1.Height, bar2.Height) = (bar2.Height, bar1.Height);
        ChangeBarColor(BaseColor, i1, i2);
    }

    public static async Task Set(int i, int height)
    {
        ChangeBarColor(ChangePerformedColor, i);
        if (DelaysModel.DelaysModelInstance!.SetDelay > 0)
            await Task.Delay(DelaysModel.DelaysModelInstance.SetDelay);
        var bar1 = (Rectangle)MainWindow.MainWindowInstance.ArrayCanvas?.Children[i]!;
        bar1.Height = height;
        ChangeBarColor(BaseColor, i);
    }

    public static async Task Inspect(params int[] indexes)
    {
        foreach (var index in indexes)
            ChangeBarColor(InspectColor, index);
        if (DelaysModel.DelaysModelInstance!.InspectDelay > 0)
            await Task.Delay(DelaysModel.DelaysModelInstance.InspectDelay);
        foreach (var index in indexes)
            ChangeBarColor(BaseColor, index);
    }

    private static void ChangeBarColor(Brush color, params int[] indexes)
    {
        foreach (var index in indexes)
            ((Rectangle)MainWindow.MainWindowInstance.ArrayCanvas?.Children[index]!).Fill = color;
    }
}