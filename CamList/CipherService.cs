using Microsoft.AspNetCore.DataProtection;

namespace CamList
{
    public static class CiphererService
    {
        static IDataProtectionProvider _provider = DataProtectionProvider.Create("CamList");
        static IDataProtector _protector = _provider.CreateProtector("FileNameEncrypter");

        public static string Encrypt (string stringToEncrypt)
        {
            return _protector.Protect(stringToEncrypt);
        }

        public static string Decrypt (string secretToDecrypt)
        {
            return _protector.Unprotect(secretToDecrypt);
        }
}
}
