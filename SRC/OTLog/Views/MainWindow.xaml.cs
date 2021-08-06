using Microsoft.Toolkit.Uwp.Notifications;
using OTLog.Core.StaticObjects;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Windows.Foundation.Collections;

namespace OTLog.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        public static void ShowToast()
        {
            //ToastNotificationManagerCompat.History.Clear();
            //var inlineUri = new Uri("E:\\Personal\\Windows10Notification\\Windows10Notification\\bin\\Debug\\net5.0-windows10.0.17763.0\\WallHaven.jpg");
            //var inlineUri = new Uri("D:\\Document\\Media\\Image\\GIF\\TextBoxFocus.gif");
            var inlineUri = new Uri(Path.GetFullPath("./Assets/Images/toastinline800.gif"));
            //var heroImageUri = new Uri("E:\\Personal\\Windows10Notification\\Windows10Notification\\bin\\Debug\\net5.0-windows10.0.17763.0\\WallHaven.jpg");
            var appLogoUri = new Uri(Path.GetFullPath("./Assets/Images/timer.png"));

            int conversationId = 384928;

            // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
            new ToastContentBuilder().AddArgument("conversationId", conversationId)
                                     .AddText("TextA")
                                     .AddText("TextB")
                                     .AddAttributionText("TimeRecorder")
                                     .AddInlineImage(inlineUri)
                                     //.AddAppLogoOverride(appLogoUri, ToastGenericAppLogoCrop.Circle)
                                     .AddAppLogoOverride(appLogoUri, ToastGenericAppLogoCrop.Default)
                                     //.AddHeroImage(heroImageUri)
                                     .AddToastInput(BuildToastSelectionBox())
                                     .AddInputTextBox("tbReply", placeHolderContent: "回复:")
                                     .AddButton(new ToastButton().SetContent("Reply")
                                                                 .AddArgument("action", "reply")
                                                                 .SetBackgroundActivation())
                                     .AddButton(new ToastButton().SetContent("Like")
                                                                 .AddArgument("action", "like")
                                                                 .SetBackgroundActivation())
                                     .AddButton(new ToastButton().SetContent("View")
                                                                 .AddArgument("action", "viewImage")
                                                                 .AddArgument("imageUrl", "imageUrl"))
                                     .AddButton(new ToastButtonSnooze())
                                     .Show();

            ToastSelectionBox BuildToastSelectionBox()
            {
                var selectionBox = new ToastSelectionBox("SelectionBox");
                selectionBox.Title = "SelectionBox";
                selectionBox.Items.Add(new ToastSelectionBoxItem("Item1", "Item A"));
                selectionBox.Items.Add(new ToastSelectionBoxItem("Item2", "Item B"));
                return selectionBox;
            }
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();

            if (!GlobalObjectHolder.Config.NotificationAfterMin) return;

            ShowToast();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.AppArgs?.Contains("--nowindow") ?? false)
            {
                await Task.Delay(10);
                Hide();
            }

            // 点击通知时, 激活程序(即使程序已关闭)
            // Listen to notification activation
            ToastNotificationManagerCompat.OnActivated += toastArgs =>
            {
                // Obtain the arguments from the notification
                ToastArguments args = ToastArguments.Parse(toastArgs.Argument);

                // Obtain any user input (text boxes, menu selections) from the notification
                ValueSet userInput = toastArgs.UserInput;
                // 文本框内容
                var textBoxContent = userInput["tbReply"].ToString();
                // Need to dispatch to UI thread if performing UI operations
                Application.Current.Dispatcher.Invoke(delegate
                {
                    // TODO: Show the corresponding content
                    MessageBox.Show("Toast activated. Args: " + toastArgs.Argument);
                });
            };
        }
    }
}
