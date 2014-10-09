using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;
using FileGenerator.Logic;

namespace FileGenerator.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _FilePath = "";
        private string _UploadFileName = "";
        private B2CFiles _B2CFiles = new B2CFiles();
        private B2BFiles _B2BFiles = new B2BFiles();
        private VendorDA _VendorDA = new VendorDA();
        //private List<Vendor> _VendorList = null;

        public MainWindow()
        {
            InitializeComponent();
            ResetFields();
    
        }

        /// <summary>
        /// Reset all fields
        /// </summary>
        public void ResetFields()
        {
            cboFileType.SelectedIndex = -1;
            cboDatabase.SelectedIndex = -1;
            cboRenewalMonth.SelectedIndex = -1;
            _FilePath = "";
            lblLocation.Content = _FilePath;
            stpOptionsDump.Visibility = System.Windows.Visibility.Hidden;
            stpButtons.Visibility = System.Windows.Visibility.Hidden;
            stpNotSupported.Visibility = System.Windows.Visibility.Hidden;
            stpRenewalDump.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// Get the filepath where the file must be generated to
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdFileLocation_Click(object sender, RoutedEventArgs e)
        {
            // Show dialog to get the directory where the file must be dropped
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowDialog();
            // Get The selected path
            _FilePath = folderBrowser.SelectedPath;
            lblLocation.Content = _FilePath.ToString();
        }

        /// <summary>
        /// Close application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Show the correct fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboFileType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Hide all stack Panels
            stpButtons.Visibility = Visibility.Hidden;
            stpNotSupported.Visibility = Visibility.Hidden;
            stpOptionsDump.Visibility = Visibility.Hidden;
            stpRenewalDump.Visibility = Visibility.Hidden;
            stpPriceEvol.Visibility = Visibility.Hidden;
            stpSoctarAnswer.Visibility = Visibility.Hidden;

            switch (cboFileType.SelectedIndex)
            {
                case -1:
                    ResetFields();
                    break;
                case 0:
                    // Soctar Full Dump
                    dtDeliveryDate.Text = DateTime.Now.ToString();
                    dtDumpDate.Text = DateTime.Now.ToString();
                    stpOptionsDump.Visibility = Visibility.Visible;
                    stpButtons.Visibility = Visibility.Visible;
                    break;
                case 1:
                    //Soctar Mutation
                    dtDeliveryDate.Text = DateTime.Now.ToString();
                    dtDumpDate.Text = DateTime.Now.ToString();
                    stpOptionsDump.Visibility = Visibility.Visible;
                    stpButtons.Visibility = Visibility.Visible;
                    break;
                case 2:
                    //AX NAW Dump
                    stpButtons.Visibility = Visibility.Visible;
                    break;
                case 3:
                    // AX Contract Dump
                    stpButtons.Visibility = Visibility.Visible;
                    break;
                case 4:
                    // AX EAN Dump
                    stpButtons.Visibility = Visibility.Visible;
                    break;
                case 5:
                    // AX Renewal Dump
                    cboRenewalMonth.SelectedIndex = -1;
                    stpRenewalDump.Visibility = Visibility.Visible;
                    stpButtons.Visibility = Visibility.Visible;
                    break;
                case 6:
                    // AX Portfolio Dump
                    stpButtons.Visibility = Visibility.Visible;
                    break;
                case 7:
                    // B2B Price Evolution
                    //FillVendor();
                    stpPriceEvol.Visibility = Visibility.Visible;
                    stpButtons.Visibility = Visibility.Visible;

                    break;
                case 8:
                    // Soctar Answer
                    stpSoctarAnswer.Visibility = Visibility.Visible;
                    stpButtons.Visibility = Visibility.Visible;
                    break;
                default:
                    stpButtons.Visibility = System.Windows.Visibility.Visible;
                    cmdGenerate.Visibility = System.Windows.Visibility.Visible;
                    break;
            }
        }

        /// <summary>
        /// Generate the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdGenerate_Click(object sender, RoutedEventArgs e)
        {
            // Check everything is filled in
            string _ValidationErrors = "Validation Errors:\n";
            string _SQLResult = "";
            bool _blnError = false;

            // Check Filepath is selected
            if (_FilePath == "")
            {
                _ValidationErrors += "- File location is required \n";
                _blnError = true;
            }
            else
            {
                try
                {
                    if (Directory.Exists(_FilePath) == false)
                    {
                        _ValidationErrors += "- File location doesn't exists \n";
                        _blnError = true;
                    }

                }
                catch (FileNotFoundException ex)
                {
                    _ValidationErrors += " - File location doesn't exists \n";
                    _ValidationErrors += ex.Message + " \n";
                    _blnError = true;
                }    
            }

            // Database is required
            if (cboDatabase.SelectedIndex == -1)
            {
                _ValidationErrors += "- Database is required \n";
                _blnError = true;
            }
            // File type is required
            if (cboFileType.SelectedIndex == -1)
            {
                _ValidationErrors += "- Fite type is required \n";
                _blnError = true;
            }
            // Parameters are required for a Full or mutation dump
            if ((cboFileType.SelectedIndex == 0) || (cboFileType.SelectedIndex == 1))
            {

                if (dtDumpDate.Text == "")
                {
                    _ValidationErrors += "- Dump Date is required \n";
                    _blnError = true;
                }

                if (dtDeliveryDate.Text == "")
                {
                    _ValidationErrors += "- Delivery Date is required \n";
                    _blnError = true;
                }
            }

            // Parameter are required for a renewal dump
            if (cboFileType.SelectedIndex == 5)
            {
                if (cboRenewalMonth.SelectedIndex == -1)
                {
                    _ValidationErrors += "- Renewal month is required \n";
                    _blnError = true;
                }
            }

            // Parameters are required for B2B Price Evolution
            if (cboFileType.SelectedIndex == 7)
            {
                if (cboRegion.SelectedIndex == -1)
                {
                    _ValidationErrors += "- Region is required \n";
                    _blnError = true;
                }
                if (cboServiceType.SelectedIndex == -1)
                {
                    _ValidationErrors += "- Service type is required \n";
                    _blnError = true;
                }
                if (dtStartPeriod.Text == "")
                {
                    _ValidationErrors += "- Start Period is required \n";
                    _blnError = true;
                }

                if (dtEndPeriod.Text == "")
                {
                    _ValidationErrors += "- End Period is required \n";
                    _blnError = true;
                }
                //if (cboVendor.SelectedIndex == -1)
                //{
                //    _ValidationErrors += "- Vendor GLN is required \n";
                //    _blnError = true;
                //}

            }
            if (cboFileType.SelectedIndex == 8)
            {
                if (string.IsNullOrEmpty(_UploadFileName))
                {
                    _ValidationErrors += " - Source File is required \n";
                    _blnError = true;
                }
                else
                {
                    try
                    {
                        if (!File.Exists(_UploadFileName))
                        {
                            _ValidationErrors += " - Source File doesn't exists \n";
                            _blnError = true;
                        }
                    }
                    catch (FileNotFoundException ex)
                    {
                        _ValidationErrors += " - Source File doesn't exists \n";
                        _ValidationErrors += ex.Message + " \n";
                        _blnError = true;
                    }
                }

            }
            // Show validation errors
            if (_blnError == true)
            {
                System.Windows.MessageBox.Show(_ValidationErrors, "Validation error", MessageBoxButton.OK, MessageBoxImage.Error);
                ResetFields();

            }
            else
            {
                // Make file
                switch (cboFileType.SelectedIndex)
                {
                    case 0:
                        //Soctar Full Dump
                        _SQLResult = _B2CFiles.CreateSoctarFile(Convert.ToDateTime(dtDumpDate.Text), Convert.ToDateTime(dtDeliveryDate.Text), cboDatabase.SelectedIndex, 0, _FilePath);
                        if (_SQLResult != "")
                        {
                            System.Windows.MessageBox.Show(_SQLResult, "Build Socatr Dumpfile Failed!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {  
                            System.Windows.MessageBox.Show("Generate Soctar Dumpfile successfully", "Build Oke", MessageBoxButton.OK, MessageBoxImage.Information); 
                        }
                        ResetFields();
                        break;
                    case 1:
                        //Soctar Mutation
                        _SQLResult = _B2CFiles.CreateSoctarFile(Convert.ToDateTime(dtDumpDate.Text), Convert.ToDateTime(dtDeliveryDate.Text), cboDatabase.SelectedIndex, 1, _FilePath);
                        if (_SQLResult != "")
                        {
                            System.Windows.MessageBox.Show(_SQLResult, "Build Soctar MutationFile Failed!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {  
                            System.Windows.MessageBox.Show("Generate Soctar MutationFile successfully", "Build Oke", MessageBoxButton.OK, MessageBoxImage.Information); 
                        }
                        ResetFields();
                        break;
                    case 2:
                        //AX NAW Dump
                        _SQLResult = _B2CFiles.BuildDumpFile(0, cboDatabase.SelectedIndex, _FilePath, 0);
                        if (_SQLResult != "")
                        {
                            System.Windows.MessageBox.Show(_SQLResult, "Generate NAW Dump Failed!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Generate NAW Dump successfully", "Build Oke", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        ResetFields();
                        break;
                    case 3:
                        // AX Contract Dump
                       _SQLResult = _B2CFiles.BuildDumpFile(1, cboDatabase.SelectedIndex, _FilePath, 0);
                        if (_SQLResult != "")
                        {
                            System.Windows.MessageBox.Show(_SQLResult, "Generate Contract Dump Failed!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Generate Contract Dump successfully", "Build Oke", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        ResetFields();
                        break;
                    case 4:
                        // AX EAN Dump
                        _SQLResult = _B2CFiles.BuildDumpFile(2, cboDatabase.SelectedIndex, _FilePath, 0);
                        if (_SQLResult != "")
                        {
                            System.Windows.MessageBox.Show(_SQLResult, "Generate EAN Dump Failed!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Generate EAN Dump successfully", "Build Oke", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        ResetFields();
                        break;
                    case 5:
                        // AX Renewal Dump
                        _SQLResult = _B2CFiles.BuildDumpFile(3, cboDatabase.SelectedIndex, _FilePath,cboRenewalMonth.SelectedIndex+1);
                        if (_SQLResult != "")
                        {
                            System.Windows.MessageBox.Show(_SQLResult, "Generate Renewal Dump Failed!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Generate Renewal Dump successfully", "Build Oke", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        ResetFields();
                        break;
                    case 6:
                        // AX Portfolio Dump
                        _SQLResult = _B2CFiles.BuildPortfolio(cboDatabase.SelectedIndex, _FilePath);
                        if (_SQLResult != "")
                        {
                            System.Windows.MessageBox.Show(_SQLResult,"Generate Portfolio Dump Failed!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Generate Portfolio Dump successfully", "Build Oke", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        ResetFields();
                        break;
                    case 7:
                        // B2B Price Evolution
                        _SQLResult = _B2BFiles.BuildPriceEvolution(Convert.ToDateTime(dtStartPeriod.Text), Convert.ToDateTime(dtEndPeriod.Text), cboDatabase.SelectedIndex, cboServiceType.SelectedIndex, cboRegion.SelectedIndex, _FilePath,"");
                        if (_SQLResult != "")
                        {
                            System.Windows.MessageBox.Show(_SQLResult, "Generate B2B Price Evolution Failed!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Generate B2B Price Evolution successfully", "Build Oke", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        break;
                    case 8:
                        // Soctar Answer file
                        _SQLResult = _B2CFiles.BuildAnswerSoctarFile(cboDatabase.SelectedIndex, _FilePath, _UploadFileName);
                        if (_SQLResult != "")
                        {
                            System.Windows.MessageBox.Show(_SQLResult, "Generate Soctar Answer file Failed!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Generate Soctar Answer file successfully", "Build Oke", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        break;
                    default:
                        System.Windows.MessageBox.Show("File type not supported", "File type not supported", MessageBoxButton.OK, MessageBoxImage.Error);
                        ResetFields();
                        break;
                }

            }
        }
    
        /// <summary>
        /// Select a file to upload
        /// Only CSV files allowed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUploadFile_Click(object sender,RoutedEventArgs e)
        {
            // Show dialog to get the file to upload
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select a file to upload";
            fileDialog.Filter = "csv files (*.csv)|*.csv";
            fileDialog.ShowDialog();
            _UploadFileName = fileDialog.FileName;
            lblSourceFileLocation.Content = _UploadFileName;
        }
        /// <summary>
        /// Fill the VendorList
        /// </summary>
        private void FillVendor()
        {
            //string _ErrorMessage = "";
            //_VendorList = _VendorDA.GetVendors(cboDatabase.SelectedIndex, out _ErrorMessage);
            //foreach (Vendor _Ven in _VendorList)
            //{
            //    cboVendor.Items.Add(_Ven.VendorEAN);
            //}
            //if (_VendorList != null && _VendorList.Count() == 1)
            //{
            //    cboVendor.SelectedIndex = 0;
            //}

        }
    

    }
}
