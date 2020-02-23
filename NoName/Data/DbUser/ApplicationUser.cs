using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

/**
 * 유저와 관련된 각각의 Table 클래스들
 */
namespace NoName.Data
{
    /**
     * 로그인 관련 페이지를 Scaffolding 해서 identity 관련 페이지를 사용하기 때문에
     * 이미 뷰에서 보여지고 처리되는 데이터가 코딩되어 있어서 필요한 테이블 정보가 정해져 있음
     */
    public class ApplicationUser : IdentityUser
    {
        public string UserID { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string ReciveSMS { get; set; }
        public string Authentication { get; set; }
    }

    public class Profile
    {

    }

    public class Authentication
    {

    }

    public class Permission
    {

    }

    public class Manager
    {

    }

    public class MyJob
    {

    }
}
