using Android.OS;
using Android.Views;
using ElementPlayer.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace ElementPlayer.Android.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
    public class BrowseFragment : BaseFragment<BrowseViewModel>
    {
        protected override int FragmentId => Resource.Layout.browse_fragment;
    }
}
