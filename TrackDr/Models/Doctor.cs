    using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TrackDr.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            ChildDoctor = new HashSet<ChildDoctor>();
            ParentDoctor = new HashSet<ParentDoctor>();
        }

        public string DoctorId { get; set; }
        public string FirstName { get; set; }

        public virtual ICollection<ChildDoctor> ChildDoctor { get; set; }
        public virtual ICollection<ParentDoctor> ParentDoctor { get; set; }
    }


    public class Rootobject
    {
        public Meta meta { get; set; }
        public Datum[] data { get; set; }

    }

    public class SingleDoctor
    {
        public Meta meta { get; set; }
        public Datum data { get; set; }
    }

    public class Meta
    {
        //    public string data_type { get; set; }
        //    public string item_type { get; set; }
        //    public int total { get; set; }
        public int count { get; set; }
        public int skip { get; set; }
        public int limit { get; set; }
    }

    public class Datum
    {
        public Practice[] practices { get; set; }
        public Education[] educations { get; set; }
        public Profile profile { get; set; }
        //public Rating[] ratings { get; set; }
        public Insurance[] insurances { get; set; }
        public Specialty[] specialties { get; set; }
        //public License[] licenses { get; set; }
        //****Dr. UID
        public string uid { get; set; }
        public string npi { get; set; }
    }

    public class Profile
    {

        public string first_name { get; set; }
        public string last_name { get; set; }
        public string slug { get; set; }
        public string title { get; set; }
        public string image_url { get; set; }
        public string gender { get; set; }
        public Language[] languages { get; set; }
        public string bio { get; set; }
        public string middle_name { get; set; }
    }

    public class Language
    {
        public string name { get; set; }
        public string code { get; set; }
    }

    public class Practice
    {
        //public string location_slug { get; set; }
        //public float lat { get; set; }
        //public float lon { get; set; }
        public string uid { get; set; }
        public string name { get; set; }
        //public bool accepts_new_patients { get; set; }
        public string[] insurance_uids { get; set; }
        public Visit_Address visit_address { get; set; }
        //public object[] office_hours { get; set; }
        //public Phone[] phones { get; set; }
        public Language1[] languages { get; set; }
        //public string website { get; set; }
        //public Medium[] media { get; set; }
    }

    public class Visit_Address
    {
        public string city { get; set; }
        //public float lat { get; set; }
        //public float lon { get; set; }
        public string state { get; set; }
        //public string state_long { get; set; }
        public string street { get; set; }
        public string zip { get; set; }
        //public string street2 { get; set; }
    }

    //public class Phone
    //{
    //    public string number { get; set; }
    //    public string type { get; set; }
    //}

    public class Language1
    {
        public string name { get; set; }
        public string code { get; set; }
    }

    public class Medium
    {
    //    public string uid { get; set; }
    //    public string status { get; set; }
    //    public string url { get; set; }
    //    public string[] tags { get; set; }
    //    public Versions versions { get; set; }
    }

    public class Versions
    {
    //    public string small { get; set; }
    //    public string medium { get; set; }
    //    public string large { get; set; }
    //    public string hero { get; set; }
    }

    public class Education
    {
        //public string school { get; set; }
        //public string graduation_year { get; set; }
        public string degree { get; set; }
    }

    public class Rating
    {
    //    public bool active { get; set; }
    //    public string provider { get; set; }
    //    public string provider_uid { get; set; }
    //    public float rating { get; set; }
    //    public int review_count { get; set; }
    //    public string image_url_small { get; set; }
    //    public string image_url_small_2x { get; set; }
    //    public string image_url_large { get; set; }
    //    public string image_url_large_2x { get; set; }
    //    public string provider_url { get; set; }
    }

    public partial class Insurance
    {
        public Insurance_Plan insurance_plan { get; set; }
        public Insurance_Provider insurance_provider { get; set; }
    }

    public class Insurance_Plan
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string[] category { get; set; }
    }

    public class Insurance_Provider
    {
        public string uid { get; set; }
        public string name { get; set; }
    }

    public class Specialty
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        //public string actor { get; set; }
       // public string actors { get; set; }
    }

    public class License
    {
    //    public string state { get; set; }
    //    public string number { get; set; }
    //    public string end_date { get; set; }
    }

}
