using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Sorting_visualizer.MVVM.Model;
using Sorting_visualizer.SortEngines;

namespace Sorting_visualizer
{
    public partial class MainWindow : Window
    {
        private CancellationTokenSource? _tokenSource;
        private ValuesModel<int> _valuesModel = new(0);
        private ISortEngine<int>? _sortEngine;


        public static bool IsLocked { get; set; }
        public static MainWindow MainWindowInstance { get; private set; } = null!;

        public MainWindow()
        {
            InitializeComponent();
            SubscribeActionsPerformedUpdater();
            SubscribeArrayView();
            MainWindowInstance = this;
        }

        private void RandomizeArray()
        {
            if (IsLocked || ArrayCanvas == null)
                return;
            ArrayCanvas.Children.Clear();
            var maxValue = (int)ArrayCanvas.ActualHeight;
            var arraySize = AlgorithmDataModel.AlgorithmDataModelInstance.ArrayLength;
            var batWidth = ArrayCanvas.ActualWidth / arraySize;
            var random = new Random();
            _valuesModel = new ValuesModel<int>(arraySize);
            for (var i = 0; i < arraySize; i++)
            {
                _valuesModel[i] = random.Next(1, maxValue);
                var bar = new Rectangle
                {
                    Fill = ArrayViewUpdater.BaseColor,
                    Width = batWidth,
                    Height = _valuesModel[i],
                };
                ArrayCanvas.Children.Add(bar);
                Canvas.SetLeft(bar, i * batWidth);
                Canvas.SetTop(bar, 0);
            }

            _valuesModel.IsCountingEnabled = true;
        }

        #region Subscriptions

        private void SubscribeActionsPerformedUpdater()
        {
            UtilityFunctions.OnSwap += ActionsPerformedUpdater.Swap;
            UtilityFunctions.OnStarightChange += ActionsPerformedUpdater.StraightChange;
        }

        private void UnsubscribeActionsPerformedUpdater()
        {
            UtilityFunctions.OnSwap -= ActionsPerformedUpdater.Swap;
            UtilityFunctions.OnStarightChange -= ActionsPerformedUpdater.StraightChange;
        }

        private void SubscribeArrayView()
        {
            UtilityFunctions.OnSwap += ArrayViewUpdater.Swap;
            UtilityFunctions.OnInspect += ArrayViewUpdater.Inspect;
            UtilityFunctions.OnStarightChange += ArrayViewUpdater.StraightChange;
        }

        private void UnsubscribeArrayView()
        {
            UtilityFunctions.OnSwap -= ArrayViewUpdater.Swap;
            UtilityFunctions.OnInspect -= ArrayViewUpdater.Inspect;
            UtilityFunctions.OnStarightChange -= ArrayViewUpdater.StraightChange;
        }

        #endregion

        #region UserInterface

        private void DragWithHeader(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RandomizeArray();
        }

        private void RandomizeArrayButton_OnClick(object sender, RoutedEventArgs e)
        {
            RandomizeArray();
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            _tokenSource?.Cancel();
        }

        private void StartSorting_OnClick(object sender, RoutedEventArgs e)
        {
            if (_valuesModel.Length < 1 || IsLocked)
                return;
            AlgorithmDataModel.AlgorithmDataModelInstance.AccessesCount = 0;
            AlgorithmDataModel.AlgorithmDataModelInstance.SwapsCount = 0;
            IsLocked = true;
            _tokenSource = new CancellationTokenSource();
            async Task Task() => await _sortEngine?.Sort(_valuesModel, _tokenSource.Token)!;
            Task().ContinueWith(_ => { IsLocked = false; });
        }

        #endregion

        #region SortAlgorithmSelector

        private void BubbleSort_OnSelected(object sender, RoutedEventArgs e)
        {
            _sortEngine = new BubbleSortEngine();
        }

        private void QuickSort_OnSelected(object sender, RoutedEventArgs e)
        {
            _sortEngine = new QuickSortEngine();
        }

        private void SelectionSort_OnSelected(object sender, RoutedEventArgs e)
        {
            _sortEngine = new SelectionSortEngine();
        }

        private void InsertionSort_OnSelected(object sender, RoutedEventArgs e)
        {
            _sortEngine = new InsertionSortEngine();
        }

        private void HeapSort_OnSelected(object sender, RoutedEventArgs e)
        {
            _sortEngine = new HeapSortEngine();
        }

        private void MergeSort_OnSelected(object sender, RoutedEventArgs e)
        {
            _sortEngine = new MergeSortEngine();
        }

        private void CountingSort_OnSelected(object sender, RoutedEventArgs e)
        {
            _sortEngine = new CountingSortEngine();
        }


        private void RadixSort_OnSelected(object sender, RoutedEventArgs e)
        {
            _sortEngine = new RadixSortEngine();
        }

        private void BucketSort_OnSelected(object sender, RoutedEventArgs e)
        {
            _sortEngine = new BucketSortEngine();
        }

        #endregion

        private void InspectUpDown_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            DelaysModel.DelaysModelInstance.InspectDelay = e.NewValue;
        }

        private void SwapUpDown_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            DelaysModel.DelaysModelInstance.SwapDelay = e.NewValue;
        }

        private void StraightChangeUpDown_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            DelaysModel.DelaysModelInstance.StraightChange = e.NewValue;
        }
    }
}