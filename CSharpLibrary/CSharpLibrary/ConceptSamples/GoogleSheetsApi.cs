using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpLibrary.ConceptSamples
{
    class GoogleSheetsApi
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "students";

        public static void CreateShhet()
        {
            UserCredential credential;

            //using (var stream =
            //    new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            //{
            //    string credPath = System.Environment.GetFolderPath(
            //        System.Environment.SpecialFolder.Personal);
            //    credPath = Path.Combine(credPath, ".credentials/sheets.googleapis.com-dotnet-quickstart.json");

                var cre = new ClientSecrets() {
                    ClientId = "486594581111-l2uj7vaolq52lakj8to4pmqmj77am9de.apps.googleusercontent.com",
                    ClientSecret = "JTqAiWu_SwFx8wajUhIEer6m" };
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    cre,
                    Scopes,
                    "user",
                    CancellationToken.None
                    //,new FileDataStore(credPath, true)
                    ).Result;
                //Console.WriteLine("Credential file saved to: " + credPath);
            //}

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            String spreadsheetId = "1vJJfXrJFw_itGIucptG3-iqplUPZ24L_p1rbAZ9Mo3o";
            String range = "Class Data!A:C";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                Console.WriteLine("Name, Major");
                foreach (var row in values)
                {
                    // Print columns A and E, which correspond to indices 0 and 4.
                    Console.WriteLine("{0}, {1}", row[0], row[1]);
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }
            Console.Read();
        }
        }
    }
