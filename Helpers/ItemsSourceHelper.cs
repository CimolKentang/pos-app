using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.Helpers
{
    public static class ItemsSourceHelper
    {
        public static CollectionView? GetCollectionView(BindableObject itemContainer)
        {
            Element? parent = itemContainer as Element;

            while (parent != null)
            {
                if (parent is CollectionView collectionView)
                    return collectionView;

                parent = parent.Parent;
            }

            return null;
        }
    }
}