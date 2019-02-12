using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Vegatro.Core.IO.Models;

namespace Vegatro.Core.IO
{
    /// <summary>
    /// File operations
    /// </summary>
    public class File
    {
        private static ReaderWriterLockSlim WriteLock = new ReaderWriterLockSlim();

        private static readonly int FileMaxSize  = 25; // MB
        private static readonly int FileMaxCount = 10;

        /// <summary>
        /// Writes given text to file with file mode
        /// </summary>
        /// <param name="message">File message</param>
        private static void Write(Message message)
        {
            Task.Factory.StartNew(() =>
            {
                Process(message);
            });
        }

        /// <summary>
        /// Writes given text to file with file mode
        /// </summary>
        /// <param name="path">File Path</param>
        /// <param name="text">Given text</param>
        /// <param name="mode">File Mode</param>
        /// <param name="addNewLine">Adds new line to end of the file</param>
        public static void Write(string path, string text, FileMode mode = FileMode.Create, bool addNewLine = false)
        {
            Write(new Message
            {
                Path = path,
                Text = text,
                Mode = mode,
                AddNewLine = addNewLine
            });
        }

        /// <summary>
        /// Writes given text to file with new line
        /// </summary>
        /// <param name="path">File Path</param>
        /// <param name="text">Given text</param>
        /// <param name="mode">File Mode</param>
        public static void WriteLine(string path, string text, FileMode mode = FileMode.Append)
        {
            Write(new Message
            {
                Path = path,
                Text = text,
                Mode = mode,
                AddNewLine = true
            });
        }

        /// <summary>
        /// Processes message queue
        /// </summary>
        private static void Process(Message message)
        {
            try
            {
                WriteLock.EnterWriteLock();

                int fileMaxSize  = (!string.IsNullOrEmpty(Config.Get("Logging:File:MaxSize"))  ? Convert.ToInt32(Config.Get("Logging:File:MaxSize")) : FileMaxSize) * 1024 * 1024;
                int fileMaxCount = (!string.IsNullOrEmpty(Config.Get("Logging:File:MaxCount")) ? Convert.ToInt32(Config.Get("Logging:File:MaxCount")) : FileMaxCount);

                string path = message.Path;

                // Create directory if not exists
                Directory.CreateDirectory(Path.GetDirectoryName(path));

                // Create file if not exists
                if (!System.IO.File.Exists(path))
                     System.IO.File.Create(path).Dispose();

                // Create file parts if necessary
                int partNumber = 1;

                do
                {
                    partNumber++;

                    if (partNumber > fileMaxCount)
                        break;

                    if (new FileInfo(path).Length >= fileMaxSize)
                        path = message.Path + partNumber;

                    // Create file if not exists
                    if (!System.IO.File.Exists(path))
                        System.IO.File.Create(path).Dispose();

                } while (new FileInfo(path).Length >= fileMaxSize);

                // Return when file reached to its size limit
                if (new FileInfo(path).Length >= fileMaxSize)
                    return;

                if (message.Mode == FileMode.Append)
                {
                    System.IO.File.AppendAllText(path, message.Text + (message.AddNewLine ? Environment.NewLine : ""));
                    return;
                }

                System.IO.File.WriteAllText(path, message.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                WriteLock.ExitWriteLock();
            }
        }
    }
}
