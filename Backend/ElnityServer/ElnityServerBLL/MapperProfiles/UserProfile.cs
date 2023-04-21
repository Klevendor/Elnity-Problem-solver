using AutoMapper;
using ElnityServerBLL.Dto.Account.Request;
using ElnityServerBLL.Dto.Account.Response;
using ElnityServerDAL.Entities.Identity;

namespace ElnityServerBLL.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserLoginResModel>();
            CreateMap<UserRegisterReqModel, ApplicationUser>().AfterMap((UserReq, AppUser) => AppUser.SecurityStamp = Guid.NewGuid().ToString());
        }
    }
}
