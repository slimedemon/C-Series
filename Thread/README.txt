1. Foreground Thread And Background Thread
* Foreground threads: The CLR will not shut down an application (which is to say, unload the hosting AppDomain) until all foreground threads have ended.
*  Background threads: if all foreground threads have terminated, any and all background threads are automatically killed when the
application domain unloads.

=> For the most part, configuring a thread to run as a background type can be helpful when the worker
thread in question is performing a noncritical task that is no longer needed when the main task of the
program is finished.

2. Using the "lock" keyword
* Example: 
public class Printer
    {
        // Lock token.
        private object threadLock = new object();

        public void PrintNumbers()
        {
            // Use the lock token.
            lock (threadLock)
            {
                // Display thread info.
                Console.WriteLine("-> {0} is executing PrintNumbers()", Thread.CurrentThread.Name);

                // Print out number
                Console.Write("Your numbers: ");
                for (int i = 0; i < 10; i++)
                { 
                    Random rnd = new Random();
                    // Thread.Sleep(1000 * rnd.Next(5));
                    Console.Write("{0}", i);
                }
                Console.WriteLine();
            }
        }
    }
* Evaluation: lock token is used to notify other threads that this lock block is used by any thread. => other threads are inaccessible to lock block.
* Note: you have effectively designed a method that will allow the current thread to complete its task. Once a thread enters into a lock scope, the lock token (in this case, a reference to the current object) is inaccessible by other threads until the lock is released after the lock scope has exited. Thus, if thread A has obtained the lock token, other threads are unable to enter any scope that uses the same lock token until thread A relinquishes the lock token

3. Using System.Threading.Monitor type
* Now, given that the lock keyword seems to require less code than making explicit use of the System.Threading.Monitor type, you might wonder about the benefits of using the Monitor type directly. The short answer is control. If you use the Monitor type, you are able to instruct the active thread to wait for some duration of time (via the static Monitor.Wait(method), inform waiting threads when the current thread is completed (via the static Monitor.Pulse() and Monitor.PulseAll() methods), and so on.

4. Using the System.Threading.Interlocked Type
* Example:
public void AddOne()
{
 lock(myLockToken)
 {
 intVal++;
 }
}

=> 

public void AddOne()
{
 int newVal = Interlocked.Increment(ref intVal);
}

5. Asynchronize nature of delegate

6. TPL (Task Parallel Library - sing System.Threading.Tasks)
* Used in WPF(.NET FRAMEWORK)
* Thread affinity (Điều khiển thread chạy trên những nhân cpu khác nhau) => increase performance.
* Usually use Parallel.For(); Parallel.Foreach(); => easily make methods run on secondary threads.
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
* Note: 
	- Secondary threads cannot access controls in other threads. => unsafe => warning when runging on debug mode (still run when running mode release mode).
	- To access controls of other threads,  Using Dispathcher is defined in WPF (cons: It blocks UI thread). For example:
using (Bitmap bitmap = new Bitmap(currentFile))
{
 bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
 bitmap.Save(Path.Combine(newDir, filename));
 // Eek! This will not work anymore!
 //this.Title = $"Processing {filename} on thread {Thread.CurrentThread.ManagedThreadId}";
 // Invoke on the Form object, to allow secondary threads to access controls
 // in a thread-safe manner.
 this.Dispatcher.Invoke((Action)delegate
 {
 this.Title = $"Processing {filename} on thread {Thread.CurrentThread.ManagedThreadId}";
 }
 );
}

	- To fix blocking UI thread, let's use Task class. For instance:
 // Start a new "task" to process the files.
 Task.Factory.StartNew(() => ProcessFiles());

* Handling cancellation request: 
	- Create cancelToken => create parOpts. For example: 
	 // Use ParallelOptions instance to store the CancellationToken.
            ParallelOptions parOpts = new ParallelOptions();
            parOpts.CancellationToken = cancelToken.Token;
            parOpts.MaxDegreeOfParallelism = System.Environment.ProcessorCount;
	- Using try catch to end processing. For example:
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

7. Using PLINQ
* usage: 
  // Note: nonparallel version.
            int[] modThreeIsZero = (from num in source where num % 3 == 0 orderby num descending select num).ToArray();
 // Note: parallel version.
            int[] modThreeIsZero = (from num in source.AsParallel() where num % 3 == 0 orderby num descending select num).ToArray();
* using CancellationTokenSource to inform PLINQ to stop processing. For example:
	internal class Program
    {
        static CancellationTokenSource cancelToken = new CancellationTokenSource();
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Start any key to start processing");
                Console.ReadKey();
                Console.WriteLine("Processing");
                Task.Factory.StartNew(() => ProcessIntData());
                Console.Write("Enter Q to quit: ");
                string answer = Console.ReadLine();
                // Does user want to quit?
                if (answer.Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                    cancelToken.Cancel();
                    break;
                }
            } while (true);
            Console.ReadLine();
        }

        static void ProcessIntData()
        {
            // Get a very large array of integers.
            int[] source = Enumerable.Range(1, 100_000_000).ToArray();
            // Find the numbers where num % 3 == 0 is true, returned
            // in descending order.
            int[] modThreeIsZero = null;
            try
            {
                modThreeIsZero = (from num in source.AsParallel().WithCancellation(cancelToken.Token)
                                  where num % 3 == 0
                                  orderby num descending
                                  select num).ToArray();
                Console.WriteLine();
                Console.WriteLine($"Found {modThreeIsZero.Count()} numbers that match query!");
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }