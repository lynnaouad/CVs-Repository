using CV_Project2022.Data;
using CV_Project2022.FileUploadService;
using CV_Project2022.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CV_Project2022
{
    public class DBService
    {
        private readonly DataContext _context = null;
        private readonly IFileUploadService _fileUploadService;
        private readonly OtherService _otherService;

        public DBService(DataContext context, IFileUploadService fileUploadService, OtherService otherService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
            _otherService = otherService;
        }


        // CV TABLE CRUD

        public async Task<List<CvModel>> GetAllCVs()
        {
            List<CvModel> cvs_list = new List<CvModel>();

            //Get all CVs from database
            var all_cvs = await _context.CV.ToListAsync();

            if (all_cvs.Any() == true)
            {
                foreach (var cv in all_cvs)
                {

                    // transform path to IFormFile
                    IFormFile photo = _fileUploadService.TransformToIFormFile(cv.Photo);


                    cvs_list.Add(new CvModel()
                    {
                        Id = cv.Id,
                        FirstName = cv.FirstName,
                        LastName = cv.LastName,
                        Date = cv.Date,
                        Nationality = cv.Nationality,
                        Gender = cv.Gender,
                        Skills = await GetSkillsFor(cv.Id),
                        Photo = photo,
                        Email = cv.Email,
                        Grade = cv.Grade
                    });
                }
            }

            return cvs_list;
        }


        public async Task<CvModel> GetCvById(int id)
        {
            var cv = await _context.CV.FindAsync(id);

            if (cv != null)
            {
                // transform path to IFormFile
                IFormFile photo = _fileUploadService.TransformToIFormFile(cv.Photo);


                CvModel CvDetails = new CvModel()
                {
                    Id = cv.Id,
                    FirstName = cv.FirstName,
                    LastName = cv.LastName,
                    Date = cv.Date,
                    Nationality = cv.Nationality,
                    Gender = cv.Gender,
                    Skills = await GetSkillsFor(cv.Id),
                    Photo = photo,
                    Email = cv.Email,
                    Grade = cv.Grade
                };

                return CvDetails;
            }
            else
            {
                throw new Exception("Unable to find the CV");
            }
        }


        public async Task<int> AddNewCv(CvModel cv)
        {
            //upload file
            string FilePath = await _fileUploadService.UploadFileAsync(cv.Photo);

            // Calculate Grade
            int grade = _otherService.CalculateGrade(cv);


            var new_cv = new CV
            {
                FirstName = cv.FirstName,
                LastName = cv.LastName,
                Date = cv.Date,
                Nationality = cv.Nationality,
                Gender = cv.Gender,
                Photo = FilePath,
                Email = cv.Email,
                Grade = grade
            };

            // Add CV into table
            _context.CV.Add(new_cv);
            await _context.SaveChangesAsync();

            // Insert selected skills into CV_SKILL table
            CvSkillModel cv_skill = new CvSkillModel();
            cv_skill.CvId = new_cv.Id;

            // Get selected Skills id
            var SkillsSelected = cv.Skills.Where(x => x.Selected).Select(y => y.Value);

            if (SkillsSelected.Any())
            {
                foreach (var SkillId in SkillsSelected)
                {
                    cv_skill.SkillId = Int32.Parse(SkillId);
                    await AddNewCvSkill(cv_skill);
                }
            }

            return new_cv.Id;
        }


        public async Task UpdateCv(CvModel newCv)
        {
            // get the cv having this id from database
            var OldCv = await _context.CV.FindAsync(newCv.Id);

            if (OldCv == null)
            { throw new Exception("Unable to find the Cv"); }

            //upload new file
            string FilePath = await _fileUploadService.UploadFileAsync(newCv.Photo);

            // Calculate new Grade
            int grade = _otherService.CalculateGrade(newCv);


            OldCv.FirstName = newCv.FirstName;
            OldCv.LastName = newCv.LastName;
            OldCv.Date = newCv.Date;
            OldCv.Nationality = newCv.Nationality;
            OldCv.Gender = newCv.Gender;
            OldCv.Photo = FilePath;
            OldCv.Email = newCv.Email;
            OldCv.Grade = grade;

            await _context.SaveChangesAsync();

            // Delete all old skills
            await DeleteAllCvSkills(newCv.Id);

            // Insert selected skills into CV_SKILL table
            CvSkillModel cv_skill = new CvSkillModel();
            cv_skill.CvId = newCv.Id;

            // Get selected Skills id
            var SkillsSelected = newCv.Skills.Where(x => x.Selected).Select(y => y.Value);

            if (SkillsSelected.Any())
            {
                foreach (var SkillId in SkillsSelected)
                {
                    cv_skill.SkillId = Int32.Parse(SkillId);
                    await AddNewCvSkill(cv_skill);
                }
            }


        }


        public async Task DeleteCv(int id)
        {
            var cv = await _context.CV.FindAsync(id);

            if (cv == null)
            { throw new Exception("Unable to find the customer"); }

            _context.CV.Remove(cv);

            await _context.SaveChangesAsync();
        }


        // SKILLS TABLE CRUD


        public async Task<List<SkillModel>> GetAllSkills()
        {
            List<SkillModel> skill_list = new List<SkillModel>();

            //Get all Skills from database
            var all_skills = await _context.SKILL.ToListAsync();

            if (all_skills.Any() == true)
            {
                foreach (var s in all_skills)
                {

                    skill_list.Add(new SkillModel()
                    {
                        Id = s.Id,
                        Name = s.Name
                    });
                }
            }

            return skill_list;
        }


        public async Task<SkillModel> GetSkillById(int id)
        {

            var skill = await _context.SKILL.FindAsync(id);

            if (skill != null)
            {
                SkillModel SkillDetails = new SkillModel()
                {
                    Id = skill.Id,
                    Name = skill.Name
                };

                return SkillDetails;
            }
            else
            {
                throw new Exception("Unable to find the skill");
            }
        }


        public async Task<int> AddNewSkill(SkillModel s)
        {
            var new_skill = new SKILL
            {
                Name = s.Name
            };

            _context.SKILL.Add(new_skill);
            await _context.SaveChangesAsync();

            return new_skill.Id;
        }


        public async Task UpdateSkill(SkillModel newSkill)
        {
            var OldSkill = await _context.SKILL.FindAsync(newSkill.Id);

            if (OldSkill == null)
            { throw new Exception("Unable to find the skill"); }

            OldSkill.Name = newSkill.Name;

            await _context.SaveChangesAsync();
        }


        public async Task DeleteSkill(int id)
        {
            var skill = await _context.SKILL.FindAsync(id);

            if (skill == null)
            { throw new Exception("Unable to find the skill"); }

            _context.SKILL.Remove(skill);

            await _context.SaveChangesAsync();
        }



        // CV_SKILL TABLE CRUD

        public async Task AddNewCvSkill(CvSkillModel model)
        {
            var new_CvSkill = new CV_SKILL
            {
                CvId = model.CvId,
                SkillId = model.SkillId
            };

            _context.CV_SKILL.Add(new_CvSkill);
            await _context.SaveChangesAsync();
        }


        public async Task<List<SelectListItem>> GetSkillsFor(int cv_id)
        {
            List<SelectListItem> skills_list = new List<SelectListItem>();

            // Get skills for this cvid
            var cv_skill = _context.CV_SKILL.Where(x => x.CvId == cv_id).ToList();


            if (cv_skill != null)
            {
                foreach (var s in cv_skill)
                {
                    // get skill name having this id
                    SkillModel skillDetails = await GetSkillById(s.SkillId);

                    skills_list.Add(new SelectListItem { Value = skillDetails.Id.ToString(), Text = skillDetails.Name });
                }

                return skills_list;
            }

            return null;

        }


        public async Task DeleteAllCvSkills(int cv_id)
        {
            // Get skills for this cvid
            var cv_skill = _context.CV_SKILL.Where(x => x.CvId == cv_id).ToList();

            if (cv_skill != null)
            {
                foreach (var s in cv_skill)
                {
                    _context.CV_SKILL.Remove(s);
                    await _context.SaveChangesAsync();

                }
            }
        }




    }
}