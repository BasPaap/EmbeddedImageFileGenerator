using CommunityToolkit.Mvvm.Messaging;
using EmbeddedImageFileGenerator.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmbeddedImageFileGenerator.ViewModels
{
    public class AppShellViewModel
    {
        public ICommand OpenFileCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        private static readonly string[] fileExtensions = [".jpg", ".jpeg", ".png"];
        public AppShellViewModel()
        {
            OpenFileCommand = new Command(async () =>
            {

                var pickOptions = new PickOptions()
                {
                    FileTypes = new FilePickerFileType(
                        new Dictionary<DevicePlatform, IEnumerable<string>> { { DevicePlatform.WinUI, fileExtensions } })
                };

                var fileResult = await FilePicker.Default.PickAsync(pickOptions);

                if (fileResult != null)
                {
                    if (fileResult.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        fileResult.FileName.EndsWith("jpeg", StringComparison.OrdinalIgnoreCase) ||
                        fileResult.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {
                        WeakReferenceMessenger.Default.Send(new ImageOpenedMessage(fileResult.FullPath));
                    }
                }
            });

            ExitCommand = new Command(() =>
            {
                Application.Current?.Quit();
            });
        }
    }
}
