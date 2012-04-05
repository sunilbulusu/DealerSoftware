
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF.Domain.Abstract;
using DF.Repositories;
using Magic;
using Magic.Extensions;
using DF.Domain.Concrete;

namespace DF.Core
{
    public interface IUserManager
    {
        #region properties
        IUserRepository UserRepository { get; set; }
        #endregion

        #region crud
        string Save(IUser user);
        string Delete(Guid Id);
        #endregion

        #region validation

        #endregion

        #region logic
        #endregion

        #region getters
        IUser GetById(Guid id);
        #endregion

        #region othermethods

        #endregion
    }

    public class UserManager : IUserManager
    {
        #region variables
        private IUserRepository _userRepository;
        #endregion

        #region constructors
        public UserManager()
        { }

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        #region properties
        public IUserRepository UserRepository
        {
            get { return _userRepository; }
            set { _userRepository = value; }
        }
        #endregion

        #region crud

        public string Save(IUser user)
        {
            if (_userRepository == null)
                throw new ArgumentNullException("UserRepository cannot be null");
            if (user.Id == Guid.Empty)
            {
                // new user 
                user.DateCreated = DateTime.Now;
                user.Active = true;
                user.Id = Guid.NewGuid();
                user.DateModified = null;
                _userRepository.Add((DF.Domain.Concrete.User)user);

                _userRepository.UnitOfWork.Commit();

            }
            else
            {
                var oUser = _userRepository.Where(u => u.Id == user.Id).SingleOrDefault();

                if (oUser == null)
                    throw new ArgumentNullException("user");

                user.DateCreated = oUser.DateCreated;

                Magically.CopyPropertyValues(ref oUser, (DF.Domain.Concrete.User)user);

                _userRepository.UnitOfWork.Commit();

            }

            return string.Format("Successfully saved user {0} {1}", user.FirstName, user.LastName);
        }

        public string Delete(Guid Id)
        {
            if (_userRepository == null)
                throw new ArgumentNullException("_userRepository");

            if (Id == Guid.Empty)
                throw new ArgumentNullException("Id");

            var user = _userRepository.Where(u => u.Id == Id).SingleOrDefault();

            if (user == null || !user.Active)
                return string.Empty;

            user.Active = false;

            _userRepository.UnitOfWork.Commit();

            return string.Format("Marked user {0} {1} as deleted.", user.FirstName, user.LastName);
        }

        #endregion

        #region validation

        #endregion

        #region logic

        #endregion

        #region getters

        public IUser GetById(Guid id)
        {
            if (_userRepository == null)
                throw new ArgumentNullException("UserRepository");

            if (id == Guid.Empty)
                throw new ArgumentNullException("id");

            return _userRepository.Where(u => u.Id == id).SingleOrDefault();
        }

        #endregion

        #region other methods

        #endregion

    }
}
