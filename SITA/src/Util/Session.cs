using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SITA.src.Model;

namespace SITA.src.Util
{
    /*
     * Instancializa uma classe de sessão principal responsável por guardar a sessão
     * de uma tela para outra/função para outra
     * 
     * Armazenada dentro da raíz do projeto de forma pública para ter acesso
     * em diferentes lugares dentro do projeto
     */
    public static class Session
    {
        private static AccessType AccessType = new AccessType();
        private static User? AccessUser = null;
        /* 
         * public void InitSession(AccessType NewAccess) ->  Inicia uma sessão apartir de um valor AccessType passado
         * por parâmetro, podendo ser independente ou provinda de um usuário.
         * Exemplos:
         * 
         * ==============================================
           
           -- Independente:
             AccessType newAccess = new AccessType();
             AccessType.Level = 0; // Aluno
             AccessType.Name = "Aluno(a)";
          
             ProgramSession.InitSession(newAccess);
             
           ==============================================
           
           -- Usuário:
             User? newUser = userStorage.GetData("User1");
             if(newUser != null)
             {
                 ProgramSession.InitSession(newUser.AccessType);
             }

         * ==============================================
         */
        public static void InitSession(User user)
        {
            AccessType = user.AccessType;
            AccessUser = user;
        }

        // Retorna apenas AccessType da instância privada
        public static AccessType GetInstanceAccess()
        {
            return AccessType;
        }
        public static bool IsLoggedIn()
        {
            return AccessType.Level >= 0;
        }
        private static string? LoggedInKey;

        public static void InitSession(AccessType newAccess, string? key = null)
        {
            AccessType = newAccess;
            LoggedInKey = key;
        }

        public static string? GetLoggedInKey() => LoggedInKey;
        public static User? GetLoggedInUser() => AccessUser;
    }
}
