using Linn;
using Android.App;
using Android.Content;
using Android.OS;
using Android;
using Android.Views;
using System.Collections.Generic;
using System;
using Android.Widget;
using Android.Text.Method;
using System.Threading;
namespace OssToolkitDroid
{

    public class CrashDumper : ICrashLogDumper
    {
        public CrashDumper(Context aContext,
                           int aNotificationIconResource, 
                           Helper aHelper,
                           OptionPageCrashDumper aOptionPage)
        {
            iContext = aContext;
            iNotificationIconResource = aNotificationIconResource;
            iHelper = aHelper;
            iOptionPage = aOptionPage;
        }

        #region ICrashLogDumper Members

        public void Dump(CrashLog aCrashLog)
        {
            try
            {
                if (iOptionPage.AutoSend)
                {
                    Send(aCrashLog.ToString(), iHelper.Title, iHelper.Version, iHelper.Title);
                }
                else
                {
                    NotificationManager notificationManager = (NotificationManager)iContext.GetSystemService(Context.NotificationService);
                    Notification notification = new Notification(iNotificationIconResource, string.Format("{0} has quit unexpectedly.", iHelper.Title));
                    Intent notificationIntent = new Intent(iContext, typeof(CrashReportActivity));
                    Bundle b = new Bundle();
                    b.PutString(kCrashLogData, aCrashLog.ToString());
                    b.PutInt(kCrashLogImage, iNotificationIconResource);
                    notificationIntent.PutExtras(b);
                    PendingIntent pendingIntent = PendingIntent.GetActivity(iContext, 0, notificationIntent, PendingIntentFlags.UpdateCurrent);
                    notification.SetLatestEventInfo(iContext, string.Format("{0} has quit unexpectedly.", iHelper.Title), string.Format("{0} has encountered a problem and had to close.", iHelper.Title), pendingIntent);
                    notificationManager.Notify((int)ENotificationType.SystemCrash, notification);
                }
            }
            catch (Exception ex)
            {
                UserLog.WriteLine("Unhandled exception in CrashDumper.Dump: " + ex);
            }
        }

        #endregion

        public static void Send(string aCrashLog, string aProduct, string aVersion, string aTitle)
        {
            Thread t = new Thread(new ThreadStart(() =>
            {
                UserLog.WriteLine("Sending crash log...");
                DebugReport report = new DebugReport("Crash log generated by " + aProduct + " ver " + aVersion);
                report.Post(aProduct, aCrashLog);
            }));
            t.Start();
        }

        private Context iContext;
        private int iNotificationIconResource;
        public const string kCrashLogData = "CrashLogData";
        public const string kCrashLogImage = "CrashLogImage";
        private Helper iHelper;
        private OptionPageCrashDumper iOptionPage;
    }

    [Activity(Label = "Linn Crash Report")]
    public class CrashReportActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                
                base.OnCreate(bundle);

                // this activity is running in a new process, cannot parcel an existing helper without making Helper an IParcelable, 
                // so we create a new one to obtain the crash dump option                
                Helper helper = new Helper(new string[] { });
                OptionPageCrashDumper optionPage = new OptionPageCrashDumper("Crash Logs");
                helper.AddOptionPage(optionPage);
                helper.ProcessOptionsFileAndCommandLine();

                // also need to set up log listeners again for same reason
                UserLog.AddListener(new AndroidUserLogListener());
                Linn.Trace.AddListener(new AndroidTraceListener());

                // cancel crash notification
                NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
                notificationManager.Cancel((int)ENotificationType.SystemCrash);
                string crashLog = Intent.GetStringExtra(CrashDumper.kCrashLogData);

                // log to adb logcat
                UserLog.WriteLine("CrashDump: " + crashLog);

                // setup activity UI
                //TODO: i18n
                this.Title = string.Format("{0} has quit unexpectedly.", helper.Product);
                
                LinearLayout rootLayout = new LinearLayout(this);
                rootLayout.Orientation = Orientation.Vertical;

                LinearLayout messagePanel = new LinearLayout(this);
                messagePanel.Orientation = Orientation.Horizontal;


                LinearLayout.LayoutParams layoutParamsImg = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
                layoutParamsImg.SetMargins(5, 5, 5, 5);
                ImageView image = new ImageView(this);
                image.SetImageResource(Intent.GetIntExtra(CrashDumper.kCrashLogImage, 0));
                messagePanel.AddView(image, layoutParamsImg);

