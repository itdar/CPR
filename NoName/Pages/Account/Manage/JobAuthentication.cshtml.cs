using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoName.BackendClass.Login;

namespace NoName.Pages.Account.Manage
{
    public class JobAuthenticationModel : PageModel
    {
        [BindProperty]
        public IFormFile UploadFile1 { get; set; }
        [BindProperty]
        public IFormFile UploadFile3 { get; set; }
        [BindProperty]
        public IFormFile UploadFile2 { get; set; }

        public List<int> UserJobCodes { get; set; }

        public JobAuthenticationModel()
        {
            UserJobCodes = UserInformation.GetInstance().JobCodes;
        }

        public void UploadFiles()
        {
            System.Diagnostics.Debug.WriteLine("Called UploadFiles.");

            // Process
            //1. binding 되어있는 IFormFile 가져와서 각 이름별 파일 업로드 했는지 확인하고 (최소 1개라도 있어야함)
            //2. 파일 type (image만 되도록) 확인해야함
            //3. 가져온거 파일이름, length, type 등을 DB table 만들어서 user 정보랑 같이 저장해야함
            //4. 저장하는 경로 폴더 만들어서 서버 위치에 설정/연결
            //5. DB table 에서 저장한 경로 저장해두고, 다른 페이지에서 혹은 직업인증 있는 경우 로드해서 이미지 띄우는 것 확인해야함

            var userInfo = UserInformation.GetInstance();

            // 각 파일 형식 및 사이즈 등 체크해야함
            if (UploadFile1 != null)
            {
                System.Diagnostics.Debug.WriteLine("file1 is not null");

                System.Diagnostics.Debug.WriteLine("FileName : " + UploadFile1.FileName); // FileName : 매크로설정 (2007).jpg
                System.Diagnostics.Debug.WriteLine("ContentType : " + UploadFile1.ContentType); // ContentType : image/jpeg
                System.Diagnostics.Debug.WriteLine("Length : " + UploadFile1.Length); // Length : 127512
                System.Diagnostics.Debug.WriteLine("Name : " + UploadFile1.Name);   // Name : UploadFile1
                System.Diagnostics.Debug.WriteLine("ToString() : " + UploadFile1.ToString()); // ToString() : Microsoft.AspNetCore.Http.FormFile
                System.Diagnostics.Debug.WriteLine("Headers : " + UploadFile1.Headers); // Headers : Microsoft.AspNetCore.Http.HeaderDictionary
                System.Diagnostics.Debug.WriteLine("ContentDisposition: " + UploadFile1.ContentDisposition); // ContentDisposition: form-data; name="UploadFile1"; filename="매크로설정 (2007).jpg"

                UserJobCodes.Add(1);
            }
            if (UploadFile2 != null)
            {
                System.Diagnostics.Debug.WriteLine("file2 is not null");

                UserJobCodes.Add(2);
            }
            if (UploadFile3 != null)
            {
                System.Diagnostics.Debug.WriteLine("file3 is not null");

                UserJobCodes.Add(3);
            }
        }

        public void OnGet()
        {
            System.Diagnostics.Debug.WriteLine("Called JobAuthentication OnGet().");
            if (UserJobCodes != null)
            {
                System.Diagnostics.Debug.WriteLine($"UserJobCodes {0} ", UserJobCodes.Count());
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("UserJobCodes = null");
            }
        }
        public void OnPost()
        {
            System.Diagnostics.Debug.WriteLine("Called JobAuthentication OnPost().");

            UploadFiles();
        }
    }
}
