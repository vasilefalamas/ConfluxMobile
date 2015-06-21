using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Media.SpeechRecognition;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Conflux.Connectivity;
using Conflux.Connectivity.Authentication;
using Conflux.Connectivity.GraphApi;
using Conflux.Core.Settings;
using Conflux.UI.Views;

namespace Conflux.UI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App
    {
        private TransitionCollection transitions;
        
        public static IFacebookClient FacebookClient { get; private set; }
        
        public static User User { get; set; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;

            RequestedTheme = ApplicationTheme.Light;
            
            User = new User();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            await RegisterVoiceCommands();

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame
                {
                    CacheSize = 2,
                    Background = new SolidColorBrush(Colors.WhiteSmoke)
                };
                
                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // Removes the turnstile navigation for startup.
                if (rootFrame.ContentTransitions != null)
                {
                    transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions  = new TransitionCollection();

                //Facebook Authentication check. Navigate to next page accordingly.
                Type startPage;

                //TODO : Extract method GetStartPage()
                var hasAcceptedTermsOfuse = AppSettings.GetTermsOfUseAcceptance();
                if (!hasAcceptedTermsOfuse)
                {
                    startPage = typeof (TermsOfUsePage);
                }
                else
                {
                    //var savedAccessToken = AppSettings.GetAccessToken();

                    startPage = InitializeFacebookClient() ? typeof (LoadingPage) : typeof(LoginPage);

                    //if (savedAccessToken != null && savedAccessToken.Expiry > DateTime.Now)
                    //{
                    //    FacebookClient = new FacebookClient(savedAccessToken);

                    //    startPage = typeof (LoadingPage);
                    //}
                    //else
                    //{
                    //    startPage = typeof (LoginPage);
                    //}
                }
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(startPage, e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);

            if (args.Kind == ActivationKind.Protocol)
            {
                var eventArgs = args as ProtocolActivatedEventArgs;

                if (eventArgs != null)
                {
                    Uri responseUri = eventArgs.Uri;

                    var freshAcessToken = AccessToken.Create(responseUri.Query);
                    StoreAccessToken(freshAcessToken);
                    FacebookClient = new FacebookClient(freshAcessToken);

                    ContinueNavigation(typeof (LoadingPage));
                }
            }
            else if (args.Kind == ActivationKind.VoiceCommand)
            {
                   ContinueNavigation(typeof(LoadingPage)); 
            } 
            else
            {
                ContinueNavigation(typeof(LoginPage));
            }

            Window.Current.Activate();

        }

        private static void ContinueNavigation(Type navigationPage)
        {
            var rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame
                {
                    Background = new SolidColorBrush(Colors.WhiteSmoke),
                    CacheSize = 2
                };
                Window.Current.Content = rootFrame;
            }

            rootFrame.Navigate(navigationPage);
        }

        private static bool InitializeFacebookClient()
        {
            var storedAccessToken = AppSettings.GetAccessToken();

            if (storedAccessToken != null && storedAccessToken.Expiry > DateTime.Now)
            {
                FacebookClient = new FacebookClient(storedAccessToken);
                return true;
            }

            return false;
        }

        private void StoreAccessToken(AccessToken accessToken)
        {
            AppSettings.SetAccessToken(accessToken);
        }
        
        private async Task RegisterVoiceCommands()
        {
            try
            {
                var voiceCommandUrl = new Uri("ms-appx:///VoiceCommands/CortanaVoiceCommand.xml");

                var voiceCommandsFile = await StorageFile.GetFileFromApplicationUriAsync(voiceCommandUrl);

                await VoiceCommandManager.InstallCommandSetsFromStorageFileAsync(voiceCommandsFile);
            }
            catch (Exception)
            {
                
            }
        }
    }
}