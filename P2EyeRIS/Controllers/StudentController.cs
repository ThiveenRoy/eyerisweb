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
using javax.jws;
using Microsoft.AspNetCore.Http;

namespace P2EyeRIS.Controllers
{
    public class StudentController : Controller
    {
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "Staff EyeRIS Dashboard";
        string spreadsheetId = "1Ws-dLtYaGjHGwpgNEZHwHWK0X-4eFfEjB5JjS7JcTeI";
        string loggedStudentId, loggedStudentName, sheet, range, totalRange;
        List<string> staffModuleClass = new List<string>();
        List<StudentGrade> sgList = new List<StudentGrade>();

        UserCredential creds;

        public IActionResult StudentChart()
        {
            //ID and Name of current user
            loggedStudentName = HttpContext.Session.GetString("LoggedStudentName");
            loggedStudentId = HttpContext.Session.GetString("LoggedStudentID");
            ViewData["StudentName"] = loggedStudentName;
            ViewData["StudentID"] = loggedStudentId;


            //For purpose sake, we will select the entire spreadsheet as the range
            ShowStudentList("FSD_T01", "A7:V14");

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

            return View(sgList);
        }

        [HttpPost]
        public ActionResult ShowStudentList(string sheet, string range)
        {
            sgList.Clear();

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = creds,
                ApplicationName = ApplicationName,
            });

            if (RetrieveStudentList(sheet, range).Count() > 0)
            {
                sgList = RetrieveStudentList(sheet, range);
            }
            return View(sgList);
        }

        public List<StudentGrade> RetrieveStudentList(string moduleClassInput, string listRangeInput)
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

            totalRange = string.Format("{0}!{1}", moduleClassInput, listRangeInput);

            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, totalRange);

            ValueRange response = request.Execute();

            IList<IList<Object>> values = response.Values;
            if (values.Count > 0)
            {
                List<StudentGrade> studentGradeList = new List<StudentGrade>();
                foreach (var row in values)
                {
                    if (row[0].ToString() == loggedStudentId){
                        StudentGrade s = new StudentGrade();
                        s.P = row[18].ToString();
                        s.L = row[19].ToString();
                        s.E = row[20].ToString();
                        s.U = row[21].ToString();
                        studentGradeList.Add(s);
                    }
                }
                return studentGradeList;
            }
            else
            {
                return new List<StudentGrade>();
            }
        }
    }
}