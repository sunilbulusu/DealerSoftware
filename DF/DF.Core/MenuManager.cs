
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
    public interface IMenuManager
    {
        #region properties
        IMenuRepository MenuRepository { get; set; }
        #endregion

        #region crud
        string Save(IMenu menu);
        string Delete(int menuId);
        #endregion

        #region validation

        #endregion

        #region logic
        #endregion

        #region getters
        IMenu GetById(int id);
        #endregion

        #region othermethods

        #endregion
    }

    public class MenuManager : IMenuManager
    {
        #region variables
        private IMenuRepository _menuRepository;
        #endregion

        #region constructors
        public MenuManager()
        { }

        public MenuManager(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        #endregion

        #region properties
        public IMenuRepository MenuRepository
        {
            get { return _menuRepository; }
            set { _menuRepository = value; }
        }
        #endregion

        #region crud

        public string Save(IMenu menu)
        {
            if (_menuRepository == null)
                throw new ArgumentNullException("MenuRepository cannot be null");
            if (menu.Id == 0)
            {
                // new menu 
                menu.DateCreated = DateTime.Now;
                menu.Active = true;
                menu.DateModified = null;
                _menuRepository.Add((DF.Domain.Concrete.Menu)menu);

                _menuRepository.UnitOfWork.Commit();

            }
            else
            {
                var oMenu = _menuRepository.Where(u => u.Id == menu.Id).SingleOrDefault();

                if (oMenu == null)
                    throw new ArgumentNullException("menu");

                menu.DateCreated = oMenu.DateCreated;

                Magically.CopyPropertyValues(ref oMenu, (Menu)menu);

                _menuRepository.UnitOfWork.Commit();

            }

            return string.Format("Successfully saved menu {0}", menu.DisplayText);
        }

        public string Delete(int menuId)
        {
            return string.Empty;
        }

        #endregion

        #region validation

        #endregion

        #region logic

        #endregion

        #region getters

        public IMenu GetById(int id)
        {
            if (_menuRepository == null)
                throw new ArgumentNullException("MenuRepository");

            if (id == 0)
                throw new ArgumentNullException("id");

            return _menuRepository.Where(u => u.Id == id).SingleOrDefault();
        }

        #endregion

        #region other methods

        #endregion

    }
}
