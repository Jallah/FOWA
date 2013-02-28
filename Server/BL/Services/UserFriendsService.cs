using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.BL.Contracts;
using Server.DAL;
using Server.DL;
using Server.DL.Contracts;

namespace Server.BL.Services
{
    public class UserFriendsService : IUserFriendsService
    {
        private readonly IRepository<user> _userRepository;
        private readonly IRepository<friends> _friendsRepository;
        private readonly fowaEntities _fowaDbContext;

        public UserFriendsService()
        {
            _fowaDbContext = new fowaEntities();
            _userRepository = new Repository<user>(_fowaDbContext);
            _friendsRepository = new Repository<friends>(_fowaDbContext);
        }

        public user GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }

        public void AddUser(user user)
        {
            _userRepository.Insert(user);
            Commit();
        }

        public void UpdateUser(user user)
        {
            _userRepository.Update(user);
            Commit();
        }

        public void DeleteUser(user user)
        {
            _userRepository.Delete(user);
            Commit();
        }

        public IList<user> GetFriends(user user)
        {
            // Inner Join (see FowaModel)
            var friends = (from u in _userRepository.Table
                           join f in _friendsRepository.Table on u.ID equals f.F_ID
                           select u).ToList();

            return friends;
        }


        public void AddFriend(user user, user newFriend)
        {
            _friendsRepository.Insert(new friends { F_ID = newFriend.ID, U_ID = user.ID });
            Commit();
        }

        public void RemoveFriend(user user, user friendToRemove)
        {
            throw new NotImplementedException();
        }

        private void Commit()
        {
            _fowaDbContext.SaveChanges();
        }

       
    }
}
