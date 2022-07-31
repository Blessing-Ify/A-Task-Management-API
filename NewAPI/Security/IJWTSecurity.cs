using NewAPI.Model;

namespace NewAPI.Security
{
    public interface IJWTSecurity
    {
        public string JWTGen(User user);

    }
}
