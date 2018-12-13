using kindle_viewer.Common;
using kindle_viewer.Misc;
using kindle_viewer.Repository;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kindle_viewer.pages {
    public class ProfileUserVM : BindableBase {
        private string name = "";
        private string email = "";
        private string avatar = "https://via.placeholder.com/300/FFFF00/000000?Text=avatar";
        private bool checkedMark = false;

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

        ProfileUserVM profile { get; set; } = new ProfileUserVM();

        public Profile() {
            this.InitializeComponent();

            this.loadUserProfile();
        }

        private async Task<bool> loadUserProfile() {

            ProfileResponse response = await (new Auth()).GetUserBy(Config.uid.ToString());

            profile.Name = response.user.name;
            profile.Email = response.user.email;
            profile.Avatar = response.user.avatar;
            profile.CheckedMark = response.user.checkedMark;
            
            return true;
        }
    }
}
