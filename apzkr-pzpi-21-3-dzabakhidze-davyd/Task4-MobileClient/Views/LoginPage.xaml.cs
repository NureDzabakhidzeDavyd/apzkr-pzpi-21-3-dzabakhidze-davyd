using CareWatch.Mobile.Models.Services;

namespace CareWatch.Mobile.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        var email = Email.Text;
        var password = Password.Text;

        var apiRepository = Application.Current.Handler.MauiContext.Services.GetService<AuthApiRepository>();
        var authenticatedResponse = await apiRepository.LoginAsync(email, password);

        if (authenticatedResponse != null)
        {
            // �������� ����� �������
            await apiRepository.SaveAccessTokenAsync(authenticatedResponse.Token);

            // ��������������� �� ������� ��������
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await DisplayAlert("Login failed", "Username or password is invalid", "Try again");
        }
    }
}