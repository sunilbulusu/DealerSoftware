
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
    public interface IDealManager
    {
        #region properties
        IDealRepository DealRepository { get; set; }
        #endregion

        #region crud
        string Save(IDeal deal);
        string Delete(Guid Id);
        #endregion

        #region validation

        #endregion

        #region logic
        #endregion

        #region getters
        IDeal GetById(Guid id);
        #endregion

        #region othermethods

        #endregion
    }

    public class DealManager : IDealManager
    {
        #region variables
        private IDealRepository _dealRepository;
        #endregion

        #region constructors
        public DealManager()
        { }

        public DealManager(IDealRepository dealRepository)
        {
            _dealRepository = dealRepository;
        }
        #endregion

        #region properties
        public IDealRepository DealRepository
        {
            get { return _dealRepository; }
            set { _dealRepository = value; }
        }
        #endregion

        #region crud

        public string Save(IDeal deal)
        {
            //if (_dealRepository == null)
            //    throw new ArgumentNullException("DealRepository cannot be null");
            //if (deal.Id == Guid.Empty)
            //{
            //    // new deal 
            //    deal.DateCreated = DateTime.Now;
            //    deal.Active = true;
            //    deal.Id = Guid.NewGuid();
            //    deal.DateModified = null;
            //    _dealRepository.Add((DF.Domain.Concrete.Deal)deal);

            //    _dealRepository.UnitOfWork.Commit();

            //}
            //else
            //{
            //    var oDeal = _dealRepository.Where(u => u.Id == deal.Id).SingleOrDefault();

            //    if (oDeal == null)
            //        throw new ArgumentNullException("deal");

            //    deal.DateCreated = oDeal.DateCreated;

            //    Magically.CopyPropertyValues(ref oDeal, (DF.Domain.Concrete.Deal)deal);

            //    _dealRepository.UnitOfWork.Commit();

            //}

            //return string.Format("Successfully saved deal {0} {1}", deal.FirstName, deal.LastName);

            return string.Empty;
        }

        public string Delete(Guid Id)
        {
            //if (_dealRepository == null)
            //    throw new ArgumentNullException("_dealRepository");

            //if (Id == Guid.Empty)
            //    throw new ArgumentNullException("Id");

            //var deal = _dealRepository.Where(u => u.Id == Id).SingleOrDefault();

            //if (deal == null || !deal.Active)
            //    return string.Empty;

            //deal.Active = false;

            //_dealRepository.UnitOfWork.Commit();

            //return string.Format("Marked deal {0} {1} as deleted.", deal.FirstName, deal.LastName);

            return string.Empty;
        }

        #endregion

        #region validation

        #endregion

        #region logic

        #endregion

        #region getters

        public IDeal GetById(Guid id)
        {
            //if (_dealRepository == null)
            //    throw new ArgumentNullException("DealRepository");

            //if (id == Guid.Empty)
            //    throw new ArgumentNullException("id");

            //return _dealRepository.Where(u => u.Id == id).SingleOrDefault();
            return null;
        }

        #endregion

        #region other methods

        #endregion

    }
}
