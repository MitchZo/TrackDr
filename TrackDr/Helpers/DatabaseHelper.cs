using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TrackDr.Models;

namespace TrackDr.Helpers
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private readonly TrackDrDbContext _context;
        private readonly IBDAPIHelper _bDAPIHelper;
        public DatabaseHelper(TrackDrDbContext context, IBDAPIHelper bDAPIHelper)
        {
            _context = context;
            _bDAPIHelper = bDAPIHelper;
        }
        public bool CanAddDoctor(Doctor doctor)
        {
            Doctor foundDoctor = FindDoctorById(doctor.DoctorId);
            if (foundDoctor == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CanAddParentDoctorRelationship(string parent, string doctor)
        {
            bool isValid = false;
            List<ParentDoctor> relationshipList = _context.ParentDoctor.ToList();
            List<ParentDoctor> currentParentDoctors = new List<ParentDoctor>();
            foreach (ParentDoctor relationship in relationshipList)
            {
                if (relationship.ParentId == parent)
                {
                    currentParentDoctors.Add(relationship);
                }
            }
            if(currentParentDoctors.Count == 0)
            {
                isValid = true;
            }
            foreach (ParentDoctor relationship in currentParentDoctors)
            {
                if (relationship.DoctorId == doctor)
                {
                    isValid = false;
                    break;
                }
                else
                {
                    isValid = true;
                }
            }
            return isValid;
        }
        public AspNetUsers GetCurrentUser(string userName)
        {
                return _context.AspNetUsers.Where(u => u.Email == userName).First();
        }
        public Parent GetCurrentParent(AspNetUsers currentUser)
        {
            return _context.Parent.Find(currentUser.Id);
        }
        public void AddNewDoctor(Doctor newDoctor)
        {
            _context.Doctor.Add(newDoctor);
            _context.SaveChanges();
        }
        public void AddNewParentDoctorRelationship(ParentDoctor newParentDoctor)
        {
            _context.ParentDoctor.Add(newParentDoctor);
            _context.SaveChanges();
        }
        public void AddNewParent(Parent newUser)
        {
            _context.Parent.Add(newUser);
            _context.SaveChanges();
        }
        public void AddNewChild(Child newChild)
        {
            _context.Child.Add(newChild);
            _context.SaveChanges();
        }
        public List<SingleDoctor> GetListOfCurrentUsersDoctors(string userName)
        {
            AspNetUsers thisUser = GetCurrentUser(userName);
            //Goes to Db, pull list of doctors associated with current user
            List<ParentDoctor> savedList = _context.ParentDoctor.Where(u => u.ParentId == thisUser.Id).ToList();
            //Establishes new list to store doctor UID's
            List<string> doctorIdList = new List<string>();
            //List of list of "Doctors", Rootobject >>> Datum[]
            List<SingleDoctor> doctorList = new List<SingleDoctor>();

            //Taking all doctorIDs from parentdoctor relationship, adding them to DoctorId list
            foreach (ParentDoctor relationship in savedList)
            {
                doctorIdList.Add(relationship.DoctorId);
            }

            //Taking every doctorid, going to api, bringing back that specific doctor and placing it in list
            foreach (string doctor in doctorIdList)
            {
                    doctorList.Add(_bDAPIHelper.GetDoctor(doctor).Result);
            }
            return doctorList;
        }
        public Parent FindParentById(string userId)
        {
            return _context.Parent.Find(userId);
        }
        public Doctor FindDoctorById(string doctorId)
        {
           return _context.Doctor.Find(doctorId);
        }
        public void UpdateParent(Parent updatedUser)
        {
            _context.Entry(updatedUser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.Update(updatedUser);
            _context.SaveChanges();
        }

        public void DeleteDoctor(ParentDoctor parentDoctor)
        {
            _context.ParentDoctor.Remove(parentDoctor);
            _context.SaveChanges();
        }

        public ParentDoctor FindParentDoctorRelationship(string doctorId, AspNetUsers currentUser)
        {
            List<ParentDoctor> parentDoctorRelationship = _context.ParentDoctor.ToList();
            foreach (var relationship in parentDoctorRelationship)
            {
                if (relationship.DoctorId == doctorId)
                {
                    if(relationship.ParentId == currentUser.Id)
                    {
                    return relationship;
                    }
                }
            }
            return _context.ParentDoctor.Find(doctorId);
        }


    }
}