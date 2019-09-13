using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackDr.Models;

namespace TrackDr.Helpers
{
    public interface IDatabaseHelper
    {
        bool CanAddDoctor(Doctor doctor);
        bool CanAddParentDoctorRelationship(string parent, string doctor);
        AspNetUsers GetCurrentUser(string userName);
        void AddNewDoctor(Doctor newDoctor);
        void AddNewParentDoctorRelationship(ParentDoctor newParentDoctor);
        List<SingleDoctor> GetListOfCurrentUsersDoctors(string userName);
        void AddNewParent(Parent newUser);
        Parent FindParentById(string userId);
        void UpdateParent(Parent updatedUser);
        Parent GetCurrentParent(AspNetUsers currentUser);
        void DeleteDoctor(ParentDoctor parentDoctor);
        ParentDoctor FindParentDoctorRelationship(string doctorId, AspNetUsers currentUser);
    }
}
