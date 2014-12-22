using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace RemoteControllerTest
{
	public partial class FirstViewController : UIViewController
	{
		private UIPanGestureRecognizer _panGesture;


		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public FirstViewController (IntPtr handle) : base (handle)
		{
			this.Title = NSBundle.MainBundle.LocalizedString ("Coordinates", "Coordinates");
			this.TabBarItem.Image = UIImage.FromBundle ("first");
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
			valueLbl.Text = gesture.LocationOfTouch (0, View).ToString();

		}
}
}

