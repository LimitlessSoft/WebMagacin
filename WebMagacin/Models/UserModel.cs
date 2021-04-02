using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebMagacin.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "Magacioner";

        private static string _rootDataPath { get; set; }

        static UserModel()
        {
            _rootDataPath = Path.Combine(Program.WebRootPath, "data", "user");
            Directory.CreateDirectory(_rootDataPath);
        }

        public UserModel()
        {

        }

        public void Update()
        {

        }

        /// <summary>
        /// Inserts new user into system
        /// </summary>
        /// <param name="username"></param>
        /// <param name="displayName"></param>
        /// <param name="password"></param>
        public static UserModel Insert(string username, string displayName, string password)
        {
            try
            {
                UserModel newUser = new UserModel();
                newUser.ID = MaxID() + 1;
                newUser.Username = username;
                newUser.DisplayName = displayName;
                newUser.Password = LimitlessSoft.Hash.Generate(password);

                File.WriteAllText(Path.Combine(_rootDataPath, newUser.ID + ".txt"), JsonConvert.SerializeObject(newUser));

                return newUser;
            }
            catch(Exception)
            {
                return null;
            }
        }
        public static UserModel Get(string username)
        {
            // To improve create buffered list
            // We want to get all new data so we cannot do it from buffer since some data may change but ID is always same
            // So in order to not get whole list (deserialize each file) we will get ID from buffer and then deserialize only that file
            // This is todo
            return List().Where(x => x.Username == username).FirstOrDefault();
        }
        public static List<UserModel> List()
        {
            List<UserModel> list = new List<UserModel>();

            foreach(string f in Directory.GetFiles(_rootDataPath))
                list.Add(JsonConvert.DeserializeObject<UserModel>(File.ReadAllText(f)));

            return list;
        }
        /// <summary>
        /// Returns maximum user ID
        /// </summary>
        /// <returns></returns>
        public static int MaxID()
        {
            int maxID = 0;
            foreach(string f in Directory.GetFiles(_rootDataPath))
            {
                int thisID = Convert.ToInt32(Path.GetFileName(f).Split('.')[0]);
                if (thisID > maxID)
                    maxID = thisID;
            }
            return maxID;
        }
    }
}
