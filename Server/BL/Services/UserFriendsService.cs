using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.BL.Contracts;
using Server.DAL;
using Server.DAL.Contracts;
using Server.DL;

namespace Server.BL.Services
{
    public class UserFriendsService : IUserFriendsService
    {
        private readonly IRepository<user> _userRepository;
        private readonly IRepository<friends> _friendsRepository;
        private readonly fowaEntities _fowaDbContext;
        private readonly object _lock = new object();

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

        public user GetUserbyEmail(string email)
        {
            var user = (from u in _userRepository.Table
                        where u.email.Contains(email)
                        select u).FirstOrDefault();
            return user;
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
            // We don't have to implicitly delete all the friends of this user because
            // (delete) dissemination is enabled on the MySql-Server. (ON DELETE/UPDATE CASCADE in the foreign key options)
            _userRepository.Delete(user);
            Commit();
        }

        public IList<user> GetFriends(user user)
        {
            IQueryable<int> friendIds = from f in _friendsRepository.Table
                                        where f.U_ID == user.ID
                                        select f.F_ID;

            IQueryable<user> friends = from u in _userRepository.Table
                                       from friendId in friendIds
                                       where u.ID == friendId
                                       select u;

            return friends.ToList(); // Execute query
        }


        public void AddFriend(user user, user newFriend)
        {
            _friendsRepository.Insert(new friends { F_ID = newFriend.ID, U_ID = user.ID });
            Commit();
        }

        public void RemoveFriend(user user, user friendToRemove)
        {
            _friendsRepository.Delete(new friends{U_ID = user.ID, F_ID = friendToRemove.ID});
            Commit();
        }

        public bool UserExists(string email) // email is an uniq field
        {
            // If the LINQ query is executed in database context, a call to Contains() is mapped to the LIKE operator:
            // .Where(a => a.Field.Contains("hello")) becomes Field LIKE '%hello%'. The LIKE operator is case insensitive by default.
            //
            // Tested:
            // hans@gmx.net an email wich alredy exsists.
            // email = hans@gmx.neT --> return true
            // email = hans@gmx.nett --> return false
            // email = hhans@gmx.net --> return false

            //open two clients (start debugging and go to the debug/release folder) .. now fill the input fields and send the data to the server at the same time.
            //without this lock below, you will got an error.
            lock (_lock)
            {
                return _userRepository.Table.FirstOrDefault(a => a.email.Contains(email)) != null;
            }
        }

        private void Commit()
        {
            _fowaDbContext.SaveChanges();
        }

    }
}
