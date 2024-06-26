﻿using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Mappers
{
    public static class UserMapper
    {
        //Преобразует объект класса Entity в объект класса ViewModel
        public static UserViewModel Map(UserEntity entity)
        {
            var viewModel = new UserViewModel
            {
                ID = entity.ID,
                Login = entity.Login,
                Password = entity.Password,
                RoleId = entity.RoleId,
                RoleName = entity.Role.Name
            };
            return viewModel;
        }

        //Принимает список объектов класса Entity и преобразует каждый объект в соответствующий объект класса
        public static List<UserViewModel> Map(List<UserEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }

        //Метод Map преобразует объект класса ViewModel в объект класса Entity
        public static UserEntity Map(UserViewModel viewModel)
        {
            var entity = new UserEntity
            {
                ID = viewModel.ID,
                Login = viewModel.Login,
                Password = viewModel.Password,
                RoleId = viewModel.RoleId
            };
            return entity;
        }

        //Принимает список объектов класса ViewModel и преобразует каждый объект в соответствующий объект класса
        public static List<UserEntity> Map(List<UserViewModel> viewModels)
        {
            var entities = viewModels.Select(vm => Map(vm)).ToList();
            return entities;
        }
    }
}
