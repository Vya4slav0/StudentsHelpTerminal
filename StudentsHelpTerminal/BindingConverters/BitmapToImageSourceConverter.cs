﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace BindingConverters
{
    public class BitmapToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
        {
            MemoryStream ms = new MemoryStream();
            ((Bitmap)value).Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            image.Freeze();

            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
