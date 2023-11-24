﻿using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class RoleRepository
    {
        public List<RoleViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Roles.ToList();
                return RoleMapper.Map(items);
            }
        }

        public RoleViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Roles.FirstOrDefault(x => x.ID == id);
                return RoleMapper.Map(item);
            }
        }
    }
}