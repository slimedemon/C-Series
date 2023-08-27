﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace HandlingCancellationRequest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // New Window-level variable.
        private CancellationTokenSource cancelToken = new CancellationTokenSource();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            cancelToken.Cancel();
        }
        private void cmdProcess_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => ProcessFiles());
        }
        private void ProcessFiles()
        {
            // Use ParallelOptions instance to store the CancellationToken.
            ParallelOptions parOpts = new ParallelOptions();
            parOpts.CancellationToken = cancelToken.Token;
            parOpts.MaxDegreeOfParallelism = System.Environment.ProcessorCount;

            // Load up all *.jpg files, and make a new folder for the modified data.
            string[] files = Directory.GetFiles(@".\TestPictures", "*.png", SearchOption.AllDirectories);
            string newDir = @".\ModifiedPictures";
            Directory.CreateDirectory(newDir);
            try
            {
                // Process the image data in a parallel manner!
                Parallel.ForEach(files, parOpts, currentFile =>
                {
                    parOpts.CancellationToken.ThrowIfCancellationRequested();
                    string filename = Path.GetFileName(currentFile);
                    using (Bitmap bitmap = new Bitmap(currentFile))
                    {
                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        bitmap.Save(Path.Combine(newDir, filename));
                        this.Dispatcher.Invoke((Action)delegate
                        {
                            this.Title = $"Processing {filename} on thread {Thread.CurrentThread.ManagedThreadId}";
                        }
                        );
                    }
                }
                );
                this.Dispatcher.Invoke((Action)delegate
                {
                    this.Title = "Done!";
                });
            }

            catch (OperationCanceledException ex)
            {
                this.Dispatcher.Invoke((Action)delegate
                {
                    this.Title = ex.Message;
                });
            }

        }
    }
}
