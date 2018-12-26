using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCore.Api.Dtos.Language;
using MyCore.Entities;
using MyCore.Repositories;

namespace MyCore.Api.Controllers
{
    [Route("api/language")]
    [ApiController]
    public class LanguageController : BaseController
    {
        public LanguageController(IUnitOfWork uow) : base(uow) { }

        #region 查（全部）
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var list =_uow.Repository<Language>().GetAll();
            return Ok(list);
        }
        #endregion

        #region 查（分页）
        [HttpGet("list")]
        public IActionResult GetList(int page=1,int limit=10,string keyword = "")
        {
            var q = _uow.Repository<Language>().GetAll(a=>!a.IsDeleted);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                q = q.Where(a=>a.Key.Contains(keyword) || a.Value.Contains(keyword));
            }
            q = q.OrderBy(a => a.Key).Skip((page - 1) * limit).Take(limit);
            var list = q.Select(a=>Mapper.Map<LanguageDto>(a));
            return Ok(list);
        }
        #endregion

        #region 查
        [HttpGet]
        public IActionResult Get(string id)
        {
            var model = _uow.Repository<Language>().Find(id);
            if (model == null)
            {
                return BadRequest("不存在");
            }
            var dto = Mapper.Map<LanguageDto>(model);
            return Ok(dto);
        }
        #endregion

        #region 增
        [HttpPost]
        public IActionResult Create(LanguageCreationDto dto)
        {
            var isExisted = _uow.Repository<Language>().Any(a => a.Key == dto.Key);
            if (isExisted)
            {
                return BadRequest("已存在");
            }

            var model = Mapper.Map<Language>(dto);
            model.CreatedTime = DateTime.Now;
            _uow.Repository<Language>().Add(model);
            _uow.SaveChanges();
            return Ok();
        }
        #endregion

        #region 改
        [HttpPut]
        public IActionResult Update(string id,LanguageModificationDto dto)
        {
            var model = _uow.Repository<Language>().Find(id);
            if (model==null)
            {
                return BadRequest("不存在");
            }

            model.Value = dto.Value;
            model.UpdatedTime=DateTime.Now;
            _uow.Repository<Language>().Update(model);
            _uow.SaveChanges();
            return Ok();
        }
        #endregion

        #region 删
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var model = _uow.Repository<Language>().Find(id);
            if (model==null)
            {
                return BadRequest("不存在");
            }

            _uow.Repository<Language>().Delete(id);
            _uow.SaveChanges();
            return Ok();
        }
        #endregion
    }
}