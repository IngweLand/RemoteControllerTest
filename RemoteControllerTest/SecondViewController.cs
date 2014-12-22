using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace RemoteControllerTest
{
	public partial class SecondViewController : UIViewController
	{
		private UIPanGestureRecognizer _panGesture;
		private PointF _previousLocation;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public SecondViewController (IntPtr handle) : base (handle)
		{
			this.Title = NSBundle.MainBundle.LocalizedString ("Direction", "Direction");
			this.TabBarItem.Image = UIImage.FromBundle ("second");
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			_panGesture = new UIPanGestureRecognizer(() => pan_handler(_panGesture));
			_panGesture.MaximumNumberOfTouches = 1;
			_panGesture.MinimumNumberOfTouches = 1;
			View.AddGestureRecognizer(_panGesture);
		}
		#endregion

		private void pan_handler(UIPanGestureRecognizer gesture)
		{
			if (gesture.NumberOfTouches != 1) {
				return;
			}
			PointF curLocation = gesture.LocationOfTouch (0, View);
			var deltaX = _previousLocation.X - curLocation.X;
			var deltaY = _previousLocation.Y - curLocation.Y;

			if (deltaX < 0) {
				if (deltaY > 0) {
					valueLbl.Text = "Top right";
				} else {
					valueLbl.Text = "Bottom right";
				}
			} else {
				if (deltaY > 0) {
					valueLbl.Text = "Top left";
				} else {
					valueLbl.Text = "Bottom left";
				}
			}
			_previousLocation = curLocation;

}

		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);

			UITouch t = (UITouch)touches.AnyObject;
			if (t != null) {
				_previousLocation = t.LocationInView (View);
			}
		}
	}
}

