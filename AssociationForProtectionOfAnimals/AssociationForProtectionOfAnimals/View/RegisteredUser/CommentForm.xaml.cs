using AssociationForProtectionOfAnimals.Controller;
using AssociationForProtectionOfAnimals.Domain.Model;
using AssociationForProtectionOfAnimals.DTO;
using System.ComponentModel;
using System.Windows;
using System.Windows.Shapes;

namespace AssociationForProtectionOfAnimals.View.RegisteredUser
{
    public partial class CommentForm : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private CommentDTO? _comment;
        public CommentDTO? Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        private RegisteredUserDTO? _user;
        public RegisteredUserDTO? User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        private Post post;
        private Domain.Model.RegisteredUser? user;
        private CommentController _commentController;
        private PostController _postController;

        public CommentForm(Post post, Domain.Model.RegisteredUser? user)
        {
            InitializeComponent();
            DataContext = this;

            Comment = new CommentDTO();
            User = new RegisteredUserDTO(user);

            _commentController = Injector.CreateInstance<CommentController>();
            _postController = Injector.CreateInstance<PostController>(); 

            this.user = user;
            this.post = post;

            firstNameTextBlock.Text = user.FirstName;
            lastNameTextBlock.Text = user.LastName;
            emailTextBlock.Text = user.Account.Username;
            dateOfCommentTextBlock.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
        }

        public void CommentPost_Click(object sender, RoutedEventArgs e)
        {
            Comment comment = new Comment();
            comment.PostId = post.Id;
            comment.PersonEmail = user.Account.Username;
            comment.Content = contentTextBox.Text;
            comment.DateOfComment = DateTime.ParseExact(dateOfCommentTextBlock.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            _commentController.Add(comment);
            this.Close();
        }
    }
}
