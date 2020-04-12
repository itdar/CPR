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
            //1. binding �Ǿ��ִ� IFormFile �����ͼ� �� �̸��� ���� ���ε� �ߴ��� Ȯ���ϰ� (�ּ� 1���� �־����)
            //2. ���� type (image�� �ǵ���) Ȯ���ؾ���
            //3. �����°� �����̸�, length, type ���� DB table ���� user ������ ���� �����ؾ���
            //4. �����ϴ� ��� ���� ���� ���� ��ġ�� ����/����
            //5. DB table ���� ������ ��� �����صΰ�, �ٸ� ���������� Ȥ�� �������� �ִ� ��� �ε��ؼ� �̹��� ���� �� Ȯ���ؾ���

            var userInfo = UserInformation.GetInstance();

            // �� ���� ���� �� ������ �� üũ�ؾ���
            if (UploadFile1 != null)
            {
                System.Diagnostics.Debug.WriteLine("file1 is not null");

                System.Diagnostics.Debug.WriteLine("FileName : " + UploadFile1.FileName); // FileName : ��ũ�μ��� (2007).jpg
                System.Diagnostics.Debug.WriteLine("ContentType : " + UploadFile1.ContentType); // ContentType : image/jpeg
                System.Diagnostics.Debug.WriteLine("Length : " + UploadFile1.Length); // Length : 127512
                System.Diagnostics.Debug.WriteLine("Name : " + UploadFile1.Name);   // Name : UploadFile1
                System.Diagnostics.Debug.WriteLine("ToString() : " + UploadFile1.ToString()); // ToString() : Microsoft.AspNetCore.Http.FormFile
                System.Diagnostics.Debug.WriteLine("Headers : " + UploadFile1.Headers); // Headers : Microsoft.AspNetCore.Http.HeaderDictionary
                System.Diagnostics.Debug.WriteLine("ContentDisposition: " + UploadFile1.ContentDisposition); // ContentDisposition: form-data; name="UploadFile1"; filename="��ũ�μ��� (2007).jpg"

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
