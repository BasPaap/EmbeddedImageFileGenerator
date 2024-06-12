using CommunityToolkit.Mvvm.Messaging;
using EmbeddedImageFileGenerator.Messages;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EmbeddedImageFileGenerator.ViewModels
{
    internal class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ImageSource? imageSource;
        public ImageSource? ImageSource
        {
            get => imageSource;
            set => SetPropertyValue(ref imageSource, value);
        }

        private string? text;
        public string? Text
        {
            get => text;
            set => SetPropertyValue(ref text, value);
        }

        public MainPageViewModel()
        {
            WeakReferenceMessenger.Default.Register<ImageOpenedMessage>(this, (recipient, message) =>
            {
                ImageSource = ImageSource.FromFile(message.Value);                
            });

            Text = "Foo";
        }

        private void SetPropertyValue<T>(ref T storageField, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (Equals(storageField, newValue)) 
                return;

            storageField = newValue;
            RaisePropertyChanged(propertyName);
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
