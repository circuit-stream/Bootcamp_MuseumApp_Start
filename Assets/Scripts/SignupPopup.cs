using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MuseumApp
{
    public class SignupPopup : MonoBehaviour
    {
        private static readonly int ExitHash = Animator.StringToHash("Exit");

        public TMP_InputField usernameInput;
        public int minUsernameCharacters = 3;
        private Image usernameHolderImage;

        public TMP_InputField passwordInput;
        public int minPasswordCharacters = 8;
        private Image passwordHolderImage;

        public Color wrongInputFieldColor = new Color(1, 0.82f, 0.82f);

        public Animator animator;

        public void OnRegisterClicked()
        {
            var usernameValid = IsInputValid(usernameInput, minUsernameCharacters);
            var passwordValid = IsInputValid(passwordInput, minPasswordCharacters);

            usernameHolderImage.color = usernameValid ? Color.white : wrongInputFieldColor;
            passwordHolderImage.color = passwordValid ? Color.white : wrongInputFieldColor;

            if (!usernameValid || !passwordValid)
                return;

            // Register player
            Database.RegisterPlayer(usernameInput.text, passwordInput.text);

            Login();
            ClosePopup();
        }

        public void OnLoginClicked()
        {
            // Check credentials
            var user = Database.GetUser(usernameInput.text);
            if(user == null)
            {
                usernameHolderImage.color = wrongInputFieldColor;
                passwordHolderImage.color = Color.white;
            }
            else if(user.Password != passwordInput.text)
            {
                usernameHolderImage.color = Color.white;
                passwordHolderImage.color = wrongInputFieldColor;
            }
            else
            {

                Login();
                ClosePopup();
            }
        }

        private void Login()
        {
            User.Login(usernameInput.text);
        }

        private void ClosePopup()
        {
            animator.SetTrigger(ExitHash);
            FindObjectOfType<HomeScreen>().Refresh();
        }

        private void OnFinishedExitAnimation()
        {
            SceneManager.UnloadSceneAsync("SignupPopup");
        }

        private bool IsInputValid(TMP_InputField inputField, int minCharacters)
        {
            return !string.IsNullOrEmpty(inputField.text) && inputField.text.Length >= minCharacters;
        }

        private void Awake()
        {
            usernameHolderImage = usernameInput.GetComponent<Image>();
            passwordHolderImage = passwordInput.GetComponent<Image>();
        }
    }
}