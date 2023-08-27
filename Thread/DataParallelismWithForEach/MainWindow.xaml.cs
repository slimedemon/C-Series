using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
using System.Drawing;
using System.Threading;
using System.IO;

namespace DataParallelismWithForEach
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

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            // update in next sections.
        }
        private void cmdProcess_Click(object sender, EventArgs e)
        {
            ProcessFiles();
            //Task.Factory.StartNew(() => ProcessFiles());
        }
        private void ProcessFiles()
        {
            // Load up all *.jpg files, and make a new folder for the modified data.
            string[] files = Directory.GetFiles(@".\TestPictures", "*.png", SearchOption.AllDirectories);
            string newDir = @".\ModifiedPictures";
            Directory.CreateDirectory(newDir);

            //// Process the image data in a blocking manner.
            //foreach (string currentFile in files)
            //{
            //    string filename = Path.GetFileName(currentFile);
            //    using (Bitmap bitmap = new Bitmap(currentFile))
            //    {
            //        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
            //        bitmap.Save(Path.Combine(newDir, filename));
            //        // Print out the ID of the thread processing the current image.
            //        this.Title = $"Processing {filename} on thread {Thread.CurrentThread.ManagedThreadId}";
            //    }
            //}

            // Process the image data in a parallel manner!
            Parallel.ForEach(files, currentFile =>
            {
                string filename = Path.GetFileName(currentFile);
                using (Bitmap bitmap = new Bitmap(currentFile))
                {
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    bitmap.Save(Path.Combine(newDir, filename));

                    // This code statement is now a problem! See next section.
                    //this.Title = $"Processing {filename} on thread {Thread.CurrentThread.ManagedThreadId}";

                    // Invoke on the Form object, to allow secondary threads to access controls
                    // in a thread-safe manner.
                    this.Dispatcher.Invoke((Action)delegate
                    {
                        this.Title = $"Processing {filename} on thread {Thread.CurrentThread.ManagedThreadId}";
                    }
                    );
                }
            }
            );

        }
    }
}
