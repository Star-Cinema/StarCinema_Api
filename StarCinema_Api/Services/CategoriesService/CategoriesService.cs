////////////////////////////////////////////////////////////////////////////////////////////////////////
///FileName: CategoriesService.cs
///FileType: Visual C# Source file
///Author : VyVNK1
///Created On : 20/05/2023
///Last Modified On : 24/05/2023
///Copy Rights : FA Academy
///Description : Category Service
////////////////////////////////////////////////////////////////////////////////////////////////////////

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.CategoriesRepository;

namespace StarCinema_Api.Services.CategoriesService
{
    /// <summary>
    /// VYVNK1 Create class CategoriesService implement ICategoriesService
    /// </summary>
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;
        public CategoriesService(ICategoriesRepository categoriesRepository,
            IMapper mapper)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// VyVNK1 METHOD CREATE CATEGORY
        /// </summary>
        /// <param name="categoriesDTO"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> CreateCategories([FromForm] CategoriesDTO categoriesDTO)
        {
            try
            {

                var categories = _mapper.Map<CategoriesDTO, Categories>(categoriesDTO);
                var categoriesList = _categoriesRepository.GetAllCategories(null).Result.ListItem;
                var isExist = IsCategoriesExist(categories, categoriesList);
                if (isExist)
                {
                    return new ResponseDTO
                    {
                        code = 400,
                        message = $"{categories.Name} is existed in system! Please re enter!"
                    };
                }

                _categoriesRepository.CreateCategories(categories);
                _categoriesRepository.SaveChange();



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
        /// VyVNK1 METHOD CHECK CATEGORY IS EXISTED
        /// </summary>
        /// <param name="newCategories"></param>
        /// <param name="CategoriesList"></param>
        /// <returns></returns>
        public bool IsCategoriesExist(Categories newCategories, List<Categories> categoriesList)
        {
            if (categoriesList.Count == 0) return false;
            foreach (var categories in categoriesList)
            {
                if (newCategories.Name.Equals(categories.Name, StringComparison.OrdinalIgnoreCase) && categories.IsTrash == false)
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
        /// <param name="CategoriesDTO"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> UpdateCategories(int id, CategoriesDTO categoriesDTO)
        {
            try
            {

                var categoriesCurrent = await _categoriesRepository.GetCategoriesById(id);
                if (categoriesCurrent == null) return new ResponseDTO
                {
                    code = 400,
                    message = $"Does not exist Category with id {id}"
                };


                var categoriesNew = _mapper.Map<CategoriesDTO, Categories>(categoriesDTO);
                categoriesNew.Id = id;

                var categoriesList = _categoriesRepository.GetAllCategories(null).Result.ListItem;

                categoriesList = categoriesList.Where(s => s.Id != categoriesCurrent.Id).ToList();
                var isExist = IsCategoriesExist(categoriesNew, categoriesList);
                if (isExist)
                {
                    return new ResponseDTO
                    {
                        code = 400,
                        message = $"The modifying Category is duplicate"
                    };
                }

                _categoriesRepository.UpdateCategories(categoriesNew);

                _categoriesRepository.SaveChange();
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
        public async Task<ResponseDTO> DeleteCategoriesById(int id)
        {
            try
            {
                var categories = await _categoriesRepository.GetCategoriesById(id);
                if (categories == null) return new ResponseDTO
                {
                    code = 404,
                    message = $"Does not exist Category with id {id}",
                };

                else
                {

                    var countFilm = categories.Films.Count;
                    if (countFilm > 0)

                    {
                        categories.IsTrash = true;
                        _categoriesRepository.UpdateCategories(categories);
                        _categoriesRepository.SaveChange();

                    }
                    else

                    {
                        _categoriesRepository.DeleteCategories(categories);
                        _categoriesRepository.SaveChange();

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
        /// VyVNK1 METHOD GET ALL Categories
        /// </summary>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> GetAllCategories(string? name, int page = 0, int limit = 10)
        {
            try
            {
                var result = await _categoriesRepository.GetAllCategories(name);
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
        /// VyVNK1 METHOD GET BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> GetCategoriesById(int id)
        {
            try
            {
                var result = await _categoriesRepository.GetCategoriesById(id);
                if (result == null) return new ResponseDTO
                {
                    code = 400,
                    message = $"Does not exist Category with id {id}"
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
