// Project: DeviceInfoSyncClient
// FileName: EnumToBooleanConverter.cs
// Copyright (c) 2022 MortalKim
// Date: 2022-8-29

using System;
using System.Globalization;
using System.Windows.Data;

namespace DeviceInfoSyncClient.Helpers
{
    internal class EnumToBooleanConverter : IValueConverter
    {
        public EnumToBooleanConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is not String enumString)
                throw new ArgumentException("ExceptionEnumToBooleanConverterParameterMustBeAnEnumName");

            if (!Enum.IsDefined(typeof(Wpf.Ui.Appearance.ThemeType), value))
                throw new ArgumentException("ExceptionEnumToBooleanConverterValueMustBeAnEnum");

            var enumValue = Enum.Parse(typeof(Wpf.Ui.Appearance.ThemeType), enumString);

            return enumValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is not String enumString)
                throw new ArgumentException("ExceptionEnumToBooleanConverterParameterMustBeAnEnumName");

            return Enum.Parse(typeof(Wpf.Ui.Appearance.ThemeType), enumString);
        }
    }
}
