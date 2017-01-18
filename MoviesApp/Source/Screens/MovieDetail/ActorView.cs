using System;
using UIKit;

namespace MoviesApp
{
    public class ActorView: UIView
    {
        private const int FontSize = 12;
        private const int LabelHeight = 18;

        private Actor Actor;
        private UIImageView ProfileImage;
        private UILabel CharacterText;
        private UILabel NameText;

        public ActorView(Actor actor)
        {
            this.Actor = actor;

            this.ProfileImage = new UIImageView()
            {
                Image = Resources.DefaultProfile(),
                ContentMode = UIViewContentMode.ScaleAspectFit
            };

            this.NameText = new UILabel()
            {
                Text = this.Actor.Name,
                TextAlignment = UITextAlignment.Center,
                Lines = 1,
                Font = UIFont.SystemFontOfSize(ActorView.FontSize)
            };

            this.CharacterText = new UILabel()
            {
                Text = this.Actor.Character,
                TextAlignment = UITextAlignment.Center,
                Lines = 1,
                Font = UIFont.SystemFontOfSize(ActorView.FontSize)
            };

            this.BackgroundColor = UIColor.White;
            this.AddSubview(this.ProfileImage);
            this.AddSubview(this.NameText);
            this.AddSubview(this.CharacterText);

            this.Actor.GetProfileImage().ContinueWith(task => InvokeOnMainThread(() =>
            {
                this.ProfileImage.Image = task.Result;
            }));
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            this.ProfileImage.Frame = new CoreGraphics.CGRect(
                0,
                0,
                this.Bounds.Width,
                this.Bounds.Height - 2 * ActorView.LabelHeight
            );
            this.NameText.Frame = new CoreGraphics.CGRect(
                0,
                this.ProfileImage.Frame.Bottom,
                this.Bounds.Width,
                ActorView.LabelHeight
            );
            this.CharacterText.Frame = new CoreGraphics.CGRect(
                0,
                this.ProfileImage.Frame.Bottom + ActorView.LabelHeight,
                this.Bounds.Width,
                ActorView.LabelHeight
            );
        }
    }
}
