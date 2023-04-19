using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace DRiUv4
{
    internal class GoogleList
    {
        public GoogleList()
        {
        }

        ~GoogleList()
        {
            SheetsService = null;
        }

        //zmienne
        private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        //private static readonly string creditential = "dazzling-spirit-383513-0f9dd5bb5d8c.json"; //uprawnienia do konta Google Sheets
        private static readonly string spreadsheetId = "1e9nHy4brmTT4UKLLMhD3QJyqM8W5OJp9Aq9jbadpGLM"; //id udostępnionego pliku
        public static string _SheetName = "lists";
        private static readonly string ApplicationName = "DRiU";

        public static SheetsService SheetsService { get; set; }


        //metody
        public static void GoogleSheetsConnect()
        {
            GoogleCredential credential;

            var assembly = typeof(Send).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("DRiUv4.dazzling-spirit-383513-0f9dd5bb5d8c.json");
            using (var reader = new StreamReader(stream))
            {
                credential = GoogleCredential.FromStream(stream)
                .CreateScoped(Scopes);
            }

            // Create Google Sheets API service.
            SheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });
        }

        public static List<string> GetListsItem(int listColumn)
        {
            if (listColumn == 0)
                return null;
            if (SheetsService == null)
                GoogleSheetsConnect();
            var range = $"{_SheetName}!R2C{listColumn}:R100C{listColumn}";

            var appendRequest = SheetsService.Spreadsheets.Values.Get(spreadsheetId, range);

            appendRequest.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;

            var response = appendRequest.Execute();
            List<string> strings = new List<string>();
            foreach (var items in response.Values[0])
            {
                strings.Add(items.ToString());
            }
            return strings;
        }

        public static ValueRange GetListsName()
        {
            GoogleSheetsConnect();

            var range = $"{_SheetName}!A1:Q1";

            var appendRequest = SheetsService.Spreadsheets.Values.Get(spreadsheetId, range);

            var response = appendRequest.Execute();

            return response;
        }
    }
}
