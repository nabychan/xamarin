using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Core.Droid
{
	[Activity (Label = "Core.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += (sender,e) => {
				button.Text = string.Format ("{0} clicks!", count++);
                button.Text += Newtonsoft.Json.JsonConvert.SerializeObject(new { Name = 1 });
			};
            button.Text ="登陆成功"+ new Core.Api.AccountApi().User("德帮制造", "101010ywq").data.access_token;     
		}
	}
}


