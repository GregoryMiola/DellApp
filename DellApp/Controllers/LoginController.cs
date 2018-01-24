using DellApp.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace DellApp.Controllers
{
    public class LoginController : Controller
    {
        private string errorMsg = "The email and/or password entered is invalid.Please try again.";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnURL"></param>
        /// <returns></returns>
        public ActionResult DoIt(string returnURL)
        {
            //Recebe a url que o usuário tentou acessar
            ViewBag.ReturnUrl = returnURL;
            return View(new UserSys());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoIt(UserSys login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (DellAppDB db = new DellAppDB())
                {
                    var vLogin = db.UserSys.Where(p => p.Email.Equals(login.Email)).FirstOrDefault();

                    //Verificar se a variavel vLogin está vazia. Isso pode ocorrer caso o usuário não existe. Caso não exista ele vai cair na condição else.
                    if (vLogin != null)
                    {
                        //Código abaixo verifica se a senha digitada no site é igual a senha que está sendo retornada do banco. Caso não cai direto no else
                        if (Equals(vLogin.Password, login.Password))
                        {
                            FormsAuthentication.SetAuthCookie(vLogin.Email, false);

                            //retorna para a tela inicial do Home
                            return RedirectToAction("Index", "Home");
                        }
                        //Else responsável da validação da senha
                        else
                        {
                            //Escreve na tela a mensagem de erro informada
                            ModelState.AddModelError("", errorMsg);
                            //Retorna a tela de login
                            return View(new UserSys());
                        }
                    }
                    //Else responsável por verificar se o usuário existe
                    else
                    {
                        //Escreve na tela a mensagem de erro informada
                        ModelState.AddModelError("", errorMsg);
                        //Retorna a tela de login
                        return View(new UserSys());
                    }
                }
            }

            //Caso os campos não esteja de acordo com a solicitação retorna a tela de login com as mensagem dos campos
            return View(login);
        }
    }
}