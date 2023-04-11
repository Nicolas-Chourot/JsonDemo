using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JsonDemo.Models
{
public class Registration
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    [JsonIgnore] public Course Course { get { return DB.Courses.Get(CourseId); } }
    [JsonIgnore] public Student Student { get { return DB.Students.Get(StudentId); } }
}
}