using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIA;
using System.Runtime.InteropServices;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
namespace WindowsFormsApp1
{
    // add refrence com microsoft windows image acquistion library v2.0
    // type using wia; change imbed Interop Types to false
    // create public class Scanner if you dont create you get error
    // add a listbox , button GetScanners, button Scan, Picturebox1, SaveFileDialoge, openFileDialog

    public partial class Form1 : Form
    {
        public string apploc;
        public Form1()
        {
            InitializeComponent();
        }
                    
        private void button1_Click(object sender, EventArgs e)
        {
            // Scanner selected?
            var device = listBox1.SelectedItem as Scanner;
            if (device == null)
            {
                MessageBox.Show("Please select a device.", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Scan
            var image = device.Scan(); // scan immage

            // setup directory where to save image file or file
            saveFileDialog1.InitialDirectory = apploc; // start location project directory apploc
            saveFileDialog1.Title = "Scan Scanned Image"; // directory title
            saveFileDialog1.FileName = ""; // empty filename
            saveFileDialog1.Filter = "ImageFile |*.jpeg"; // filter .jpeg
            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel) // if savefile ok
            {
                image.SaveFile(saveFileDialog1.FileName);
                var path = saveFileDialog1.FileName;
                
                pictureBox1.ImageLocation = path;
            }

            // Save the image
            
            
        }

       
        // click button to get scanners
        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            // Create a DeviceManager instance
            var deviceManager = new DeviceManager();

            // Loop through the list of devices and add the name to the listbox
            for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
            {
                //Add the device to the list if it is a scanner
                if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                {
                    return;
                }

                listBox1.Items.Add(new Scanner(deviceManager.DeviceInfos[i]));
            }
        }

        // when app runs it start here and load scanners list automatically
        private void Form1_Load_1(object sender, EventArgs e)
        {
            // get my applocation
            //apploc = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            // this line get the project directory
            apploc = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName.ToString();
           // MessageBox.Show(apploc);
            // Clear the ListBox.
            listBox1.Items.Clear();

            // Create a DeviceManager instance
            var deviceManager = new DeviceManager();

            // Loop through the list of devices and add the name to the listbox
            for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
            {
                //Add the device to the list if it is a scanner
                if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                {
                    return;
                }
                // Scanner class name and have scan method
                listBox1.Items.Add(new Scanner(deviceManager.DeviceInfos[i]));
            }
        }

        private void GetSavedImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = apploc; // where to start
            openFileDialog1.Title = "Scan Scanned Image";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "ImageFile |*.jpeg";
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                
                var path = openFileDialog1.FileName;

                pictureBox1.ImageLocation = path;
            }
        }
    }

    
}
