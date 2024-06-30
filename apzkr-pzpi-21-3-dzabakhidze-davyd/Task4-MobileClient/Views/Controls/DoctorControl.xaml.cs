namespace CareWatch.Mobile.Views.Controls;

public partial class DoctorControl : ContentView
{
    public DoctorControl()
    {
        InitializeComponent();
    }

    public string FirstName
    {
        get => entryFirstName.Text;
        set => entryFirstName.Text = value;
    }

    public string LastName
    {
        get => entryLastName.Text;
        set => entryLastName.Text = value;
    }

    public string MiddleName
    {
        get => entryMiddleName.Text;
        set => entryMiddleName.Text = value;
    }

    public string Phone
    {
        get => entryPhoneNumber.Text;
        set => entryPhoneNumber.Text = value;
    }

    public string Email
    {
        get => entryEmail.Text;
        set => entryEmail.Text = value;
    }

    public string Address
    {
        get => entryAddress.Text;
        set => entryAddress.Text = value;
    }

    public DateTime DateOfBirth
    {
        get => datePickerDateOfBirth.Date;
        set => datePickerDateOfBirth.Date = value;
    }

    public string Gender
    {
        get => pickerGender.SelectedItem?.ToString();
        set => pickerGender.SelectedItem = value;
    }

    public event EventHandler SaveButtonClicked;

    public event EventHandler<string> OnError;

    private void btnSave_Clicked(object sender, EventArgs e)
    {
        if (emailValidator.IsNotValid)
        {
            foreach (var error in emailValidator.Errors)
            {
                OnError?.Invoke(this, error.ToString());
            }
            return;
        }

        SaveButtonClicked?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler CancelButtonClicked;

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        CancelButtonClicked?.Invoke(this, EventArgs.Empty);
    }
}