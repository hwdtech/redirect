using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RedirectApplication.Models;
namespace RedirectApplication.RedirectDB
{
    public class RedirectInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<RedirectContext>
    {
        protected override void Seed(RedirectContext context)
        {
            var redirs = new List<RedirectRule>
            {
                new RedirectRule {TargetUrl = "https://habrahabr.ru/post/175999/", Conditions = "Урок 0. Вступление" },
                new RedirectRule {TargetUrl = "https://habrahabr.ru/post/176001/", Conditions = "Урок 1. Мы просто создадим и запустим проект. И немного изучим NuGet. NLog и Logger." },
                new RedirectRule {TargetUrl = "https://habrahabr.ru/post/176007/", Conditions = "Урок 2. Изучение Dependency Injection. Изучим различные реализации. Ninject, Unity, Autofac" },
                new RedirectRule {TargetUrl = "https://habrahabr.ru/post/176017/", Conditions = "Урок 3. Работа с БД. SQL-команды. LinqToSql" },
                new RedirectRule {TargetUrl = "https://habrahabr.ru/post/176021/", Conditions = "Урок 4. Маршруты и связки. Структура asp.net mvc – приложения" },
                new RedirectRule {TargetUrl = "https://habrahabr.ru/post/176023/", Conditions = "Урок 5. Создание записи в БД через веб-интерфейс. Валидация данных. Automapping" },
                new RedirectRule {TargetUrl = "https://habrahabr.ru/post/176043/", Conditions = "Урок 6. Авторизация (и почему мы не используем стандартный MembershipProvider)" },
                new RedirectRule {TargetUrl = "https://habrahabr.ru/post/176053/", Conditions = "Урок 7. Html, css, Bootrap, jquery. Справочные данные о клиентской части" },
                new RedirectRule {TargetUrl = "https://habrahabr.ru/post/176063/", Conditions = "Урок 8. View, Razor. Изучаем View-engine Razor. Дополняем наше приложение страницей с обработкой ошибки" },
                new RedirectRule {TargetUrl = "https://habrahabr.ru/post/176069/", Conditions = "Урок 9. Configuration, и работа с загрузкой файлов. Обработка изображений" },
                new RedirectRule {TargetUrl = "https://habrahabr.ru/post/176075/", Conditions = "Урок A. Работа с почтой и sms" },
                new RedirectRule {TargetUrl = "https://habrahabr.ru/post/176087/", Conditions = "Урок B. Json, работа с этим форматом. Json.net" },
                new RedirectRule {TargetUrl = "https://habrahabr.ru/post/176095/", Conditions = "Урок С. Создание мультиязычного сайта" },
                new RedirectRule {TargetUrl = "https://habrahabr.ru/post/176097/", Conditions = "Урок D. Scaffolding" },
                new RedirectRule {TargetUrl = "https://habrahabr.ru/post/176137/", Conditions = "Урок E. Тестирование" },
                new RedirectRule {TargetUrl = "https://habrahabr.ru/post/176139/", Conditions = "Урок F. Работа как она есть. Мои принципы работы. Как писать ТЗ." }
            };
            redirs.ForEach(s => context.RedirectRules.Add(s));
            context.SaveChanges();
        }
    }
}