namespace BuildingBlocks.Api.Logging
{
    public class SystemVersion
    {
        private readonly string _mainSystemVersion;
        /// <summary>
        /// Build number will be replaced during a release. Please check release definition for more information.
        /// BuildNumber => Build.BuildId
        /// @see https://dev.azure.com/bcc-its/ActivityWeb/_apps/hub/ms.vss-distributed-task.hub-library?itemType=VariableGroups&view=VariableGroupView&variableGroupId=89&path=AW%20Common%20Settings
        /// </summary>
        private readonly string _buildNumber;

        private SystemVersion(string mainSystemVersion, string buildNumber)
        {
            _mainSystemVersion = mainSystemVersion;
            _buildNumber = buildNumber;
        }

        public override string ToString()
        {
            return $"{_mainSystemVersion}.{_buildNumber}";
        }

        public static string Get(string mainSystemVersion, string buildNumber)
        {
            var sv = new SystemVersion(mainSystemVersion, buildNumber);
            return sv.ToString();
        }
    }
}