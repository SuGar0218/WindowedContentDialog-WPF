using System;
using System.Windows;

namespace SuGarToolkit.WPF.SourceGenerators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class DependencyPropertyAttribute : Attribute
    {
        public object DefaultValue { get; set; }

        public string DefaultValuePath { get; set; }

        //public PropertyChangedCallback OnPropertyChanged { get; set; }
    }
}