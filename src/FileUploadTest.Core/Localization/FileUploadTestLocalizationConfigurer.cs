using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace FileUploadTest.Localization
{
    public static class FileUploadTestLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(FileUploadTestConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(FileUploadTestLocalizationConfigurer).GetAssembly(),
                        "FileUploadTest.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
