using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.FilmsRepository;
using static System.Net.Mime.MediaTypeNames;

namespace StarCinema_Api.Services.FilmsService
{
    public class FilmsService : IFilmsService
    {
        private readonly IFilmsRepository _filmsRepository;
        private readonly IMapper _mapper;
        public FilmsService(IFilmsRepository filmsRepository,
            IMapper mapper)
        {
            _filmsRepository = filmsRepository;
            _mapper = mapper;
        }

        // CREATE FILM
        public async Task<ResponseDTO> CreateFilm([FromForm] FilmDTO filmDTO)
        {
            try
            {

                //var film = _mapper.Map<FilmDTO, Films>(filmDTO);
                var film = _mapper.Map<Films>(filmDTO);
                //FIX YOUTUBE VIDEO LINK
                int delimiters = film.VideoLink.Count(x => x == '/');

                if (delimiters == 3)
                { film.VideoLink = film.VideoLink.Split("/")[3]; }
                else { film.VideoLink = film.VideoLink; }

                var filmList = _filmsRepository.getAllFilms(null).Result.ListItem;
                var IsExist = IsFilmExist(film, filmList);
                if (IsExist)
                {
                    return new ResponseDTO
                    {
                        code = 400,
                        message = $"The Film is existed! Please re enter!"
                    };
                }

                _filmsRepository.CreateFilm(film);
                _filmsRepository.SaveChange();

                //Upload images
                var filmID = await _filmsRepository.GetLastIDFilm();

                // Code to get image link
                //.....

                foreach (var item in filmDTO.Image)
                {

                    if (item.Name != null && item.Name.Length > 0)
                    {
                       
                        var image = new Images
                        {
                            FilmId = filmID,
                            Name = item.Name, 
                            Path = item.Path 
                        };
                        _filmsRepository.CreateImage(image);
                        //_filmsRepository.SaveChange();
                    }
                }

                return new ResponseDTO
                {
                    code = 200,
                    message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    code = 500,
                    message = ex.Message
                };
            }
        }

        public bool IsFilmExist(Films newFilm, List<Films> filmList)
        {

            if (filmList.Count == 0) return false;
            foreach (var film in filmList)
            {
                if (newFilm.Name.Equals(film.Name, StringComparison.OrdinalIgnoreCase)
                    && newFilm.CategoryId == film.CategoryId
                    && newFilm.Director.Equals(film.Director, StringComparison.OrdinalIgnoreCase)
                    && newFilm.Country.Equals(film.Country, StringComparison.OrdinalIgnoreCase)
                    && film.IsDelete == false
                    )
                {
                    return true;
                }
            }
            return false;
        }

        // METHOD UPDATE
        public async Task<ResponseDTO> UpdateFilm(int id, FilmDTO filmDTO)
        {
            try
            {

                var filmCurrent = await _filmsRepository.getFilmById(id);
                if (filmCurrent == null) return new ResponseDTO
                {
                    code = 400,
                    message = $"Does not exist film with id {id}"
                };
                var filmNew = _mapper.Map<FilmDTO, Films>(filmDTO);
                filmNew.Id = id;

                //FIX YOUTUBE VIDEO LINK
                int delimiters = filmNew.VideoLink.Count(x => x == '/');
                if (delimiters == 3)
                { filmNew.VideoLink = filmNew.VideoLink.Split("/")[3]; }
                else { filmNew.VideoLink = filmNew.VideoLink; }

                var filmList = _filmsRepository.getAllFilms(null).Result.ListItem;
                filmList = filmList.Where(s => s.Id != filmCurrent.Id).ToList();
                var IsExist = IsFilmExist(filmNew, filmList);
                if (IsExist)
                {
                    return new ResponseDTO
                    {
                        code = 400,
                        message = $"The modifying film is exist"
                    };
                }

                _filmsRepository.UpdateFilm(filmNew);

                _filmsRepository.SaveChange();
                return new ResponseDTO { code = 200, message = "Success" };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    code = 500,
                    message = ex.Message
                };
            }
        }

        // METHOD DELETE
        public async Task<ResponseDTO> DeleteFilmById(int id)
        {
            try
            {
                var film = await _filmsRepository.getFilmById(id);
                if (film == null) return new ResponseDTO
                {
                    code = 404,
                    message = $"Does not exist film with id {id}",
                };

                else
                {
                    film.IsDelete = true;
                    _filmsRepository.UpdateFilm(film);
                    _filmsRepository.SaveChange();
                }

                //_filmsRepository.DeleteFilm(film);
                //_filmsRepository.SaveChange();
                return new ResponseDTO
                {
                    code = 200,
                    message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    code = 500,
                    message = ex.Message
                };
            }
        }

        //METHOD GET ALL FILM
        public async Task<ResponseDTO> GetAllFilms(string? search, int page = 0, int limit = 10)
        {
            try
            {
                var result = await _filmsRepository.getAllFilms(search);
                return new ResponseDTO
                {
                    code = 200,
                    message = "Success",
                    data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    code = 500,
                    message = ex.Message
                };
            }
        }

        //METHOD GET ALL NOW SHOWING FILM
        public async Task<ResponseDTO> getNowShowingFilms()
        {
            try
            {
                var result = await _filmsRepository.getNowShowingFilms();
                return new ResponseDTO
                {
                    code = 200,
                    message = "Success",
                    data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    code = 500,
                    message = ex.Message
                };
            }
        }

        //METHOD GET ALL UPCOMING FILM
        public async Task<ResponseDTO> getUpComingFilms()
        {
            try
            {
                var result = await _filmsRepository.getUpComingFilms();
                return new ResponseDTO
                {
                    code = 200,
                    message = "Success",
                    data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    code = 500,
                    message = ex.Message
                };
            }
        }
        //METHOD GET FILM BY ID
        public async Task<ResponseDTO> GetFilmById(int id)
        {
            try
            {
                var result = await _filmsRepository.getFilmById(id);
                if (result == null) return new ResponseDTO
                {
                    code = 400,
                    message = $"Does not exist film with id {id}"
                };
                return new ResponseDTO
                {
                    code = 200,
                    message = "Success",
                    data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    code = 500,
                    message = ex.Message
                };
            }
        }
    }
}
