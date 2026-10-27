using MAUIFoodie.Resources.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAUIFoodie.Services
{
    [ContentProperty(nameof(Name))]
    public class TranslationExtension : IMarkupExtension
    {
        public string Name { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(Name))
                return string.Empty;

            var translation = AppResources.ResourceManager.GetString(Name, AppResources.Culture);
            return translation ?? Name;
        }
    }
}
