﻿using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class DayOfTheWeekRepository : IBaseRepository<DayOfTheWeekViewModel>
    {
        //Получает все объекты из базы данных и возвращает их список после применения маппинга
        public List<DayOfTheWeekViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.DaysOfTheWeeks.ToList();
                return DayOfTheWeekMapper.Map(items);
            }
        }

        //Выполняет поиск объектов по указанной строке.Возвращает список найденных объектов после применения маппинга
        public List<DayOfTheWeekViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.DaysOfTheWeeks
                    .Where(x => x.Name.ToLower().Contains(search)).ToList();

                return DayOfTheWeekMapper.Map(result);
            }
        }
        //Получает объект из базы данных по указанному id и возвращает его после применения маппинга
        public DayOfTheWeekViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.DaysOfTheWeeks.FirstOrDefault(x => x.ID == id);
                return DayOfTheWeekMapper.Map(item);
            }
        }

        //Добавляет новый объект в базу данных. Производит маппинг перед добавлением, сохраняет изменения и возвращает добавленный объект после применения маппинга
        public DayOfTheWeekViewModel Add(DayOfTheWeekViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = DayOfTheWeekMapper.Map(viewModel);

                context.DaysOfTheWeeks.Add(entity);
                context.SaveChanges();

                return DayOfTheWeekMapper.Map(entity);
            }
        }

        //Обновляет существующий объект в базе данных. Ищет объект по указанному id сохраняет изменения и возвращает объект после применения маппинга
        public DayOfTheWeekViewModel Update(DayOfTheWeekViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.DaysOfTheWeeks.FirstOrDefault(x => x.ID == viewModel.ID);
                if (entity == null)
                    return null;

                entity.Name = viewModel.Name;

                context.SaveChanges();

                return DayOfTheWeekMapper.Map(entity);
            }
        }
        //удаляет объект из базы данных по указанному id. Ищет объект для удаления, выполняет маппинг перед удалением, удаляет объект из базы данных
        //и возвращает удаленный объект после применения маппинга
        public DayOfTheWeekViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var entity = context.DaysOfTheWeeks.FirstOrDefault(x => x.ID == id);
                if (entity == null)
                    return null;

                DayOfTheWeekViewModel viewModel = DayOfTheWeekMapper.Map(entity);

                context.DaysOfTheWeeks.Remove(entity);
                context.SaveChanges();

                return viewModel;
            }
        }
    }
}
