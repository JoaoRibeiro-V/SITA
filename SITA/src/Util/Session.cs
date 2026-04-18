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
    public class Session
    {
        private static Session Instance = new Session();
        private AccessType AccessType = new AccessType();
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
        public void InitSession(AccessType NewAccess)
        {
            Instance.AccessType = NewAccess;
            //Console.WriteLine("Changed access to: " + Instance.AccessType.Name);
        }

        // Retorna apenas AccessType da instância privada
        public AccessType GetInstanceAccess()
        {
            return Instance.AccessType;
        }
        public bool IsLoggedIn()
        {
            return Instance.AccessType.Level >= 0;
        }
        private string? LoggedInKey;

        public void InitSession(AccessType newAccess, string? key = null)
        {
            Instance.AccessType = newAccess;
            Instance.LoggedInKey = key;
        }

        public string? GetLoggedInKey() => Instance.LoggedInKey;
    }
}
