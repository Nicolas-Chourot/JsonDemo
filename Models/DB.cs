using JsonDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Hosting;
using System.Web.UI.WebControls;

namespace JsonDemo.Models
{
public class DB
{
    #region singleton setup
    private static readonly DB instance = new DB();
    public static DB Instance { get { return instance; } }
    #endregion
    #region Repositories
    static public StudentsRepository Students { get; set; }
    static public CoursesRepository Courses { get; set; }
    static public Repository<Registration> Registrations { get; set; }
    #endregion
    #region initialization
    public DB()
    {
        #region Repositories instanciations
        Students = new StudentsRepository();
        Courses = new CoursesRepository();
        Registrations = new Repository<Registration>();
        #endregion
        InitRepositories(this);
    }
    private static void InitRepositories(DB db)
    {
        string serverPath = HostingEnvironment.MapPath(@"~/App_Data/");
        PropertyInfo[] myPropertyInfo = db.GetType().GetProperties();
        foreach (PropertyInfo propertyInfo in myPropertyInfo)
        {
            if (propertyInfo.PropertyType.Name.Contains("Repository"))
            {
                MethodInfo method = propertyInfo.PropertyType.GetMethod("Init");
                method.Invoke(propertyInfo.GetValue(db), new object[] { serverPath + propertyInfo.Name + ".json" });
            }
        }
    }
    #endregion
}
}