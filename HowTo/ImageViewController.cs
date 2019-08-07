using Foundation;
using System;
using UIKit;

namespace HowTo
{
    public partial class ImageViewController : UIViewController
    {
        public ImageViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //FeaturedImage.Image = Message;
            //containerView.Layer.CornerRadius = kCornerRadius;
            var tapGesture = new UITapGestureRecognizer();
            tapGesture.AddTarget((NSObject obj) =>
            {
                DismissViewController(true, null);
            });
            View.AddGestureRecognizer(tapGesture);
        }
    }
}