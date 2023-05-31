////////////////////////////////////////////////////////////////////////////////////////////////////////
///FileName: FilmsService.cs
///FileType: Visual C# Source file
///Author : VyVNK1
///Created On : 20/05/2023
///Last Modified On : 24/05/2023
///Copy Rights : FA Academy
///Description : Film Service
////////////////////////////////////////////////////////////////////////////////////////////////////////

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.FilmsRepository;

namespace StarCinema_Api.Services.FilmsService
{

    /// <summary>
    /// VYVNK1 Create class FilmService implement IFilmsService
    /// </summary>
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

        /// <summary>
        /// VyVNK1 CREATE FILM
        /// </summary>
        /// <param name="filmDTO"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> CreateFilm([FromForm] FilmDTO filmDTO)
        {
            try
            {
                var film = _mapper.Map<Films>(filmDTO);
                /// FIX YOUTUBE VIDEO LINK
                int delimiters = film.VideoLink.Count(x => x == '/');

                if (delimiters == 3)
                {
                    film.VideoLink = film.VideoLink.Split("/")[3];
                }
                else
                {
                    film.VideoLink = film.VideoLink;
                }

                var filmList = _filmsRepository.GetAllFilms(null).Result.ListItem;
                var isExist = IsFilmExist(film, filmList);
                if (isExist)
                {
                    return new ResponseDTO
                    {
                        code = 400,
                        message = $"The Film is existed! Please re enter!"
                    };
                }

                _filmsRepository.CreateFilm(film);
                _filmsRepository.SaveChange();

                /// Upload images
                var filmID = await _filmsRepository.GetLastIDFilm();

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

        /// <summary>
        /// VyVNK1 METHOD CHECK IF FILM ALREADY EXIST
        /// </summary>
        /// <param name="newFilm"></param>
        /// <param name="filmList"></param>
        /// <returns></returns>
        public bool IsFilmExist(Films newFilm, List<Films> filmList)
        {

            if (filmList.Count == 0) return false;
            foreach (var film in filmList)
            {
                if (newFilm.Name.Equals(film.Name, StringComparison.OrdinalIgnoreCase)
                    && newFilm.CategoryId == film.Category.Id
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

        /// <summary>
        /// VyVNK1 METHOD UPDATE
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filmDTO"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> UpdateFilm(int id, FilmDTO filmDTO)
        {
            try
            {

                var filmCurrent = await _filmsRepository.GetFilmById(id);
                if (filmCurrent == null) return new ResponseDTO
                {
                    code = 400,
                    message = $"Does not exist film with id {id}"
                };
                var filmNew = _mapper.Map<FilmDTO, Films>(filmDTO);
                filmNew.Id = id;

                /// FIX YOUTUBE VIDEO LINK
                int delimiters = filmNew.VideoLink.Count(x => x == '/');
                if (delimiters == 3)
                {
                    filmNew.VideoLink = filmNew.VideoLink.Split("/")[3];
                }
                else
                {
                    filmNew.VideoLink = filmNew.VideoLink;
                }

                var filmList = _filmsRepository.GetAllFilms(null).Result.ListItem;
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

                foreach (var item in filmDTO.Image)
                {

                    if (item.Name != null && item.Name.Length > 0)
                    {
                        _filmsRepository.UpdateImage(id, item.Path);
                        _filmsRepository.SaveChange();
                    }
                }
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

        /// <summary>
        /// VyVNK1 METHOD DELETE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> DeleteFilmById(int id)
        {
            try
            {
                var film = await _filmsRepository.GetFilmById(id);
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

        /// <summary>
        /// VyVNK1 METHOD GET ALL FILM
        /// </summary>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> GetAllFilms(string? search, int page = 0, int limit = 10)
        {
            try
            {
                var result = await _filmsRepository.GetAllFilms(search);
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

        /// <summary>
        /// VyVNK1 METHOD GET ALL NOW SHOWING FILM
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseDTO> getNowShowingFilms()
        {
            try
            {
                var result = await _filmsRepository.GetNowShowingFilms();
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

        /// <summary>
        /// VyVNK1 METHOD GET ALL UPCOMING FILM
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseDTO> getUpComingFilms()
        {
            try
            {
                var result = await _filmsRepository.GetUpComingFilms();
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
        /// <summary>
        /// VyVNK1 METHOD GET FILM BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> GetFilmById(int id)
        {
            try
            {
                var result = await _filmsRepository.GetFilmById(id);
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
