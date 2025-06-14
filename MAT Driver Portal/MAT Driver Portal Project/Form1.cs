using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace SharanPranav_MAT_Assignment1_BAP
{
    public partial class Welcome_Page : Form
    {


        //Field Variables Declaration
        string DriverName;
        string BusID;       
        public int TotalDrivers = 0;
        public int TotalPassengers;
        public decimal TotalCompanyReceipts;
        public decimal TotalFullFaresReceipts;
        public decimal TotalStudentFaresReceipts;
        public decimal TotalChildFaresReceipts;
        public decimal TotalFullFarePercent;
        public decimal TotalStudentFarePercent;
        public decimal TotalChildFarePercent;
        public int FullFares;
        public int StudentFares;
        public int ChildFares;

        // Constant Variable Delaration
        const decimal FULL_FARE = 9.50m;
        const decimal STUDENT_FARE = 6.75m;
        const decimal CHILD_FARE = 4.75m;

        public Welcome_Page()
        {
            InitializeComponent();
        }

        private void Welcome_Page_Load(object sender, EventArgs e)
        {
            // Toggling the Visibility and setting the cursor to Driver Name
            MainPanel.Visible = true;
            DriverNameTextbox.Select();
            DriverNameTextbox.Focus();
            FaresPanel.Visible = false;
        }

        // On Clicking Proceed button
        private void Proceed_button_Click(object sender, EventArgs e)
        {
            try
            {

                // Validating if DriverName and BusID are NotNull
                if (String.IsNullOrEmpty(DriverNameTextbox.Text) || String.IsNullOrEmpty(BusIDTextBox.Text))
                {
                    throw new Exception();
                }
                else
                {

                    //Local Varaiable Declaration and assigning values to DriverName and BusID
                    string ZeroValue = "0";
                    DriverName = DriverNameTextbox.Text;
                    BusID = BusIDTextBox.Text;
                    
                    // Naming the Form Text
                    Welcome_Page.ActiveForm.Text = "Data Entry for Driver: " + DriverName + "  Bus ID: " + BusID;

                    //Toggling the Visibility and Setting Enabled Property
                    MainPanel.Visible = false;
                    FaresPanel.Visible = true;
                    DriverSummaryGroupBox.Visible = false;
                    CompanySummaryGroupBox.Visible = false;
                    ProcessButton.Enabled = true;
                    DailyPassengersGroupBox.Show();
                    
                    // Assigning zero to Fares Textboxes
                    FullFaresTextBox.Text = ZeroValue;
                    FullFaresTextBox.Focus();
                    StudentFaresTextBox.Text = ZeroValue;
                    ChildFaresTextBox.Text = ZeroValue;                                       
                }
            }
            catch
            {

                // DriverName Exception
                if (String.IsNullOrEmpty(DriverNameTextbox.Text))
                {
                    MessageBox.Show("Please enter the Driver Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DriverNameTextbox.Focus();
                }
                else
                {

                    //BusID Exception
                    MessageBox.Show("Please enter the Bus ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    BusIDTextBox.Focus();
                }
            }
        }
        
        // On Clicking Process button
        private void ProcessButton_Click_1(object sender, EventArgs e)
        {
            // local Variable Declaration
            int TotalNumberOfpassengers;
            decimal TotalDriverReceipts;

            //Exceptional Handling for Fares Textboxes
            try
            {
                int FullFares = int.Parse(FullFaresTextBox.Text);
                try
                {
                    int StudentFares = int.Parse(StudentFaresTextBox.Text);
                    try
                    {
                        int ChildFares = int.Parse(ChildFaresTextBox.Text);

                        // Total Number of Passengers and Total Driver Receipts Calculations
                        TotalNumberOfpassengers = FullFares + StudentFares + ChildFares;
                        TotalDriverReceipts = (FullFares * FULL_FARE) + (StudentFares * STUDENT_FARE) + (ChildFares * CHILD_FARE);

                        // Error Handling for Zero Fares Count
                        try
                        {
                            if(TotalNumberOfpassengers>0)
                            {
                                // Changing the Form Text
                                Welcome_Page.ActiveForm.Text = "Driver Summary: " + DriverName + "  Bus ID: " + BusID;

                                // Toggling the Visibility and Setting Enabled Property
                                DriverSummaryGroupBox.Visible = true;
                                DailyPassengersGroupBox.Enabled = false;                                
                                SummaryButton.Enabled = true;
                                ProcessButton.Enabled = false;

                                // Local Variable Declaration
                                decimal FullFareReceipts;
                                decimal StudentFareReceipts;
                                decimal ChildFareReceipts;
                                decimal FullFarePercent;
                                decimal StudentFarePercent;
                                decimal ChildFarePercent;

                                // Fare Receipts Calculation
                                FullFareReceipts = FullFares * FULL_FARE;
                                StudentFareReceipts = StudentFares * STUDENT_FARE;
                                ChildFareReceipts = ChildFares * CHILD_FARE;

                                // Fare Percentage Calculation
                                FullFarePercent = (FullFareReceipts / TotalDriverReceipts) * 100;
                                StudentFarePercent = (StudentFareReceipts / TotalDriverReceipts) * 100;
                                ChildFarePercent = (ChildFareReceipts / TotalDriverReceipts) * 100;

                                // Calculating Total Values for Company Summary
                                TotalDrivers++;
                                TotalPassengers += TotalNumberOfpassengers;
                                TotalCompanyReceipts += TotalDriverReceipts;
                                TotalFullFaresReceipts += FullFareReceipts;
                                TotalStudentFaresReceipts += StudentFareReceipts;
                                TotalChildFaresReceipts += ChildFareReceipts;

                                // Assigning Values to Driver Summary
                                TotalPassengersTextBox.Text = TotalNumberOfpassengers.ToString();
                                TotalDriverReceiptsTextBox.Text = TotalDriverReceipts.ToString("C");
                                FullFareReceiptsTextBox.Text = FullFareReceipts.ToString("C");
                                StudentFareReceiptsTextBox.Text = StudentFareReceipts.ToString("C");
                                ChildFareReceiptsTextBox.Text = ChildFareReceipts.ToString("C");
                                FullFarePercentageTextBox.Text = Math.Round(FullFarePercent).ToString() + "%";
                                StudentFarePercentageTextBox.Text = Math.Round(StudentFarePercent).ToString() + "%";
                                ChildFarePercentageTextBox.Text = Math.Round(ChildFarePercent).ToString() + "%";                            
                            }
                            else
                            {
                                throw new Exception();
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Please enter atleast one Passenger", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            FullFaresTextBox.Focus();
                            FullFaresTextBox.SelectAll();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Please enter numerical data for Child Passengers", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ChildFaresTextBox.Focus();
                        ChildFaresTextBox.SelectAll();
                    }
                }
                catch
                {
                    MessageBox.Show("Please enter numerical data for Student Passengers", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    StudentFaresTextBox.Focus();
                    StudentFaresTextBox.SelectAll();
                }
            }
            catch
            {
                MessageBox.Show("Please enter numerical d" +
                    "ata for Full Passengers", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FullFaresTextBox.Focus();
                FullFaresTextBox.SelectAll();
            }           
        }

        // On Clicking Clear Button
        private void ClearButton_Click(object sender, EventArgs e)
        {

            // Toggling the Visibility and Setting Enabled Property
            MainPanel.Visible = true;
            FaresPanel.Visible = false;
            DailyPassengersGroupBox.Enabled = true;

            // Clearing the TextBoxes
            DriverNameTextbox.Text = "";
            BusIDTextBox.Text = "";
            FullFaresTextBox.Text = "";
            StudentFaresTextBox.Text = "";
            ChildFaresTextBox.Text = "";
            TotalPassengersTextBox.Text = "";
            TotalDriverReceiptsTextBox.Text = "";
            FullFareReceiptsTextBox.Text = "";
            StudentFareReceiptsTextBox.Text = "";
            ChildFareReceiptsTextBox.Text = "";
            FullFarePercentageTextBox.Text = "";
            StudentFarePercentageTextBox.Text = "";
            ChildFarePercentageTextBox.Text = "";

        }

        // On Clicking Summary Button
        private void SummaryButton_Click(object sender, EventArgs e)
        {
            // Changing the Form Text
            Welcome_Page.ActiveForm.Text = "MAT Summary Data";

            // Toggling the Visibility
            DailyPassengersGroupBox.Visible = false;
            DriverSummaryGroupBox.Visible = false;
            CompanySummaryGroupBox.Visible = true;

            // Calculating the Total Fare Percentage 
            TotalFullFarePercent = (TotalFullFaresReceipts / TotalCompanyReceipts) * 100;
            TotalStudentFarePercent = (TotalStudentFaresReceipts / TotalCompanyReceipts) * 100;
            TotalChildFarePercent = (TotalChildFaresReceipts / TotalCompanyReceipts) * 100;

            // Assigning Values to the Company Summary Section
            TotalDriversTextBox.Text = TotalDrivers.ToString();
            TotalPassengersTextBox1.Text = TotalPassengers.ToString();
            TotalCompanyReceiptsTextBox.Text = TotalCompanyReceipts.ToString("C");
            FullFareReceiptsTextBox1.Text = TotalFullFaresReceipts.ToString("C");
            StudentFareReceiptsTextBox1.Text = TotalStudentFaresReceipts.ToString("C");
            ChildFareReceiptsTextBox1.Text = TotalChildFaresReceipts.ToString("C");
            FullFarePercentageTextBox1.Text = Math.Round(TotalFullFarePercent).ToString() + "%";
            StudentFarePercentageTextBox1.Text = Math.Round(TotalStudentFarePercent).ToString() + "%";
            ChildFarePercentageTextBox1.Text = Math.Round(TotalChildFarePercent).ToString() + "%";
        }

        // Clicking on Exit Button
        private void ExitButton_Click(object sender, EventArgs e)
        {
            // Closing the page
            this.Close();
        }
    }
}

