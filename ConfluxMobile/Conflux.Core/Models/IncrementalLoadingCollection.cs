using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Conflux.Core.Models
{
    
    //TODO Give up at interface
    public class IncrementalLoadingCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading 
    {
        private IIncrementalSource<T> collectionSource;
        
        private bool hasMoreItems;

        private uint offset;

        private readonly uint pageSize;

        public IncrementalLoadingCollection(IIncrementalSource<T> collectionSource, uint pageSize = 25)
        {
            this.collectionSource = collectionSource; 
            hasMoreItems = true;
            this.pageSize = pageSize;
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            var dispatcher = Window.Current.Dispatcher;

            return Task.Run(
                async () =>
                {
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                    {
                        var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();

                        statusBar.ProgressIndicator.Text = "Loading more events...";
                        await statusBar.ProgressIndicator.ShowAsync();
                    });

                    uint resultCount = 0;

                    var result = (await collectionSource.GetPagedItems(offset, pageSize)).ToList();
                    
                    if (!result.Any())
                    {
                        hasMoreItems = false;
                    }
                    else
                    {
                        resultCount = (uint) result.Count();

                        offset += resultCount;

                        await dispatcher.RunAsync(
                            CoreDispatcherPriority.Normal,
                            () =>
                            {
                                foreach (T item in result)
                                {
                                    Add(item);
                                }
                            });
                    }

                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                    {
                        var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();

                        await statusBar.ProgressIndicator.HideAsync();
                    });

                    return new LoadMoreItemsResult
                    {
                        Count = resultCount
                    };

                }).AsAsyncOperation();
        }

        public bool HasMoreItems
        {
            get { return hasMoreItems; }
        }
    }
}
