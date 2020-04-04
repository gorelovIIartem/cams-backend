namespace WebApi
{
    public class WebApiRoutes
    {
        public class User
        {
            public const string Login = "api/User/Login";
            public const string Register = "api/User/Register";
        }

        public class Group
        {
            public const string Create = "api/Group";
        }

        public class DeviceLog
        {
            public const string Create = "api/DeviceLog";
            public const string SortedLogs = "api/DeviceLog";
            public const string Report = "api/DeviceLog/{deviceId:int}/create-report";
        }

        public class Device
        {
            public const string Create = "api/Device";
        }
    }
}
