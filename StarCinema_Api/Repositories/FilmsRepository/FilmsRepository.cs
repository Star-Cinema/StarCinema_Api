﻿using StarCinema_Api.DTOs;
using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.Data;
using StarCinema_Api.Repositories.FilmsRepository;
using System.Linq.Expressions;
using StarCinema_Api.DTOs;
using System.Threading.Tasks;
using System;


namespace StarCinema_Api.Repositories.FilmsRepository
{
    public class FilmsRepository : IFilmsRepository
    {
        private readonly MyDbContext _context;
        public FilmsRepository(MyDbContext context)
        {
            _context = context;
        }

        //METHOD: GET ALL FILM 
        public async Task<PaginationDTO<Films>> getAllFilms(string? search,
            int page = 0, int limit = 10)
        {
            var query = _context.Films
                 .Where(s => s.IsDelete == false)
                .Select(x => new Films
                {
                    Id = x.Id,
                    Name = x.Name,
                    Producer = x.Producer,
                    Director = x.Director,
                    Duration = x.Duration,
                    Description = x.Description,
                    Country = x.Country,
                    Release = x.Release,
                    IsDelete = x.IsDelete,
                    VideoLink = x.VideoLink,
                    Category = x.Category,
                    Images = x.Images,
                    Schedules = x.Schedules
                })
                .AsQueryable();
            if (search != null)
            {
                //query = query.Where(s => s.Name == name);
                query = query.Where(s => s.Name.Contains(search)
                               || s.Director.Contains(search)
                               || s.Country.Contains(search)
                               || s.Category.Name.Contains(search));
            }
            
            var films = await query.ToListAsync();
            var pagination = new PaginationDTO<Films>();

            pagination.TotalCount = films.Count;

            //films = films.Skip(limit * page).Take(limit).ToList();
            pagination.PageSize = limit;
            pagination.Page = page;
            pagination.ListItem = films;
            return pagination;
        }
      

        //METHOD: GET ALL NOW SHOWING FILM 
        //from list schedule, if first start date <= today <= last start date
        public async Task<List<Films>> getNowShowingFilms()
        {
            var query = _context.Films
                .Where(s => s.IsDelete == false &&
                s.Schedules.OrderBy(e => e.StartTime).First().StartTime.Day <= DateTime.Today.Day &&
                s.Schedules.OrderBy(e => e.StartTime).Last().EndTime.Day >= DateTime.Today.Day
                //s.Schedules.Any(s => s.StartTime.Day == DateTime.Today.Day) ||
                // s.Schedules.Any(s => s.StartTime.Day >= DateTime.Today.Day)
                )

                .Select(x => new Films
                {
                    Id = x.Id,
                    Name = x.Name,
                    Producer = x.Producer,
                    Director = x.Director,
                    Duration = x.Duration,
                    Description = x.Description,
                    Country = x.Country,
                    Release = x.Release,
                    IsDelete = x.IsDelete,
                    VideoLink = x.VideoLink,
                    Category = x.Category,
                    Images = x.Images,
                    Schedules = x.Schedules
                })
                .AsQueryable();


            var films = await query.ToListAsync();

            return films;
        }

        //METHOD: GET ALL UPCOMING FILM 
        //from list schedule, if first start date > today
        public async Task<List<Films>> getUpComingFilms()
        {
            var query = _context.Films
                 .Where(s => s.IsDelete == false &&
                          //DateTime.Compare(s.Schedules.OrderBy(e => e.StartTime).First().StartTime, DateTime.Today) > 0
                          s.Schedules.OrderBy(e => e.StartTime).First().StartTime.Day > DateTime.Today.Day
                 )
                .Select(x => new Films
                {
                    Id = x.Id,
                    Name = x.Name,
                    Producer = x.Producer,
                    Director = x.Director,
                    Duration = x.Duration,
                    Description = x.Description,
                    Country = x.Country,
                    Release = x.Release,
                    IsDelete = x.IsDelete,
                    VideoLink = x.VideoLink,
                    Category = x.Category,
                    Images = x.Images,
                    Schedules = x.Schedules
                })
                .AsQueryable();


            var films = await query.ToListAsync();

            return films;
        }


        // METHOD: GET FILM BY ID 
        public async Task<Films> getFilmById(int filmId)
        {
            return await _context.Films
                .Select(x => new Films
                {
                    Id = x.Id,
                    Name = x.Name,
                    Producer = x.Producer,
                    Director = x.Director,
                    Duration = x.Duration,
                    Description = x.Description,
                    Country = x.Country,
                    Release = x.Release,
                    IsDelete = x.IsDelete,
                    VideoLink = x.VideoLink,
                    Category = x.Category,
                    Images = x.Images,
                    Schedules = x.Schedules
                })
                .Where(s => s.Id == filmId).FirstOrDefaultAsync();
        }

        // METHOD CREATE FILM
        public void CreateFilm(Films film)
        {

            _context.Films.Add(film);
        }

        // METHOD CREATE IMAGE
        public async void CreateImage(Images image)
        {

            _context.Images.Add(image);
            _context.SaveChanges();
        }

        // METHOD DELETE FILM
        public void DeleteFilm(Films film)
        {
            _context.Films.Remove(film);

        }

        // METHOD UPDATE FILM
        public void UpdateFilm(Films film)
        {
            _context.Entry(film).State = EntityState.Modified;
        }

        public bool SaveChange()
        {
            return _context.SaveChanges() > 0;
        }

        // GET LAST FILM ID FOR INSERT INTO IMAGES TABLE
        public async Task<int> GetLastIDFilm()
        {
            return _context.Films.OrderBy(s => s.Id).LastOrDefaultAsync().Result.Id;
        }

    }
}
