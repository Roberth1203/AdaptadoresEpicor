using Epicor.ServiceModel.StandardBindings;
using Ice.BO;
using Ice.Lib;
using Ice.Proxy.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using static Ice.BO.UD01DataSet;


namespace EpicorConnect
{
    public class Patern
    {
        protected Credenciales cred;
        protected string FileSys;
        protected string gCompany;

        public Patern(string environment, string company, string user, string password) //ideal pruebas en varios ambientes
        {
            gCompany = company;
            FileSys = string.Format("{0}{1}.sysconfig", "C:\\Epicor\\ERP10.0ClientTest\\Client\\config\\", environment);
            cred.UserName = user;
            cred.Password = password;
        }

        public Patern(string user, string password, string company) // apps tipo login
        {
            cred = new Credenciales();
            gCompany = company;
            FileSys = "C:\\Epicor\\ERP10.0ClientTest\\Client\\config\\Epicor10.sysconfig";
            cred.UserName = user;
            cred.Password = password;

            CambiaCompañia(gCompany);
        }

        public Patern(string company) // acceso como manager
        {
            gCompany = company;
            FileSys = "C:\\Epicor\\ERP10.0ClientTest\\Client\\config\\Epicor10.sysconfig";
            cred.UserName = "manager";
            cred.Password = "manager";
        }

        public void updateUD01()
        {
            bool Res = true;
            bool existe = false;
            string appServerUrl = string.Empty;

            EnvironmentInformation.ConfigurationFileName = FileSys;
            appServerUrl = AppSettingsHandler.AppServerUrl;
            CustomBinding wcfBinding = new CustomBinding();
            wcfBinding = NetTcp.UsernameWindowsChannel();
            Uri CustSvcUri = new Uri(string.Format("{0}/Ice/BO/{1}.svc", appServerUrl, "UD01"));
            using (UD01Impl OB = new UD01Impl(wcfBinding, CustSvcUri))
            {
                OB.ClientCredentials.UserName.UserName = cred.UserName;
                OB.ClientCredentials.UserName.Password = cred.Password;
                UD01DataSet dt = new UD01DataSet();

                try
                {
                    dt = OB.GetByID("hola1","","","","");
                }
                catch(Exception e)
                {
                    existe = false;

                }
                if (!existe)
                {
                    OB.GetaNewUD01(dt);

                    dt.Tables["UD01"].Rows[0]["Company"] = gCompany;
                    dt.Tables["UD01"].Rows[0]["key1"] = "hola1";
                    dt.Tables["UD01"].Rows[0]["key2"] = "";
                    dt.Tables["UD01"].Rows[0]["key3"] = "";
                    dt.Tables["UD01"].Rows[0]["key4"] = "";
                    dt.Tables["UD01"].Rows[0]["key5"] = "";
                }
                
                dt.Tables["UD01"].Rows[0]["character01"] = "prueba_01:15";

                OB.Update(dt);
            }
        }

        public void insertUD01()
        {
            bool Res = true;
            string appServerUrl = string.Empty;

            EnvironmentInformation.ConfigurationFileName = FileSys;
            appServerUrl = AppSettingsHandler.AppServerUrl;
            CustomBinding wcfBinding = new CustomBinding();
            wcfBinding = NetTcp.UsernameWindowsChannel();
            Uri CustSvcUri = new Uri(string.Format("{0}/Ice/BO/{1}.svc", appServerUrl, "UD01"));
            using (UD01Impl OB = new UD01Impl(wcfBinding, CustSvcUri))
            {
                OB.ClientCredentials.UserName.UserName = cred.UserName;
                OB.ClientCredentials.UserName.Password = cred.Password;
                UD01DataSet dt = new UD01DataSet();
                OB.GetaNewUD01(dt);

                dt.Tables["UD01"].Rows[0]["Company"] = gCompany;
                dt.Tables["UD01"].Rows[0]["key1"] = "T4921O";
                dt.Tables["UD01"].Rows[0]["key2"] = "";
                dt.Tables["UD01"].Rows[0]["key3"] = "";
                dt.Tables["UD01"].Rows[0]["key4"] = "";
                dt.Tables["UD01"].Rows[0]["key5"] = "";
                dt.Tables["UD01"].Rows[0]["character01"] = "prueba_12:59";

                OB.Update(dt);
            }
        }

        private void CambiaCompañia(string Compañia)
        {
            bool Res = true;
            string appServerUrl = string.Empty;

            EnvironmentInformation.ConfigurationFileName = FileSys;
            appServerUrl = AppSettingsHandler.AppServerUrl;
            CustomBinding wcfBinding = new CustomBinding();
            wcfBinding = NetTcp.UsernameWindowsChannel();
            Uri CustSvcUri = new Uri(string.Format("{0}/Ice/BO/{1}.svc", appServerUrl, "UserFile"));

            using (UserFileImpl US = new UserFileImpl(wcfBinding, CustSvcUri))
            {
                try
                {
                    US.ClientCredentials.UserName.UserName = cred.UserName;
                    US.ClientCredentials.UserName.Password = cred.Password;
                    US.SaveSettings(cred.UserName, true, gCompany, true, false, true, true, true, true, true, true, true,
                                           false, false, -2, 0, 1456, 886, 2, "MAINMENU", "", "", 0, -1, 0, "", false);
                }
                catch (Exception ex)
                {
                    US.Close();
                    US.Dispose();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    US.Close();
                    US.Dispose();
                }


            }
        }
    }


}
