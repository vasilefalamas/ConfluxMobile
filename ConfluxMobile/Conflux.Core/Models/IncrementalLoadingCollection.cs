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

        private uint offset;

        private readonly uint pageSize;

        public IncrementalLoadingCollection(IIncrementalSource<T> collectionSource, uint pageSize = 25)
        {
            this.collectionSource = collectionSource; 
            HasMoreItems = true;
            this.pageSize = pageSize;
        }

        public bool HasMoreItems { get; private set; }

        public event Action LoadMoreItemsStarted;

        public event Action LoadMoreItemsCompleted;

        protected virtual void OnLoadMoreItemsStarted()
        {
            var handler = LoadMoreItemsStarted;
            if (handler != null)
            {
                handler();
            }
        }

        protected virtual void OnLoadMoreItemsCompleted()
        {
            var handler = LoadMoreItemsCompleted;
            if (handler != null)
            {
                handler();
            }
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            var dispatcher = Window.Current.Dispatcher;

            return Task.Run(
                async () =>
                {
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, OnLoadMoreItemsStarted);

                    uint resultCount = 0;

                    var result = (await collectionSource.GetPagedItems(offset, pageSize)).ToList();
                    
                    if (!result.Any())
                    {
                        HasMoreItems = false;
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

                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, OnLoadMoreItemsCompleted);

                    return new LoadMoreItemsResult
                    {
                        Count = resultCount
                    };

                }).AsAsyncOperation();
        }
    }
}
