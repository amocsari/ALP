using System.Text;
using System.Threading.Tasks;

namespace ALP.ViewModel
{
    /// <summary>
    /// The ViewModel of the WelcomePage
    /// </summary>
    public class WelcomeScreenViewModel
    {
        /// <summary>
        /// Returns the version of the application
        /// </summary>
        public string ApplicationVersion
        {
            get
            {
                var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                var stringBuilder = new StringBuilder();
                stringBuilder.Append(version.Major);
                stringBuilder.Append(".")
                    .Append(version.Minor)
                    .Append(".")
                    .Append(version.Build)
                    .Append(".")
                    .Append(version.Revision);
                return stringBuilder.ToString();
            }
        }
    }
}
