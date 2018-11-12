using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Models {
    public sealed class LoginSingleton {
        private static LoginSingleton instance = new LoginSingleton();
        private static readonly object padlock = new object();
        private static string userName = null;

        LoginSingleton() {
        }

        public static void setUser(string p_userName) {
            userName = p_userName;
        }

        public static string getUserName() {
            return userName;
        }

        public static LoginSingleton getInstance() {
            
            lock (padlock) {
                if (instance == null) {
                    instance = new LoginSingleton();
                }
                return instance;
            }
            
        }
    }
}

