using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

using Microsoft.AspNetCore.Mvc;
using P2EyeRIS.Models;
using System.IO;
using System.Threading;

namespace P2EyeRIS.Controllers
{
    public class StaffController : Controller
    {
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "Staff EyeRIS Dashboard";
        string spreadsheetId = "1Ws-dLtYaGjHGwpgNEZHwHWK0X-4eFfEjB5JjS7JcTeI";
        string moduleClass, listRange, totalRange;

        UserCredential creds;

        public async Task<ActionResult> Index()
        {
            using (var stream = new FileStream("cred.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                creds = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = creds,
                ApplicationName = ApplicationName,
            });

            List<Student> sList = new List<Student>();

            string sheet = "FSD_T01"; //yes, these are hardcoded in the meantime, to test the dropdown listx`
            string range = "A7:B12";

            if(ShowStudentList(sheet, range).Count() > 0)
            {
                sList = ShowStudentList(sheet, range);
            }
            return View(sList); //Return empty list onload
        }

        public void StudentProfile(string id)
        {
            //return student profile view provided their id and their attendance
        }

        [HttpPost] //Change return type to actionresult ltr
        public List<Student> ShowStudentList(string moduleClassInput, string listRangeInput)
        {
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = creds,
                ApplicationName = ApplicationName,
            });

            moduleClass = moduleClassInput; //can be automated later
            listRange = listRangeInput; //can be automated later
            totalRange = string.Format("{0}!{1}", moduleClass, listRange);
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, totalRange);

            ValueRange response = request.Execute();

            IList<IList<Object>> values = response.Values;
            if (values.Count > 0)
            {
                List<Student> studentList = new List<Student>();
                foreach (var row in values)
                {
                    Student s = new Student();
                    s.Id = row[0].ToString();
                    s.Name = row[1].ToString();
                    studentList.Add(s);
                }
                return studentList;
            }
            else
            {
                return new List<Student>();
            }
        }
    }
}