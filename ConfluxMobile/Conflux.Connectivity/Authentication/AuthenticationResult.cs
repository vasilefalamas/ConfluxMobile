namespace Conflux.Connectivity.Authentication
{
    public class AuthenticationResult
    {
        public AuthenticationState State { get; private set; }

        public AccessToken AccessToken { get; private set; }

        public AuthenticationResult(AuthenticationState state, AccessToken accessToken)
        {
            State = state;
            AccessToken = accessToken;
        }
    }
}
