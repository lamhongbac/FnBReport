namespace FnBReport.BLL.Constants
{
    public static class DomainErrorCodes
    {
        public static class Store
        {
            public const string NumberEmpty = "STORE_NUMBER_EMPTY";
            public const string NameEmpty = "STORE_NAME_EMPTY";
            public const string NumberExists = "STORE_NUMBER_EXISTS";
            public const string NotFound = "STORE_NOT_FOUND";
        }

        public static class StoreGroup
        {
            public const string NameEmpty = "STOREGROUP_NAME_EMPTY";
            public const string NotFound = "STOREGROUP_NOT_FOUND";
        }
    }
}
