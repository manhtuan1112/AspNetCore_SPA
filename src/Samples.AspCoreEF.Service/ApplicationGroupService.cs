using Sample.AspCoreEF.Common.Exceptions;
using Samples.AspCoreEF.DAL.EF.Models;
using Samples.AspCoreEF.DAL.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.AspCoreEF.Service
{
    public interface IApplicationGroupService
    {
        ApplicationGroup Detail(long id);
        IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter);
        IEnumerable<ApplicationGroup> GetAll();
        ApplicationGroup Add(ApplicationGroup appGroup);
        void Update(ApplicationGroup appGroup);
        void Delete(long id);
        bool AddUserToGroups(IEnumerable<ApplicationUserGroup> userGroups, string userId);
        IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId);
        IEnumerable<ApplicationUser> GetListUserByGroupId(long groupId);
        
    }
    public class ApplicationGroupService : IApplicationGroupService
    {
        private IApplicationGroupRepository _appGroupRepository;
        private IApplicationUserGroupRepository _appUserGroupRepository;
        public ApplicationGroupService(IApplicationGroupRepository appGroupRepository, IApplicationUserGroupRepository appUserGroupRepository)
        {
            this._appGroupRepository = appGroupRepository;
            this._appUserGroupRepository = appUserGroupRepository;
        }

        public ApplicationGroup Add(ApplicationGroup appGroup)
        {
            if (_appGroupRepository.CheckContains(x => x.Name == appGroup.Name))
                throw new NameDuplicatedException("This Group Name already exists !Please choose another");
              var newAppGroup= _appGroupRepository.Insert(appGroup);
            return newAppGroup;
        }

      
        public bool AddUserToGroups(IEnumerable<ApplicationUserGroup> userGroups, string userId)
        {
            _appUserGroupRepository.DeleteMulti(x => x.UserId == userId);
            foreach (var userGroup in userGroups)
            {
                _appUserGroupRepository.Insert(userGroup);
            }
            return true;
        }

        public void Delete(long id)
        {
            var appGroup = this._appGroupRepository.GetSingleById(id);
             _appGroupRepository.Delete(appGroup);
        }

        public ApplicationGroup Detail(long id)
        {
            return _appGroupRepository.GetSingleById(id);
        }

        public IEnumerable<ApplicationGroup> GetAll()
        {
            return _appGroupRepository.GetAll();
        }

        public IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter)
        {
            var query = _appGroupRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Name.Contains(filter));

            totalRow = query.Count();
            return query.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId)
        {
            return _appGroupRepository.GetListGroupByUserId(userId);
        }

        public IEnumerable<ApplicationUser> GetListUserByGroupId(long groupId)
        {
            return _appGroupRepository.GetListUserByGroupId(groupId);
        }

        public void Update(ApplicationGroup appGroup)
        {
            if (_appGroupRepository.CheckContains(x => x.Name == appGroup.Name && x.ID != appGroup.ID))
                throw new NameDuplicatedException("This Group Name already exists !Please choose another");
            _appGroupRepository.Update(appGroup);
        }
    }
}
