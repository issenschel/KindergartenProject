﻿using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class KindergartenRepository : IBaseRepository<KindergartenViewModel>
    {
        //Получает все объекты из базы данных и возвращает их список после применения маппинга
        public List<KindergartenViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Kindergartens.ToList();
                return KindergartenMapper.Map(items);
            }
        }

        //Выполняет поиск объектов по указанной строке.Возвращает список найденных объектов после применения маппинга
        public List<KindergartenViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.Kindergartens.ToList()
                    .Where(x => x.Name.ToLower().Contains(search))
                    .ToList();

                return KindergartenMapper.Map(result);
            }
        }

        //Получает объект из базы данных по указанному id и возвращает его после применения маппинга
        public KindergartenViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Kindergartens.FirstOrDefault(x => x.ID == id);
                return KindergartenMapper.Map(item);
            }
        }

        //Добавляет новый объект в базу данных. Производит маппинг перед добавлением, сохраняет изменения и возвращает добавленный объект после применения маппинга
        public KindergartenViewModel Add(KindergartenViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = KindergartenMapper.Map(viewModel);

                context.Kindergartens.Add(entity);
                context.SaveChanges();

                return KindergartenMapper.Map(entity);
            }
        }

        //Обновляет существующий объект в базе данных. Ищет объект по указанному id сохраняет изменения и возвращает объект после применения маппинга
        public KindergartenViewModel Update(KindergartenViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.Kindergartens.FirstOrDefault(x => x.ID == viewModel.ID);
                if (entity == null)
                    return null;

                entity.Name = viewModel.Name;
                entity.Address = viewModel.Address;
                entity.StartTime = viewModel.StartTime;
                entity.EndTime = viewModel.EndTime;

                context.SaveChanges();

                return KindergartenMapper.Map(entity);
            }
        }

        //удаляет объект из базы данных по указанному id. Ищет объект для удаления, выполняет маппинг перед удалением, удаляет объект из базы данных
        //и возвращает удаленный объект после применения маппинга
        public KindergartenViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var entity = context.Kindergartens.FirstOrDefault(x => x.ID == id);
                if (entity == null)
                    return null;

                KindergartenViewModel viewModel = KindergartenMapper.Map(entity);

                context.Kindergartens.Remove(entity);
                context.SaveChanges();

                return viewModel;
            }
        }
    }
}
