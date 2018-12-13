using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using kindle_viewer.pages;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace kindle_viewer.Components
{

    public sealed partial class ProfileInfoUserControl : UserControl
    {
        public ProfileUserVM ProfileInfoData {
            get { return (ProfileUserVM)GetValue(profileInfoDataProperty); }
            set { SetValue(profileInfoDataProperty, value); }
        }

        public static readonly DependencyProperty profileInfoDataProperty =
            DependencyProperty.Register("profileInfoData", typeof(ProfileUserVM), typeof(ProfileInfoUserControl), null);

        public ProfileInfoUserControl()
        {
            this.InitializeComponent();
        }
    }
}
