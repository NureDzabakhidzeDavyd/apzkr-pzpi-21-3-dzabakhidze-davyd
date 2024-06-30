namespace CareWatch.Mobile.Views.Controls;

    public partial class EmergencyRequestControl : ContentView
    {
        public EmergencyRequestControl()
        {
            InitializeComponent();
        }

        public string Type
        {
            get => entryType.Text;
            set => entryType.Text = value;
        }

        public string Location
        {
            get => entryLocation.Text;
            set => entryLocation.Text = value;
        }

        public string Status
        {
            get => entryStatus.Text;
            set => entryStatus.Text = value;
        }

        //public string AssignedDoctor
        //{
        //    get => pickerAssignedDoctor.SelectedItem?.ToString();
        //    set => pickerAssignedDoctor.SelectedItem = value;
        //}

        public event EventHandler SaveButtonClicked;

        public event EventHandler<string> OnError;

        private void btnSave_Clicked(object sender, EventArgs e)
        {
            // Add any validation logic here
            SaveButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CancelButtonClicked;

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            CancelButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
