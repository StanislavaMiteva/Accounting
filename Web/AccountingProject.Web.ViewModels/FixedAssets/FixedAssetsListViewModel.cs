namespace AccountingProject.Web.ViewModels.FixedAssets
{
    using System.Collections.Generic;
    using System.Linq;

    public class FixedAssetsListViewModel
    {
        public IEnumerable<FixedAssetInListViewModel> FixedAssets { get; set; }

        public decimal TotalAmount => this.FixedAssets.Sum(x => x.Amount);
    }
}
