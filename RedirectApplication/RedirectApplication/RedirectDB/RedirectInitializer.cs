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
                new RedirectRule {Id = "https://habrahabr.ru/post/175999/", Data = "Урок 0. Вступление" },
                new RedirectRule {Id = "https://habrahabr.ru/post/176001/", Data = "Урок 1. Мы просто создадим и запустим проект. И немного изучим NuGet. NLog и Logger." },
                new RedirectRule {Id = "https://habrahabr.ru/post/176007/", Data = "Урок 2. Изучение Dependency Injection. Изучим различные реализации. Ninject, Unity, Autofac" },
                new RedirectRule {Id = "https://habrahabr.ru/post/176017/", Data = "Урок 3. Работа с БД. SQL-команды. LinqToSql" },
                new RedirectRule {Id = "https://habrahabr.ru/post/176021/", Data = "Урок 4. Маршруты и связки. Структура asp.net mvc – приложения" },
                new RedirectRule {Id = "https://habrahabr.ru/post/176023/", Data = "Урок 5. Создание записи в БД через веб-интерфейс. Валидация данных. Automapping" },
                new RedirectRule {Id = "https://habrahabr.ru/post/176043/", Data = "Урок 6. Авторизация (и почему мы не используем стандартный MembershipProvider)" },
                new RedirectRule {Id = "https://habrahabr.ru/post/176053/", Data = "Урок 7. Html, css, Bootrap, jquery. Справочные данные о клиентской части" },
                new RedirectRule {Id = "https://habrahabr.ru/post/176063/", Data = "Урок 8. View, Razor. Изучаем View-engine Razor. Дополняем наше приложение страницей с обработкой ошибки" },
                new RedirectRule {Id = "https://habrahabr.ru/post/176069/", Data = "Урок 9. Configuration, и работа с загрузкой файлов. Обработка изображений" },
                new RedirectRule {Id = "https://habrahabr.ru/post/176075/", Data = "Урок A. Работа с почтой и sms" },
                new RedirectRule {Id = "https://habrahabr.ru/post/176087/", Data = "Урок B. Json, работа с этим форматом. Json.net" },
                new RedirectRule {Id = "https://habrahabr.ru/post/176095/", Data = "Урок С. Создание мультиязычного сайта" },
                new RedirectRule {Id = "https://habrahabr.ru/post/176097/", Data = "Урок D. Scaffolding" },
                new RedirectRule {Id = "https://habrahabr.ru/post/176137/", Data = "Урок E. Тестирование" },
                new RedirectRule {Id = "https://habrahabr.ru/post/176139/", Data = "Урок F. Работа как она есть. Мои принципы работы. Как писать ТЗ." }
            };
            redirs.ForEach(s => context.RedirectRules.Add(s));
            context.SaveChanges();
        }
    }
}