

using System.Collections.Generic;
using FowaProtocol;


namespace Server.BL.Contracts
{
    public interface IUserFriendsService
    {
        user GetUserById(int id);
        void AddUser(user user);
        void UpdateUser(user user);
        void DeleteUser(user user);
        void AddFriend(user user, user newFriend);
        void RemoveFriend(user user, user friendToRemove);
        IList<user> GetFriends(user user);
    }
}
