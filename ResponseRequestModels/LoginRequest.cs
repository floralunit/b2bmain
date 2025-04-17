using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B2BWebService.ResponseRequestModels
{
    public class LoginRequest
    {
        /// <summary>
        /// Логин пользователя в системе MT.
        /// </summary>
        public string? Login { get; set; }

        /// <summary>
        /// SHA1-hash пароля в системе MT.
        /// </summary>
        public string? PwdHash { get; set; }

        /// <summary>
        /// Уникальный идентификатор устройства.
        /// </summary>
        public string? DeviceUniqID { get; set; }
    }

}
