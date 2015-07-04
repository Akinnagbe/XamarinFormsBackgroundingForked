﻿using Android.App;
using Android.Content.PM;
using Android.OS;

namespace FormsBackgrounding.Droid
{
	[Activity (Label = "FormsBackgrounding.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		FormsBackgrounding.App _app;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			App.Current.ConnectionChanged += (sender, e) => {
				if (e.IsConnected) {
					ILongRunningTaskExample longRunningTaskExample = App.Current.Service;
					//ILongRunningTaskExample longRunningTaskExample = new DroidLongRunningTaskExample(this);
					_app = new FormsBackgrounding.App (longRunningTaskExample);
					LoadApplication (_app);
				}
			};


//			App.Current.ConnectionChanged += (sender, e) => 
//			{
//				if (e.IsConnected) {
//					App.Current.Service.Ticked += OnTick;
//				}
//				else {
//					App.Current.Service.Ticked -= OnTick;
//				}
//			};
		}

		void OnTick (object sender, TickedEventArgs e)
		{
			
			var x = e.TickCounter;
		}
	}
}