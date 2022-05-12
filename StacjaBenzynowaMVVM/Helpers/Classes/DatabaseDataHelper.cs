﻿using StacjaBenzynowaLibrary;
using StacjaBenzynowaMVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StacjaBenzynowaMVVM.Helpers.Classes
{
    class DatabaseDataHelper
    {


        public static ObservableCollection<Product> GetProducts()
        {
            return DatabaseClassesHelper.GetModels<Product>
                (
                    DataBaseAccess.GetImportedData
                    (
                        "SELECT * FROM DOSTEPNE_PRODUKTY WHERE ILOSC>0",
                        new List<KeyValuePair<KeyValuePair<string, string>, string>>
                        {
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_PRODUKTU",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NAZWA",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("CENA",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("DATA_DOSTAWY",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("DATA_WAZNOSCI",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ILOSC",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("RABAT",""),"")
                        }
                    )
                );
        }

        public static ObservableCollection<Employee> GetEmployees()
        {
            return DatabaseClassesHelper.GetModels<Employee>
                (
                    DataBaseAccess.GetImportedData
                    (
                        "SELECT ID_PRACOWNIKA,IMIE,NAZWISKO,LOGIN,Z.STANOWISKO AS POZYCJA,ZATRUDNIONY FROM PRACOWNICY LEFT OUTER JOIN STANOWISKA Z ON PRACOWNICY.ID_STANOWISKA=Z.ID_STANOWISKA WHERE ZATRUDNIONY==1",
                        new List<KeyValuePair<KeyValuePair<string, string>, string>>
                        {
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_PRACOWNIKA",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("IMIE",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NAZWISKO",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("POZYCJA",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ZATRUDNIONY",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("LOGIN",""),"")
                        }
                    )
                );
        }

        public static ObservableCollection<Client> GetClients()
        {
            return DatabaseClassesHelper.GetModels<Client>
                (
                    DataBaseAccess.GetImportedData
                    (
                        "SELECT * FROM KLIENCI WHERE AKTYWNY==1",
                        new List<KeyValuePair<KeyValuePair<string, string>, string>>
                        {
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_KLIENTA",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("IMIE",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NAZWISKO",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NIP",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NUMER_TELEFONU",""),""),
                        }
                    )
                );
        }

        public static ObservableCollection<Sale> GetSales()
        {
            return DatabaseClassesHelper.GetModels<Sale>
                (
                    DataBaseAccess.GetImportedData
                    (
                        "SELECT ID_ZAMOWNIENIA, O.IMIE || ' ' || O.NAZWISKO AS IMIE_KLIENTA, P.IMIE || ' ' || P.NAZWISKO AS IMIE_SPRZEDAWCY, DATA_WYDANIA FROM ZAMOWIENIA Z LEFT OUTER JOIN KLIENCI O ON Z.ID_KLIENTA = O.ID_KLIENTA LEFT OUTER JOIN PRACOWNICY P ON Z.ID_PRACOWNIKA = P.ID_PRACOWNIKA",
                        new List<KeyValuePair<KeyValuePair<string, string>, string>>
                        {
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_ZAMOWNIENIA",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("IMIE_KLIENTA",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("IMIE_SPRZEDAWCY",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("DATA_WYDANIA",""),""),
                        }
                    )
                );
        }

        public static ObservableCollection<SaleDetails> GetSaleDetails(int id)
        {
            return DatabaseClassesHelper.GetModels<SaleDetails>
                (
                    DataBaseAccess.GetImportedData
                    (
                        "SELECT Z.ID_ZAMOWNIENIA AS NUMER, P.NAZWA, O.ILOSC, O.CENA FROM OPISY_ZAMOWIEN O LEFT OUTER JOIN PRODUKTY P ON O.ID_PRODUKTU = P.ID_PRODUKTU LEFT OUTER JOIN ZAMOWIENIA Z ON O.ID_ZAMOWIENIA = Z.ID_ZAMOWNIENIA WHERE NUMER=@numer",
                        new List<KeyValuePair<KeyValuePair<string, string>, string>>
                        {
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NUMER","@numer"),id.ToString()),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NAZWA",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ILOSC",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("CENA",""),""),
                        }
                    )
                );
        }

        public static Client GetClient(string cardCode)
        {
            Client client=null;
            List<Dictionary<string, object>> answer = DataBaseAccess.GetImportedData("SELECT * FROM KLIENCI WHERE ID_KLIENTA=@id", 
                new List<KeyValuePair<KeyValuePair<string, string>, string>>
                {
                    new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_KLIENTA","@id"),cardCode),
                    new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("IMIE",""),""),
                    new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NAZWISKO",""),""),
                    new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NIP",""),"")
                });
            client= DatabaseClassesHelper.GetModel<Client>(answer);
            if(client!=null)
            {
                answer = DataBaseAccess.GetImportedData("SELECT SUM(IFNULL(PUNKTY,0)-(IFNULL(RABAT,0)*100)) AS PUNKTY FROM ZAMOWIENIA WHERE ID_KLIENTA=@id",
                    new List<KeyValuePair<KeyValuePair<string, string>, string>>
                    {
                        new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("PUNKTY",""),""),
                        new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_KLIENTA","@id"),cardCode)
                    });
                client.PUNKTY = DatabaseClassesHelper.GetClientPoints(answer);
            }
            return client;
        }

        public static Client GetClientNIP(string nip)
        {
            return DatabaseClassesHelper.GetModel<Client>
                (
                    DataBaseAccess.GetImportedData
                    (
                        "SELECT * FROM KLIENCI WHERE NIP=@nip",
                        new List<KeyValuePair<KeyValuePair<string, string>, string>>
                        {
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_KLIENTA",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("IMIE",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NAZWISKO",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NIP","@nip"), nip),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NUMER_TELEFONU",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("AKTYWNY",""),"")
                        }
                    )
               );
        }

        public static Client GetClientPhone(string phoneNumber)
        {
            return DatabaseClassesHelper.GetModel<Client>
                (
                    DataBaseAccess.GetImportedData
                    (
                        "SELECT * FROM KLIENCI WHERE NUMER_TELEFONU=@numer_telefonu",
                        new List<KeyValuePair<KeyValuePair<string, string>, string>>
                        {
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_KLIENTA",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("IMIE",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NAZWISKO",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NIP",""), ""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NUMER_TELEFONU","@numer_telefonu"),phoneNumber),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("AKTYWNY",""),"")
                        }
                    )
               );
        }

        public static int SetClient(string name, string surname, string nip, string phone_number, int active)
        {
            List<KeyValuePair<KeyValuePair<string, string>, string>> parameters = new List<KeyValuePair<KeyValuePair<string, string>, string>>
            {
            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("IMIE", "@imie"), name),
            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NAZWISKO", "@nazwisko"), surname),
            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NIP", "@nip"), nip),
            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NUMER_TELEFONU", "@numer_telefonu"), phone_number),
            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("AKTYWNY", "@aktywny"), active.ToString())
            };
            KeyValuePair<string, string> parameter = InsertParametersToString(parameters);
            return DataBaseAccess.SetData("INSERT INTO KLIENCI (" + parameter.Key + ") VALUES (" + parameter.Value + ")", parameters);
        }

        public static int SetEmployee(string name, string surname, int position, int empoyed, string login, string password)
        {
            List<KeyValuePair<KeyValuePair<string, string>, string>> parameters = new List<KeyValuePair<KeyValuePair<string, string>, string>>
            {
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("IMIE", "@imie"), name),
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NAZWISKO", "@nazwisko"), surname),
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_STANOWISKA", "@pozycja"), position.ToString()),
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ZATRUDNIONY", "@zatrudniony"), empoyed.ToString()),
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("LOGIN", "@login"), login),
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("HASLO", "@haslo"), password),
            };
            KeyValuePair<string, string> parameter = InsertParametersToString(parameters);
            return DataBaseAccess.SetData("INSERT INTO PRACOWNICY (" + parameter.Key + ") VALUES (" + parameter.Value + ")", parameters);
        }

        public static int SetSupplier(string name)
        {
            List<KeyValuePair<KeyValuePair<string, string>, string>> parameters = new List<KeyValuePair<KeyValuePair<string, string>, string>>
            {
            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NAZWA_FIRMY", "@nazwa"), name),
            };
            KeyValuePair<string, string> parameter = InsertParametersToString(parameters);
            return DataBaseAccess.SetData("INSERT INTO DOSTAWCY (" + parameter.Key + ") VALUES (" + parameter.Value + ")", parameters);
        }

        public static int SetProducts(ObservableCollection<Product> products)
        {
            List<string> statements = new List<string>();
            List<List<KeyValuePair<KeyValuePair<string, string>, string>>> parameters = new List<List<KeyValuePair<KeyValuePair<string, string>, string>>>();
            List<KeyValuePair<KeyValuePair<string, string>, string>> parameter;
            string statement = "";
            KeyValuePair<string, string> param;
            foreach (Product p in products)
            {
                parameter = new List<KeyValuePair<KeyValuePair<string, string>, string>>();
                parameter.Add(new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NAZWA", "@id_produktu"), p.NAZWA));
                parameter.Add(new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ILOSC", "@ilosc"), p.ILOSC.ToString()));
                parameter.Add(new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("CENA", "@cena"), p.CENA.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)));
                parameter.Add(new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("DATA_DOSTAWY", "@deliverDate"), p.DATA_DOSTAWY.ToString()));
                if(p.DATA_WAZNOSCI.Equals(new DateTime()))
                    parameter.Add(new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("DATA_WAZNOSCI", "@expDate"), ""));
                else
                    parameter.Add(new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("DATA_WAZNOSCI", "@expDate"), p.DATA_WAZNOSCI.ToString()));
                parameter.Add(new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_DOSTAWCY", "@id"), p.ID_DOSTAWCY.ToString()));
                param = InsertParametersToString(parameter);
                statement = "INSERT INTO PRODUKTY (" + param.Key + ") VALUES (" + param.Value + ")";
                statements.Add(statement);
                parameters.Add(parameter);
            }

            return DataBaseAccess.SetDataTransaction(statements, parameters);
        }

        public static int SetSale(Client client, ObservableCollection<Product> products, Employee employee, double price)
        {
            List<string> statements = new List<string>();
            List<List<KeyValuePair<KeyValuePair<string, string>, string>>> parameters = new List<List<KeyValuePair<KeyValuePair<string, string>, string>>>();
            List<KeyValuePair<KeyValuePair<string, string>, string>> parameter = new List<KeyValuePair<KeyValuePair<string, string>, string>>
            {
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_PRACOWNIKA","@id_pracownika"),employee.ID_PRACOWNIKA.ToString()),
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("DATA_WYDANIA","@data"),DateTime.Now.ToString()),
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_KLIENTA", "@id_klienta"), client.GetClientID()),
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("RABAT", "@rabat"), client.RABAT.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)),
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("PUNKTY", "@punkty"), (price / 100).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture))
            };
            KeyValuePair<string, string> param = InsertParametersToString(parameter);
            string statement = "INSERT INTO ZAMOWIENIA (" + param.Key + ") VALUES (" + param.Value + ")";

            statements.Add(statement);
            parameters.Add(parameter);

            foreach (Product p in products)
            {
                parameter = new List<KeyValuePair<KeyValuePair<string, string>, string>>();
                parameter.Add(new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_ZAMOWIENIA", "@FIRSTINSERTEDROW"), ""));
                parameter.Add(new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_PRODUKTU", "@id_produktu"), p.ID_PRODUKTU.ToString()));
                parameter.Add(new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ILOSC", "@ilosc"), p.ILOSC.ToString()));
                parameter.Add(new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("CENA", "@cena"), p.CENA.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)));
                param = InsertParametersToString(parameter);
                statement="INSERT INTO OPISY_ZAMOWIEN (" + param.Key + ") VALUES (" + param.Value + ")";
                statements.Add(statement);
                parameters.Add(parameter);
            }

            return DataBaseAccess.SetDataTransaction(statements, parameters);
        }

        public static int UpdateProducts(List<Product> products)
        {
            List<string> statements = new List<string>();
            List<List<KeyValuePair<KeyValuePair<string, string>, string>>> parameters = new List<List<KeyValuePair<KeyValuePair<string, string>, string>>>();
            List<KeyValuePair<KeyValuePair<string, string>, string>> parameter;
            string statement = "";
            foreach (Product p in products)
            {
                parameter = new List<KeyValuePair<KeyValuePair<string, string>, string>>();
                parameter.Add(new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_PRODUKTU", "@id"), p.ID_PRODUKTU.ToString()));
                parameter.Add(new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("RABAT", "@discount"), p.RABAT.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)));
                statement = "UPDATE PRODUKTY SET RABAT=@discount WHERE ID_PRODUKTU=@id";
                statements.Add(statement);
                parameters.Add(parameter);
            }

            return DataBaseAccess.SetDataTransaction(statements, parameters);
        }

        public static int UpdateEmployee(Employee employee)
        {
            List<KeyValuePair<KeyValuePair<string, string>, string>> parameters = new List<KeyValuePair<KeyValuePair<string, string>, string>>
            {
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("IMIE", "@imie"), employee.IMIE),
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NAZWISKO", "@nazwisko"), employee.NAZWISKO),
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_STANOWISKA", "@pozycja"), employee.POZYCJA),
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ZATRUDNIONY", "@zatrudniony"), employee.ZATRUDNIONY.ToString()),
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("LOGIN", "@login"), employee.LOGIN),
            };
            string parameter = UpdateParametersToString(parameters);
            parameters.Add(new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_PRACOWNIKA", "@id"), employee.ID_PRACOWNIKA.ToString()));
            return DataBaseAccess.SetData("UPDATE PRACOWNICY SET "+parameter+ " WHERE ID_PRACOWNIKA=@id", parameters);
        }
        
        public static int UpdateEmployeePassword(string password, string id)
        {
            List<KeyValuePair<KeyValuePair<string, string>, string>> parameters = new List<KeyValuePair<KeyValuePair<string, string>, string>>
            {
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("HASLO", "@haslo"), PasswordHashHelper.HashPassword(password))
            };
            string parameter = UpdateParametersToString(parameters);
            parameters.Add(new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_PRACOWNIKA", "@id"), id));
            return DataBaseAccess.SetData("UPDATE PRACOWNICY SET " + parameter + " WHERE ID_PRACOWNIKA=@id", parameters);
        }

        public static int UpdateClient(Client client)
        {
            List<KeyValuePair<KeyValuePair<string, string>, string>> parameters = new List<KeyValuePair<KeyValuePair<string, string>, string>>
            {
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("IMIE", "@imie"), client.IMIE),
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NAZWISKO", "@nazwisko"), client.NAZWISKO),
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NIP", "@nip"), client.NIP),
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NUMER_TELEFONU", "@numer_telefonu"), client.NUMER_TELEFONU),
                new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("AKTYWNY", "@aktywny"), client.AKTYWNY.ToString())
            };
            string parameter = UpdateParametersToString(parameters);
            parameters.Add(new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_KLIENTA", "@id"), client.ID_KLIENTA.ToString()));
            return DataBaseAccess.SetData("UPDATE KLIENCI SET " + parameter + " WHERE ID_KLIENTA=@id", parameters);
        }

        public static int SetExpiredProducts(List<Product> products)
        {
            List<string> statements = new List<string>();
            List<List<KeyValuePair<KeyValuePair<string, string>, string>>> parameters = new List<List<KeyValuePair<KeyValuePair<string, string>, string>>>();
            List<KeyValuePair<KeyValuePair<string, string>, string>> parameter;
            string statement = "";
            KeyValuePair<string, string> param;
            foreach (Product p in products)
            {
                parameter = new List<KeyValuePair<KeyValuePair<string, string>, string>>();
                parameter.Add(new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_PRODUKTU", "@id_produktu"), p.ID_PRODUKTU.ToString()));
                parameter.Add(new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ILOSC", "@ilosc"), p.ILOSC.ToString()));
                param = InsertParametersToString(parameter);
                statement = "INSERT INTO PRODUKTY_PO_TERMINIE (" + param.Key + ") VALUES (" + param.Value + ")";
                statements.Add(statement);
                parameters.Add(parameter);
            }

            return DataBaseAccess.SetDataTransaction(statements, parameters);
        }

        public static Employee GetEmployee(string userName, string password)
        {
            return DatabaseClassesHelper.GetModel<Employee>
                (
                    DataBaseAccess.GetImportedData
                    (
                        "SELECT ID_PRACOWNIKA,IMIE,NAZWISKO,S.STANOWISKO AS POZYCJA,ZATRUDNIONY FROM PRACOWNICY P LEFT OUTER JOIN STANOWISKA S ON P.ID_STANOWISKA=S.ID_STANOWISKA WHERE LOGIN=@login AND HASLO=@password",
                        new List<KeyValuePair<KeyValuePair<string, string>, string>>
                        {
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_PRACOWNIKA",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("IMIE",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NAZWISKO",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("POZYCJA",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ZATRUDNIONY",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("LOGIN","@login"),userName),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("HASLO","@password"),PasswordHashHelper.HashPassword(password))
                        }
                    )
               );
        }

        public static Employee GetEmployeeLogin(string userName)
        {
            return DatabaseClassesHelper.GetModel<Employee>
                (
                    DataBaseAccess.GetImportedData
                    (
                        "SELECT ID_PRACOWNIKA,IMIE,NAZWISKO,S.STANOWISKO AS POZYCJA,ZATRUDNIONY FROM PRACOWNICY P LEFT OUTER JOIN STANOWISKA S ON P.ID_STANOWISKA=S.ID_STANOWISKA WHERE LOGIN=@login",
                        new List<KeyValuePair<KeyValuePair<string, string>, string>>
                        {
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_PRACOWNIKA",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("IMIE",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NAZWISKO",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("POZYCJA",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ZATRUDNIONY",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("LOGIN","@login"),userName),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("HASLO","@password"), "")
                        }
                    )
               );
        }

        public static Employee GetEmployeePassword(string password)
        {
            return DatabaseClassesHelper.GetModel<Employee>
                (
                    DataBaseAccess.GetImportedData
                    (
                        "SELECT ID_PRACOWNIKA,IMIE,NAZWISKO,S.STANOWISKO AS POZYCJA,ZATRUDNIONY FROM PRACOWNICY P LEFT OUTER JOIN STANOWISKA S ON P.ID_STANOWISKA=S.ID_STANOWISKA WHERE HAslo=@haslo",
                        new List<KeyValuePair<KeyValuePair<string, string>, string>>
                        {
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_PRACOWNIKA",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("IMIE",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NAZWISKO",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("POZYCJA",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ZATRUDNIONY",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("LOGIN","@login"),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("HASLO","@haslo"), PasswordHashHelper.HashPassword(password))
                        }
                    )
               );
        }

        public static ObservableCollection<Supplier> GetSuppliers()
        {
            return DatabaseClassesHelper.GetModels<Supplier>
                (
                    DataBaseAccess.GetImportedData
                    (
                        "SELECT * FROM DOSTAWCY",
                        new List<KeyValuePair<KeyValuePair<string, string>, string>>
                        {
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_DOSTAWCY",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("NAZWA_FIRMY",""),"")
                        }
                    )
               );
        }

        public static ObservableCollection<Position> GetPositions()
        {
            return DatabaseClassesHelper.GetModels<Position>
                (
                    DataBaseAccess.GetImportedData
                    (
                        "SELECT * FROM STANOWISKA",
                        new List<KeyValuePair<KeyValuePair<string, string>, string>>
                        {
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("ID_STANOWISKA",""),""),
                            new KeyValuePair<KeyValuePair<string, string>, string>(new KeyValuePair<string, string>("STANOWISKO",""),"")
                        }
                    )
               );
        }

        public static KeyValuePair<string,string> InsertParametersToString(List<KeyValuePair<KeyValuePair<string, string>, string>> parameters)
        {
            string key="";
            string value="";

            foreach(KeyValuePair<KeyValuePair<string, string>, string> pair in parameters)
            {
                key += pair.Key.Key+",";
                value += pair.Key.Value+",";
            }
            key = key.Remove(key.Length - 1, 1);
            value = value.Remove(value.Length - 1, 1);
            return new KeyValuePair<string, string>(key,value);
        }

        public static string UpdateParametersToString(List<KeyValuePair<KeyValuePair<string, string>, string>> parameters)
        {
            string s = "";

            foreach (KeyValuePair<KeyValuePair<string, string>, string> pair in parameters)
            {
                s += pair.Key.Key+"="+pair.Key.Value + ",";
            }
            s = s.Remove(s.Length - 1, 1);
            return s;
        }

    }

}
