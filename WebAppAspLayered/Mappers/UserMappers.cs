using WebAppAspLayered.DL.Entities;
using WebAppAspLayered.Models.Users;

namespace WebAppAspLayered.Mappers;

public static class UserMappers
{
    public static User ToUser(this RegisterFormDto form)
    {
        return new User()
        {
            Email = form.Email,
            Password = form.Password,
        };
    }
}
