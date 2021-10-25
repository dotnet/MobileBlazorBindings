namespace HybridAuthSample
{
	[Register("AppDelegate")]
	public class AppDelegate : MauiUIApplicationDelegate
	{
		protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

		public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
		{
			if (Platform.OpenUrl(app, url, options))
				return true;

			return base.OpenUrl(app, url, options);
		}
	}
}