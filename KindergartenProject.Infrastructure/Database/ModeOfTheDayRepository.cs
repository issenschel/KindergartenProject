﻿using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class ModeOfTheDayRepository : IBaseRepository<ModeOfTheDayViewModel>
    {
        private DateTime date;
        //Получает все объекты из базы данных и возвращает их список после применения маппинга
        public List<ModeOfTheDayViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.ModesOfTheDays.ToList();
                return ModeOfTheDayMapper.Map(items);
            }
        }

        //Выполняет поиск объектов по указанной строке.Возвращает список найденных объектов после применения маппинга
        public List<ModeOfTheDayViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.ModesOfTheDays.ToList()
                    .Where(x => x.Occupation.Name.ToLower().Contains(search))
                    .ToList();

                return ModeOfTheDayMapper.Map(result);
            }
        }

        //Получает объект из базы данных по указанному id и возвращает его после применения маппинга
        public ModeOfTheDayViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.ModesOfTheDays.FirstOrDefault(x => x.ID == id);
                return ModeOfTheDayMapper.Map(item);
            }
        }

        //Получает объект из базы данных по указанному id и возвращает его после применения маппинга
        public List<ModeOfTheDayViewModel> GetByGroupId(long groupId)
        {
            using (var context = new Context())
            {
                var items = context.ModesOfTheDays.Where(m => m.GroupId == groupId).ToList();
                return ModeOfTheDayMapper.Map(items);
            }
        }

        //Добавляет новый объект в базу данных. Производит маппинг перед добавлением, сохраняет изменения и возвращает добавленный объект после применения маппинга
        public ModeOfTheDayViewModel Add(ModeOfTheDayViewModel viewModel)
        {
            using (var context = new Context())
            {
                var occupation = context.Occupations.FirstOrDefault(o => o.Name == viewModel.OccupationName);
                if (occupation == null)
                {
                    occupation = new OccupationEntity { Name = viewModel.OccupationName };
                    if (occupation.Name == "Занятие")
                    {
                        throw new Exception("Занятие не может быть пустым");
                    }
                    context.Occupations.Add(occupation);
                    context.SaveChanges();
                }

                var entity = ModeOfTheDayMapper.Map(viewModel);
                entity.OccupationId = occupation.ID; // Присвоение ID новой Occupation

                if (!DateTime.TryParseExact(entity.StartTime, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    throw new Exception("Дата должна быть в формате времени (например, 14:30)");
                }
                if (!DateTime.TryParseExact(entity.EndTime, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    throw new Exception("Дата должна быть в формате времени (например, 14:30)");
                }

                context.ModesOfTheDays.Add(entity);
                context.SaveChanges();

                return ModeOfTheDayMapper.Map(entity);
            }
        }

        //Обновляет существующий объект в базе данных. Ищет объект по указанному id сохраняет изменения и возвращает объект после применения маппинга
        public ModeOfTheDayViewModel Update(ModeOfTheDayViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.ModesOfTheDays.FirstOrDefault(x => x.ID == viewModel.ID);
                if (entity == null)
                    return null;

                // Сначала обновляем информацию о занятии, если она изменилась.
                var occupation = context.Occupations.FirstOrDefault(o => o.ID == entity.OccupationId);
                if (occupation != null && !string.Equals(occupation.Name, viewModel.OccupationName, StringComparison.OrdinalIgnoreCase))
                {
                    occupation.Name = viewModel.OccupationName;
                    if (occupation.Name == "Занятие")
                    {
                        throw new Exception("Занятие не может быть пустым");
                    }
                    // Здесь не вызываем context.SaveChanges(), так как это будет сделано ниже, после всех изменений.
                }

                
                entity.StartTime = viewModel.StartTime;

                if (!DateTime.TryParseExact(entity.StartTime, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    throw new Exception("Дата должна быть в формате времени (например, 14:30)");
                }

                entity.EndTime = viewModel.EndTime;

                if (!DateTime.TryParseExact(entity.EndTime, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    throw new Exception("Дата должна быть в формате времени (например, 14:30)");
                }

                entity.GroupId = viewModel.GroupId;

                // Сохраняем все изменения в контексте.
                context.SaveChanges();

                // Возвращаем обновлённый ViewModel.
                return ModeOfTheDayMapper.Map(entity);
            }
        }


        //удаляет объект из базы данных по указанному id. Ищет объект для удаления, выполняет маппинг перед удалением, удаляет объект из базы данных
        //и возвращает удаленный объект после применения маппинга
        public ModeOfTheDayViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var entity = context.ModesOfTheDays.FirstOrDefault(x => x.ID == id);
                if (entity == null)
                    return null;

                ModeOfTheDayViewModel viewModel = ModeOfTheDayMapper.Map(entity);

                context.ModesOfTheDays.Remove(entity);
                context.SaveChanges();

                return viewModel;
            }
        }

        //Используется для получения группы по её названию.
        public long? GetGroupIdByName(string groupName)
        {
            using (var context = new Context())
            {
                var group = context.Groups.FirstOrDefault(g => g.Name == groupName);
                return group?.ID;
            }
        }

        //Используется для получения списка групп.
        public List<GroupViewModel> GetGroups()
        {
            using (var context = new Context())
            {
                var items = context.Groups.ToList();
                return GroupMapper.Map(items);
            }
        }
    }
}
