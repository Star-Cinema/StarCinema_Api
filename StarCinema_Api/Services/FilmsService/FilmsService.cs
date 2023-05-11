using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.FilmsRepository;
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

        public async Task<ResponseDTO> CreateFilm([FromForm] FilmDTO filmDTO)
        {
            try
            {

                var film = _mapper.Map<FilmDTO, Films>(filmDTO);
                var filmList = _filmsRepository.getAllFilms(null, null, null, null).Result.ListItem;
                var IsExist = IsFilmExist(film, filmList);
                if (IsExist)
                {
                    return new ResponseDTO
                    {
                        code = 400,
                        message = $"The Film is existed!"
                    };
                }

                _filmsRepository.CreateFilm(film);
                _filmsRepository.SaveChange();

                //Upload images
                var lastFilmId = filmDTO.Id;

                // Code to get image link
                //.....

                //foreach (var item in filmDTO.Images)
                //{

                //if (item.FileName != null && item.FileName.Length > 0)
                //{
                //    //Insert In User Profile table
                //    var path = Path.Combine("~/Public/Library/Images/", item.FileName);
                //    //using (FileStream stream = new FileStream(path, FileMode.Create))
                //    //{
                //    //    await item.CopyToAsync(stream);
                //    //    stream.Close();
                //    //}

                //var image = new Images
                //{
                //    FilmId = lastFilmId,
                //    Name = item.FileName, // pending
                //    Path = path // pending

                //};
                //_filmsRepository.CreateImage(image);
                //_filmsRepository.SaveChange();
                //}

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
                if (newFilm.Name == film.Name && newFilm.CategoryId == film.CategoryId && newFilm.Director == film.Director && newFilm.Country == film.Country)
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
                
                var filmList = _filmsRepository.getAllFilms(null, null, null, null).Result.ListItem;

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

                _filmsRepository.DeleteFilm(film);
                _filmsRepository.SaveChange();
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
        public async Task<ResponseDTO> GetAllFilms(string? name, string? director, string? country, string? category, int page = 0, int limit = 10)
        {
            try
            {
                var result = await _filmsRepository.getAllFilms(null, null, null, null);
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