                LinearLayout.LayoutParams layoutParamsMessage = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
                layoutParamsMessage.SetMargins(5, 5, 5, 5);
                TextView message = new TextView(this);
                message.Text = string.Format("{0} encountered a problem and had to close.  We are sorry for the inconvenience", helper.Product);
                messagePanel.AddView(message, layoutParamsMessage);

                rootLayout.AddView(messagePanel);

                LinearLayout.LayoutParams layoutParamsMessage2 = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
                layoutParamsMessage2.SetMargins(5, 5, 5, 5);
                TextView message2 = new TextView(this);
                message2.Text = "Please submit a crash report to Linn to help us fix this problem for future versions.";
                rootLayout.AddView(message2, layoutParamsMessage2);

                RelativeLayout buttonsPanel = new RelativeLayout(this);
                buttonsPanel.Id = 1; // need an id for relative layout references

                // don't send button
                RelativeLayout.LayoutParams dontSendButtonLayoutParams = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, RelativeLayout.LayoutParams.WrapContent);
                dontSendButtonLayoutParams.SetMargins(5, 5, 5, 5);
                dontSendButtonLayoutParams.AddRule(LayoutRules.AlignParentTop, buttonsPanel.Id);
                dontSendButtonLayoutParams.AddRule(LayoutRules.AlignParentRight, buttonsPanel.Id);
                Button dontSendButton = new Button(this);
                dontSendButton.Text = "Don't Send";
                dontSendButton.Id = 2; // need an id for relative layout references
                buttonsPanel.AddView(dontSendButton, dontSendButtonLayoutParams);

                // send button
                RelativeLayout.LayoutParams sendButtonLayoutParams = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, RelativeLayout.LayoutParams.WrapContent);
                sendButtonLayoutParams.SetMargins(5, 5, 5, 5);
                sendButtonLayoutParams.AddRule(LayoutRules.AlignParentTop, buttonsPanel.Id);
                sendButtonLayoutParams.AddRule(LayoutRules.LeftOf, dontSendButton.Id);
                Button sendButton = new Button(this);
                sendButton.Text = "Send";
                buttonsPanel.AddView(sendButton, sendButtonLayoutParams);

                // auto send checkbox
                RelativeLayout.LayoutParams autoSendLayoutParams = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, RelativeLayout.LayoutParams.WrapContent);
                autoSendLayoutParams.SetMargins(5, 5, 5, 5);
                autoSendLayoutParams.AddRule(LayoutRules.Below, dontSendButton.Id);
                autoSendLayoutParams.AddRule(LayoutRules.AlignParentRight, buttonsPanel.Id);
                CheckBox autoSend = new CheckBox(this);
                autoSend.Checked = optionPage.AutoSend;
                autoSend.Text = "Automatically send crash reports in future";
                buttonsPanel.AddView(autoSend, autoSendLayoutParams);


                // details button
                RelativeLayout.LayoutParams detailsButtonLayoutParams = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, RelativeLayout.LayoutParams.WrapContent);
                detailsButtonLayoutParams.SetMargins(5, 5, 5, 5);
                detailsButtonLayoutParams.AddRule(LayoutRules.AlignParentTop, buttonsPanel.Id);
                detailsButtonLayoutParams.AddRule(LayoutRules.AlignParentLeft, buttonsPanel.Id);
                Button detailsButton = new Button(this);
                detailsButton.Text = "Show Details...";
                buttonsPanel.AddView(detailsButton, detailsButtonLayoutParams);

                // scrolling textbox for crash details
                ScrollView scrollView = new ScrollView(this);
                scrollView.Visibility = ViewStates.Invisible;
                TextView text = new TextView(this);
                text.Text = crashLog;
                scrollView.AddView(text);


                rootLayout.AddView(buttonsPanel);
                rootLayout.AddView(scrollView);
                SetContentView(rootLayout);


                sendButton.Click += (sender, args) =>
                {
                    CrashDumper.Send(crashLog, helper.Product, helper.Version, helper.Title);
                    Finish();
                };

                dontSendButton.Click += (sender, args) =>
                {
                    Finish();
                };

                autoSend.CheckedChange += (sender, args) =>
                {
                    optionPage.AutoSend = autoSend.Checked;
                };

                detailsButton.Click += (sender, args) =>
                {
                    if (scrollView.Visibility == ViewStates.Invisible)
                    {
                        detailsButton.Text = "Hide Details...";
                        scrollView.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        detailsButton.Text = "Show Details...";
                        scrollView.Visibility = ViewStates.Invisible;
                    }
                };

            }
            catch (Exception ex)
            {
                UserLog.WriteLine("CrashReportActivity.OnCreate:: " + ex);
            }
        }
    }

}