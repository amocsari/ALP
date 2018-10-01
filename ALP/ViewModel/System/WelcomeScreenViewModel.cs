using System.Text;

namespace ALP.ViewModel
{
    public class WelcomeScreenViewModel
    {
        public string ApplicationVersion
        {
            get
            {
                //TODO: build dátum
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
