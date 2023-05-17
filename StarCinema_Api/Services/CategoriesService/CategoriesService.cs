using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.CategoriesRepository;

namespace StarCinema_Api.Services.CategoriesService
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _CategoriesRepository;
        private readonly IMapper _mapper;
        public CategoriesService(ICategoriesRepository CategoriesRepository,
            IMapper mapper)
        {
            _CategoriesRepository = CategoriesRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> CreateCategories([FromForm] CategoriesDTO CategoriesDTO)
        {
            try
            {

                var Categories = _mapper.Map<CategoriesDTO, Categories>(CategoriesDTO);
                var CategoriesList = _CategoriesRepository.getAllCategories(null).Result.ListItem;
                var IsExist = IsCategoriesExist(Categories, CategoriesList);
                if (IsExist)
                {
                    return new ResponseDTO
                    {
                        code = 400,
                        message = $"The Category is existed!"
                    };
                }

                _CategoriesRepository.CreateCategories(Categories);
                _CategoriesRepository.SaveChange();

                

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

        public bool IsCategoriesExist(Categories newCategories, List<Categories> CategoriesList)
        {
            if (CategoriesList.Count == 0) return false;
            foreach (var Categories in CategoriesList)
            {
                if (newCategories.Name == Categories.Name)
                {
                    return true;
                }
            }
            return false;
        }

        // METHOD UPDATE
        public async Task<ResponseDTO> UpdateCategories(int id, CategoriesDTO CategoriesDTO)
        {
            try
            {

                var CategoriesCurrent = await _CategoriesRepository.getCategoriesById(id);
                if (CategoriesCurrent == null) return new ResponseDTO
                {
                    code = 400,
                    message = $"Does not exist Category with id {id}"
                };


                var CategoriesNew = _mapper.Map<CategoriesDTO, Categories>(CategoriesDTO);
                CategoriesNew.Id = id;

                var CategoriesList = _CategoriesRepository.getAllCategories(null).Result.ListItem;

                CategoriesList = CategoriesList.Where(s => s.Id != CategoriesCurrent.Id).ToList();
                var IsExist = IsCategoriesExist(CategoriesNew, CategoriesList);
                if (IsExist)
                {
                    return new ResponseDTO
                    {
                        code = 400,
                        message = $"The modifying Category is duplicate"
                    };
                }

                _CategoriesRepository.UpdateCategories(CategoriesNew);

                _CategoriesRepository.SaveChange();
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
        public async Task<ResponseDTO> DeleteCategoriesById(int id)
        {
            try
            {
                var Categories = await _CategoriesRepository.getCategoriesById(id);
                if (Categories == null) return new ResponseDTO
                {
                    code = 404,
                    message = $"Does not exist Category with id {id}",
                };

                _CategoriesRepository.DeleteCategories(Categories);
                _CategoriesRepository.SaveChange();
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

        //METHOD GET ALL Categories
        public async Task<ResponseDTO> GetAllCategories(string? name, int page = 0, int limit = 10)
        {
            try
            {
                var result = await _CategoriesRepository.getAllCategories(null);
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

        //METHOD GET BY ID
        public async Task<ResponseDTO> GetCategoriesById(int id)
        {
            try
            {
                var result = await _CategoriesRepository.getCategoriesById(id);
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
