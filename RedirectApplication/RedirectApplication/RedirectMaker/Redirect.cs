using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RedirectApplication.RedirectDB;
using RedirectApplication.Models;
using Newtonsoft.Json;
using System.IO;

namespace RedirectApplication.RedirectMaker
{
    public class Redirect
    {
        RedirectRepository db = new RedirectRepository();

        public DbJson DeserializationJsonFromDb(UsersAttributes user)
        {
            var json = db.GetJsonFromDb(user.Url);
            var reader = new JsonTextReader(new StringReader(json));
            return JsonSerializer.CreateDefault().Deserialize<DbJson>(reader);
        }

        public string VerificationByRules(UsersAttributes user)
        {
            var json = DeserializationJsonFromDb(user);
            var array = json.Conditions.ToArray();
            var correct = true;
            foreach(var fields in array)
            {
                if (fields is Composite)
                {
                    var composite = fields as Composite;
                    var rules = composite.Rules;
                    foreach(var oneRule in rules)
                    {
                        if (oneRule is ByBrowser)
                        {
                            var byBrowser = oneRule as ByBrowser;
                            if (user.Browser == byBrowser.Browser)
                            {
                                continue;
                            }
                            else
                            {
                                correct = false;
                                break;
                            }
                        }
                        else if (oneRule is ByLanguage)
                        {
                            var byLanguage = oneRule as ByLanguage;
                            if (user.Language == byLanguage.Language)
                            {
                                continue;
                            }
                            else
                            {
                                correct = false;
                                break;
                            }
                        }
                        else if (oneRule is ByCountry)
                        {
                            var byCountry = oneRule as ByCountry;
                            if (user.Country == byCountry.Country)
                            {
                                continue;
                            }
                            else
                            {
                                correct = false;
                                break;
                            }
                        }
                        else if (oneRule is ByIp)
                        {
                            var byIp = oneRule as ByIp;
                            if (user.UserIP == byIp.Ip[0])
                            {
                                continue;
                            }
                            else if (user.UserIP == byIp.Ip[1])
                            {
                                continue;
                            }
                            else
                            {
                                correct = false;
                                break;
                            }
                        }
                        else if (oneRule is ByOS)
                        {
                            var byOS = oneRule as ByOS;
                            if (user.OS == byOS.OS)
                            {
                                continue;
                            }
                            else
                            {
                                correct = false;
                                break;
                            }
                        }
                        else if (oneRule is ByDevice)
                        {
                            var byDevice = oneRule as ByDevice;
                            if (user.MobileOrNot == byDevice.Device)
                            {
                                continue;
                            }
                            else
                            {
                                correct = false;
                                break;
                            }
                        }
                        else if (oneRule is ByDate)
                        {
                            var byDate = oneRule as ByDate;
                            if (user.Time == byDate.Date)
                            {
                                continue;
                            }
                            else
                            {
                                correct = false;
                                break;
                            }
                        }
                    }
                    if (correct == false)
                    {
                        return "404";
                    }
                    else return composite.Url;
                }
                else if (fields is ByBrowser)
                {
                    var byBrowser = fields as ByBrowser;
                    if (user.Browser == byBrowser.Browser)
                    {
                        return byBrowser.Url;
                    }
                }
                else if (fields is ByLanguage)
                {
                    var byLanguage = fields as ByLanguage;
                    if (user.Language == byLanguage.Language)
                    {
                        return byLanguage.Url;
                    }
                }
                else if (fields is ByCountry)
                {
                    var byCountry = fields as ByCountry;
                    if (user.Country == byCountry.Country)
                    {
                        return byCountry.Url;
                    }
                }
                else if (fields is ByIp)
                {
                    var byIp = fields as ByIp;
                    if (user.UserIP == byIp.Ip[0])
                    {
                        return byIp.Url;
                    }
                    else if (user.UserIP == byIp.Ip[1])
                    {
                        return byIp.Url;
                    }
                }
                else if (fields is ByOS)
                {
                    var byOS = fields as ByOS;
                    if (user.OS == byOS.OS)
                    {
                        return byOS.Url;
                    }
                }
                else if (fields is ByDevice)
                {
                    var byDevice = fields as ByDevice;
                    if (user.MobileOrNot == byDevice.Device)
                    {
                        return byDevice.Url;
                    }
                }
                else if (fields is ByDate)
                {
                    var byDate = fields as ByDate;
                    if (user.Time == byDate.Date)
                    {
                        return byDate.Url;
                    }
                }
            }
            return null;
        }
    }
}
