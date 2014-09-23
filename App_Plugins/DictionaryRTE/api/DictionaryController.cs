using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Umbraco.Web.WebApi;

namespace DictionaryRTE.api
{
    public class PostNode
    {
        public Int32 LngId { get; set; }
        public Guid DictionaryId { get; set; }
    }

    [UmbracoAuthorize]
    public class DictionaryController : UmbracoApiController
    {
        private UmbracoDatabase Db
        {
            get
            {
                return ApplicationContext.Current.DatabaseContext.Database;
            }
        }

        [HttpGet]
        public IEnumerable<ItemDictionary> GetDictionaryItems()
        {
            var res = Db.Query<ItemDictionary>(
                    "SELECT distinct d.[key], d.id FROM [dbo].[cmsDictionary] d join [dbo].[cmsLanguageText] lt on lt.UniqueId = d.id");
            return res;
        }

        [HttpGet]
        public IEnumerable<Language> GetLanguages()
        {
            var res = Db.Query<Language>("SELECT [id], [languageISOCode] FROM [dbo].[umbracoLanguage]").Select(x=>new Language
            {
                id = x.id,
                languageISOCode = CultureInfo.GetCultureInfo(x.languageISOCode).EnglishName
            });

            return res;
        }

        [HttpPost]
        public Object GetValue(PostNode item)
        {
            var res = Db.SingleOrDefault<String>(
                String.Format("SELECT [value] FROM [dbo].[cmsLanguageText] where languageId = {0} and UniqueId = '{1}'", item.LngId, item.DictionaryId));

            return new { value = res };
        }
    }
}