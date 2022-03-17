using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCareApp.Model
{
    public class Enums
    {
        public enum Gender
        {
            Male = 1,
            Female = 2
        }

        public enum Role
        {
            Nurse = 1,
            AssistantNurse = 2,
            CareAssistant = 3,
            Manager = 4
        }

        public enum DeviceType
        {
            Desktop = 1,
            Mobile = 2
        }
    }
}