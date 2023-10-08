using Project.Business.Abstract;
using Project.Core.Business;
using Project.DataAccess.Abstract;
using Project.Models;

namespace Project.Business.Concrete
{
    public class UserManager: IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal) 
        {
            _userDal = userDal;
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public List<UserToShowDto> GetAll()
        {
            return _userDal.GetAll().Select(u=>new UserToShowDto { ID=u.ID,Email=u.Email,Username=u.Username,Role=u.Role}).ToList();
        }
        public List<UserToShowDto> GetByRole(string role)
        {
             return GetAll().Where(u => u.Role == role).ToList( );
        }

        public User GetById(Guid id)
        {
            var user = _userDal.Get(u=>u.ID == id);
            return user;
        }

        public User GetByMail(string mail)
        {
            var user = _userDal.Get(u => u.Email == mail);
            return user;
        }

        public User GetByRefreshToken(string refreshToken)
        {
            var user = _userDal.Get(u => u.RefreshToken == refreshToken);
            return user;
        }

        public void Update(User user)
        {
            _userDal.Update(user);
        }
    }
}
