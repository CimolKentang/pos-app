using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Helpers;

namespace inovasyposmobile.Selectors
{
    public class FirstItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FirstItemTemplate { get; set; } = null!;
        public DataTemplate DefaultItemTemplate { get; set; } = null!;

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var collectionView = ItemsSourceHelper.GetCollectionView(container);
            var items = collectionView?.ItemsSource?.Cast<object>().ToList();
            int index = items?.IndexOf(item) ?? -1;

            return index == 0 ? FirstItemTemplate : DefaultItemTemplate;
        }
    }
}