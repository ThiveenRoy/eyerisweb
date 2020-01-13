﻿using System;
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
        static string ApplicationName = "EyeRIS Dashboard";

        public async Task<ActionResult> Index()
        {
            UserCredential creds;
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

            String spreadsheetId = "1Ws-dLtYaGjHGwpgNEZHwHWK0X-4eFfEjB5JjS7JcTeI";
            String range = "FSD_T01!A7:B12";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

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

                return View(studentList);
            }
            else
            {
                return View(new List<Student>());
            }
        }
    }
}