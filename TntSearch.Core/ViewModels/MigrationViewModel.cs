using MvvmCross.ViewModels;

namespace TntSearch.Core.ViewModels
{
    public class MigrationViewModel : MvxViewModel<bool>
    {
        private bool _dataMigration;

        public override void Prepare(bool parameter)
        {
            _dataMigration = parameter;
        }
    }
}
