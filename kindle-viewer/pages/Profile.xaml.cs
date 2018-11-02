using kindle_viewer.Common;
using kindle_viewer.Repository;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kindle_viewer.pages {
    class ProfileUserVM : BindableBase {
        private string name;
        private string email;
        private string avatar;
        private bool checkedMark;

        public string Name {
            get { return this.name; }
            set => this.SetProperty(ref this.name, value);
        }

        public string Email {
            get { return this.email; }
            set {
                this.SetProperty(ref this.email, value);
            }
        }
        public string Avatar {
            get { return this.avatar; }
            set {
                this.SetProperty(ref this.avatar, value);
            }
        }
        public bool CheckedMark {
            get { return this.checkedMark; }
            set {
                this.SetProperty(ref this.checkedMark, value);
            }
        }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Profile : Page {

        ProfileUserVM profile { get; set; }

        public Profile() {
            this.InitializeComponent();

            this.profile = new ProfileUserVM();

            this.loadUserProfile();
        }

        private async Task<bool> loadUserProfile() {

            User user = await (new Auth()).GetUserBy("1");

            profile.Name = user.name;
            profile.Email = user.email;
            profile.Avatar = user.avatar;
            profile.CheckedMark = user.checkedMark;
            
            return true;
        }
    }
}
