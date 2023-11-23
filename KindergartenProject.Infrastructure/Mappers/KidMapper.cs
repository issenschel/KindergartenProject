﻿using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Mappers
{
    public static class KidMapper
    {
        public static KidViewModel Map(KidEntity entity)
        {
            var viewModel = new KidViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                Surname = entity.Surname,
                Patronymic = entity.Patronymic,
                FullName = $"{entity.Name} {entity.Surname} {entity.Patronymic}",
                DateOfBirth = entity.DateOfBirth,
                GroupId = entity.GroupId,
                GroupName = entity.Group.Name,
            };
            return viewModel;
        }

        public static List<KidViewModel> Map(List<KidEntity> entities) 
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
