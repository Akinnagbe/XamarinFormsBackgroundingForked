﻿using System;
using Xamarin.Forms;
using FormsBackgrounding.Messages;

namespace FormsBackgrounding
{
	public partial class BackgroundPage : ContentPage
	{
		public BackgroundPage ()
		{
			InitializeComponent ();

			longRunningTask.Clicked += StartLongRunningTask;
			stopLongRunningTask.Clicked += StopLongRunningTask;
			download.Clicked += Download;

			MessagingCenter.Subscribe<TickedMessage> (this, "TickedMessage", message => {
				Device.BeginInvokeOnMainThread(() => {
					ticker.Text = message.Message.ToString ();
				});
			});

			MessagingCenter.Subscribe<DownloadProgressMessage> (this, "DownloadProgressMessage", message => {
				Device.BeginInvokeOnMainThread(() => {
					this.downloadStatus.Text = message.Percentage.ToString("P2");
				});
			});

			MessagingCenter.Subscribe<DownloadFinishedMessage> (this, "DownloadFinishedMessage", message => {
				Device.BeginInvokeOnMainThread(() =>
				{
				    catImage.Source = FileImageSource.FromFile(message.FilePath);
				});
			});
		}

		private void StartLongRunningTask (object sender, EventArgs e)
		{
			var message = new StartLongRunningTaskMessage ();
			MessagingCenter.Send (message, "StartLongRunningTaskMessage");
		}

		private void StopLongRunningTask (object sender, EventArgs e)
		{
			var message = new StopLongRunningTaskMessage ();
			MessagingCenter.Send (message, "StopLongRunningTaskMessage");
		}

		void Download (object sender, EventArgs e)
		{
		    this.catImage.Source = null;
			var message = new DownloadMessage {
				Url = "http://xamarinuniversity.blob.core.windows.net/ios210/huge_monkey.png"
			};

			MessagingCenter.Send (message, "Download");
		}
	}
}